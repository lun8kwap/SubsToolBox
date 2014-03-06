using SubsToolBox.Service;
using System.Windows;

namespace SubsToolBoxWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BtnLaunch.IsEnabled = false;
        }

        private void OpenSubtitleFile(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".srt";
            ofd.Filter = "Subtitle file (.srt)|*.srt";

            if (ofd.ShowDialog() == true)
            {
                TxtSubtitlePath.Text = ofd.FileName;
                BtnLaunch.IsEnabled = true;
            }
            else
            {
                BtnLaunch.IsEnabled = false;
            }
        }

        private void LaunchSync(object sender, RoutedEventArgs e)
        {
            SyncService service = new SyncService(TxtSubtitlePath.Text, TxtFirstTimecode.Text);

            if (rbtLinearResync.IsChecked.Value)
            {
                service.LinearSynchronization(chkOverlapFix.IsChecked.Value);
            }

            if (rbtProgressiveResync.IsChecked.Value)
            {
                service.ProgressiveSynchronization(TxtLastTimecode.Text, chkOverlapFix.IsChecked.Value);
            }
        }
    }
}
