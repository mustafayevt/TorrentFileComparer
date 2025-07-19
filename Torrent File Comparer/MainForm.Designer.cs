using System;
using System.Drawing;
using System.Windows.Forms;

namespace TorrentFileComparer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTorrent = new System.Windows.Forms.Label();
            this.txtTorrentPath = new System.Windows.Forms.TextBox();
            this.btnBrowseTorrent = new System.Windows.Forms.Button();
            this.lblFolder = new System.Windows.Forms.Label();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.btnCompare = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTorrent
            // 
            this.lblTorrent.Location = new System.Drawing.Point(20, 20);
            this.lblTorrent.Name = "lblTorrent";
            this.lblTorrent.Size = new System.Drawing.Size(80, 23);
            this.lblTorrent.TabIndex = 0;
            this.lblTorrent.Text = "Torrent File:";
            this.lblTorrent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTorrentPath
            // 
            this.txtTorrentPath.Location = new System.Drawing.Point(110, 20);
            this.txtTorrentPath.Name = "txtTorrentPath";
            this.txtTorrentPath.ReadOnly = true;
            this.txtTorrentPath.Size = new System.Drawing.Size(700, 20);
            this.txtTorrentPath.TabIndex = 1;
            // 
            // btnBrowseTorrent
            // 
            this.btnBrowseTorrent.Location = new System.Drawing.Point(820, 20);
            this.btnBrowseTorrent.Name = "btnBrowseTorrent";
            this.btnBrowseTorrent.Size = new System.Drawing.Size(80, 23);
            this.btnBrowseTorrent.TabIndex = 2;
            this.btnBrowseTorrent.Text = "Browse";
            this.btnBrowseTorrent.UseVisualStyleBackColor = true;
            this.btnBrowseTorrent.Click += new System.EventHandler(this.BtnBrowseTorrent_Click);
            // 
            // lblFolder
            // 
            this.lblFolder.Location = new System.Drawing.Point(20, 60);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(100, 23);
            this.lblFolder.TabIndex = 3;
            this.lblFolder.Text = "Compare Folder:";
            this.lblFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(130, 60);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.ReadOnly = true;
            this.txtFolderPath.Size = new System.Drawing.Size(680, 20);
            this.txtFolderPath.TabIndex = 4;
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Location = new System.Drawing.Point(820, 60);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(80, 23);
            this.btnBrowseFolder.TabIndex = 5;
            this.btnBrowseFolder.Text = "Browse";
            this.btnBrowseFolder.UseVisualStyleBackColor = true;
            this.btnBrowseFolder.Click += new System.EventHandler(this.BtnBrowseFolder_Click);
            // 
            // btnCompare
            // 
            this.btnCompare.Enabled = false;
            this.btnCompare.Location = new System.Drawing.Point(360, 100);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(100, 30);
            this.btnCompare.TabIndex = 6;
            this.btnCompare.Text = "Compare Files";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.BtnCompare_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(470, 100);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Delete Selected";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Enabled = false;
            this.btnSelectAll.Location = new System.Drawing.Point(580, 100);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(80, 30);
            this.btnSelectAll.TabIndex = 8;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.BtnSelectAll_Click);
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Enabled = false;
            this.btnDeselectAll.Location = new System.Drawing.Point(670, 100);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(80, 30);
            this.btnDeselectAll.TabIndex = 9;
            this.btnDeselectAll.Text = "Deselect All";
            this.btnDeselectAll.UseVisualStyleBackColor = true;
            this.btnDeselectAll.Click += new System.EventHandler(this.BtnDeselectAll_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(20, 140);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(880, 23);
            this.progressBar.TabIndex = 10;
            this.progressBar.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(20, 170);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(880, 23);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResults.Location = new System.Drawing.Point(20, 200);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = false;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.Size = new System.Drawing.Size(960, 480);
            this.dgvResults.TabIndex = 12;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnDeselectAll);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.btnBrowseFolder);
            this.Controls.Add(this.txtFolderPath);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.btnBrowseTorrent);
            this.Controls.Add(this.txtTorrentPath);
            this.Controls.Add(this.lblTorrent);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Torrent File Comparer";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            // Add columns to DataGridView
            var checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.HeaderText = "Select";
            checkColumn.Name = "Select";
            checkColumn.Width = 50;
            this.dgvResults.Columns.Add(checkColumn);
            
            this.dgvResults.Columns.Add("FileName", "File Name");
            this.dgvResults.Columns.Add("Status", "Status");
            this.dgvResults.Columns.Add("TorrentSize", "Torrent Size");
            this.dgvResults.Columns.Add("LocalSize", "Local Size");
            this.dgvResults.Columns.Add("Path", "Local Path");
        }

        #endregion

        private System.Windows.Forms.Label lblTorrent;
        private System.Windows.Forms.TextBox txtTorrentPath;
        private System.Windows.Forms.Button btnBrowseTorrent;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnDeselectAll;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DataGridView dgvResults;
    }
}