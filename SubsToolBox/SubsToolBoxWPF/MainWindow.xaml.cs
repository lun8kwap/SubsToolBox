using Microsoft.Win32;
using SubsToolBox.Service;
using System;
using System.IO;
using System.Windows;

namespace SubsToolBoxWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string OUTPUT_PREFIX = "out_";

        public MainWindow()
        {
            InitializeComponent();
            BtnLaunch.IsEnabled = false;
        }

        private void OpenSubtitleFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".srt";
            ofd.Filter = "Subtitle file (.srt)|*.srt";

            if (ofd.ShowDialog() == true)
            {
                TxtSubtitlePath.Text = ofd.FileName;
                TxtDestinationFile.Text = GetOutputDestination(ofd.FileName);
                BtnLaunch.IsEnabled = true;
            }
            else
            {
                BtnLaunch.IsEnabled = false;
            }
        }

        private void LaunchSync(object sender, RoutedEventArgs e)
        {
            int startId;
            if (int.TryParse(TxtFirstSubtitleId.Text, out startId))
            {
                try
                {
                    SyncService service = new SyncService(TxtSubtitlePath.Text, TimeSpan.Parse(TxtFirstTimecode.Text), startId);

                    if (rbtLinearResync.IsChecked.Value)
                    {
                        service.LinearSynchronization(TxtDestinationFile.Text, chkOverlapFix.IsChecked.Value);
                    }

                    if (rbtProgressiveResync.IsChecked.Value)
                    {
                        double videoFps, subtitleFps;
                        if (double.TryParse(TxtVideoFrameRate.Text, out videoFps) && double.TryParse(TxtSubtitleFrameRate.Text, out subtitleFps))
                            service.ProgressiveSynchronization(TxtDestinationFile.Text, videoFps, subtitleFps, TimeSpan.Parse(TxtLastTimecode.Text), chkOverlapFix.IsChecked.Value);
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void OpenDestinationFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Path.GetDirectoryName(TxtDestinationFile.Text);
            sfd.DefaultExt = ".srt";
            sfd.FileName = Path.GetFileName(TxtDestinationFile.Text);
            sfd.Filter = "Subtitle file (.srt)|*.srt";

            if (sfd.ShowDialog() == true)
            {
                TxtDestinationFile.Text = sfd.FileName;
            }
        }
        
        /// <summary>
        /// Generate default output destination from input file and default prefix use
        /// </summary>
        /// <param name="inputFile">Path to input file</param>
        /// <returns>Destination file path</returns>
        private string GetOutputDestination(string inputFile){
            string output = Path.GetDirectoryName(inputFile);
            output += "\\"+OUTPUT_PREFIX;
            output += Path.GetFileName(inputFile);

            return output;
        }
    }
}
