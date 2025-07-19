using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MonoTorrent;
using Torrent_File_Comparer.Helpers;

namespace TorrentFileComparer
{
    public partial class MainForm : Form
    {
        private string selectedTorrentPath = "";
        private string selectedFolderPath = "";
        private Torrent loadedTorrent;

        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnBrowseTorrent_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Torrent files (*.torrent)|*.torrent|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedTorrentPath = openFileDialog.FileName;
                    txtTorrentPath.Text = selectedTorrentPath;

                    try
                    {
                        loadedTorrent = Torrent.Load(selectedTorrentPath);
                        lblStatus.Text = $"Loaded torrent: {loadedTorrent.Name} ({loadedTorrent.Files.Count} files)";
                        UpdateCompareButtonState();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading torrent: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnBrowseFolder_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select folder to compare with torrent";
                
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFolderPath = folderBrowserDialog.SelectedPath;
                    txtFolderPath.Text = selectedFolderPath;
                    UpdateCompareButtonState();
                }
            }
        }

        private void UpdateCompareButtonState()
        {
            btnCompare.Enabled = !string.IsNullOrEmpty(selectedTorrentPath) && 
                               !string.IsNullOrEmpty(selectedFolderPath) && 
                               loadedTorrent != null;
        }

        private async void BtnCompare_Click(object sender, EventArgs e)
        {
            btnCompare.Enabled = false;
            btnDelete.Enabled = false;
            btnSelectAll.Enabled = false;
            btnDeselectAll.Enabled = false;
            progressBar.Visible = true;
            progressBar.Maximum = loadedTorrent.Files.Count;
            progressBar.Value = 0;
            dgvResults.Rows.Clear();

            lblStatus.Text = "Comparing files...";

            try
            {
                await Task.Run(() => CompareFiles());
                lblStatus.Text = "Comparison completed!";
                btnDelete.Enabled = true;
                btnSelectAll.Enabled = true;
                btnDeselectAll.Enabled = true;
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error during comparison: {ex.Message}";
                MessageBox.Show($"Error during comparison: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCompare.Enabled = true;
                progressBar.Visible = false;
            }
        }

        private void CompareFiles()
        {
            var results = new List<FileComparisonResult>();
            var processedFiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // First pass: Check torrent files against local folder
            for (int i = 0; i < loadedTorrent.Files.Count; i++)
            {
                var torrentFile = loadedTorrent.Files[i];
                var result = new FileComparisonResult
                {
                    FileName = torrentFile.Path,
                    TorrentSize = torrentFile.Length,
                    LocalPath = "",
                    LocalSize = 0,
                    Status = "Missing"
                };

                // Update status on UI thread
                this.Invoke(new Action(() => {
                    lblStatus.Text = $"Checking torrent file: {torrentFile.Path}";
                    progressBar.Value = i + 1;
                }));

                // Look for the file in the selected folder
                var possiblePaths = new[]
                {
                    Path.Combine(selectedFolderPath, torrentFile.Path),
                    Path.Combine(selectedFolderPath, Path.GetFileName(torrentFile.Path))
                };

                string foundPath = null;
                foreach (var path in possiblePaths)
                {
                    if (File.Exists(path))
                    {
                        foundPath = path;
                        processedFiles.Add(path.ToLowerInvariant());
                        break;
                    }
                }

                if (foundPath != null)
                {
                    result.LocalPath = foundPath;
                    var fileInfo = new FileInfo(foundPath);
                    result.LocalSize = fileInfo.Length;

                    if (result.LocalSize == result.TorrentSize)
                    {
                        result.Status = "Found - Size Match";
                    }
                    else
                    {
                        result.Status = "Found - Size Mismatch";
                    }
                }

                results.Add(result);
            }

            // Second pass: Find additional files in the folder that aren't in the torrent
            this.Invoke(new Action(() => {
                lblStatus.Text = "Scanning for additional files...";
            }));

            var allLocalFiles = Directory.GetFiles(selectedFolderPath, "*", SearchOption.AllDirectories);
            var additionalFiles = allLocalFiles.Where(f => !processedFiles.Contains(f.ToLowerInvariant())).ToList();

            foreach (var additionalFile in additionalFiles)
            {
                var fileInfo = new FileInfo(additionalFile);
                var relativePath = PathHelper.GetRelativePath(selectedFolderPath, additionalFile);
                
                var result = new FileComparisonResult
                {
                    FileName = relativePath,
                    TorrentSize = 0,
                    LocalPath = additionalFile,
                    LocalSize = fileInfo.Length,
                    Status = "Additional File"
                };

                results.Add(result);
            }

            // Update UI on main thread
            this.Invoke(new Action(() => {
                foreach (var result in results)
                {
                    dgvResults.Rows.Add(
                        false, // Checkbox column
                        result.FileName,
                        result.Status,
                        result.TorrentSize > 0 ? FormatFileSize(result.TorrentSize) : "N/A",
                        result.LocalSize > 0 ? FormatFileSize(result.LocalSize) : "N/A",
                        result.LocalPath
                    );
                    
                    // Color code the rows
                    var row = dgvResults.Rows[dgvResults.Rows.Count - 1];
                    switch (result.Status)
                    {
                        case "Missing":
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                            break;
                        case "Found - Size Match":
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                            break;
                        case "Found - Size Mismatch":
                            row.DefaultCellStyle.BackColor = Color.Orange;
                            break;
                        case "Additional File":
                            row.DefaultCellStyle.BackColor = Color.LightBlue;
                            break;
                    }
                }
            }));
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var filesToDelete = new List<string>();
            var rowsToRemove = new List<DataGridViewRow>();

            // Collect files to delete
            foreach (DataGridViewRow row in dgvResults.Rows)
            {
                if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                {
                    var filePath = row.Cells[5].Value?.ToString();
                    if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                    {
                        filesToDelete.Add(filePath);
                        rowsToRemove.Add(row);
                    }
                }
            }

            if (filesToDelete.Count == 0)
            {
                MessageBox.Show("No files selected for deletion.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete {filesToDelete.Count} selected file(s)?\n\nThis action cannot be undone!", 
                                       "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                int deletedCount = 0;
                var failedFiles = new List<string>();

                foreach (var filePath in filesToDelete)
                {
                    try
                    {
                        File.Delete(filePath);
                        deletedCount++;
                    }
                    catch (Exception ex)
                    {
                        failedFiles.Add($"{Path.GetFileName(filePath)}: {ex.Message}");
                    }
                }

                // Remove deleted files from the grid
                foreach (var row in rowsToRemove)
                {
                    var filePath = row.Cells[5].Value?.ToString();
                    if (!string.IsNullOrEmpty(filePath) && !File.Exists(filePath))
                    {
                        dgvResults.Rows.Remove(row);
                    }
                }

                lblStatus.Text = $"Deleted {deletedCount} file(s).";

                if (failedFiles.Count > 0)
                {
                    MessageBox.Show($"Failed to delete {failedFiles.Count} file(s):\n\n{string.Join("\n", failedFiles)}", 
                                  "Deletion Errors", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvResults.Rows)
            {
                row.Cells[0].Value = true;
            }
        }

        private void BtnDeselectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvResults.Rows)
            {
                row.Cells[0].Value = false;
            }
        }

        private string FormatFileSize(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int counter = 0;
            decimal number = bytes;
            
            while (Math.Round(number / 1024) >= 1)
            {
                number = number / 1024;
                counter++;
            }
            
            return string.Format("{0:n1} {1}", number, suffixes[counter]);
        }

        private class FileComparisonResult
        {
            public string FileName { get; set; }
            public string Status { get; set; }
            public long TorrentSize { get; set; }
            public long LocalSize { get; set; }
            public string LocalPath { get; set; }
        }
    }
}