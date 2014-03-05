using SubsToolBox.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }

        private void OpenSubtitleFile(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".srt";
            ofd.Filter = "Subtitle file (.srt)|*.srt";

            if (ofd.ShowDialog() == true)
            {
                TxtSubtitlePath.Text = ofd.FileName;
            }
        }

        private void LaunchSync(object sender, RoutedEventArgs e)
        {
            // Test
            SyncService service = new SyncService(TxtSubtitlePath.Text, "00:00:51,320");
            service.LinearSynchronization(true);
        }
    }
}
