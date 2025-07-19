using System;
using System.Windows.Forms;
using TorrentFileComparer;

namespace Torrent_File_Comparer
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}