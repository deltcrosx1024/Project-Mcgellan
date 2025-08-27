using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_Mcgellan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly Settings _settings = App.CurrentSettings ?? new Settings();

        public int? CurrentWidth { get; set; }
        public int? CurrentHeight { get; set; }
        public string? CurrentTheme { get; set; }
        public string? CurrentFacilityName { get; set; }
        public string? CurrentFacilityId { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            this.Width = CurrentWidth ?? 1280;
            this.Height = CurrentHeight ?? 720;

            this.Loaded += MainWindow_Loaded;
        }

        private static void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //code goes here to initialize the main window after it has loaded
        }

        public void SettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(App.CurrentSettings == null)
            {
                MessageBox.Show("Settings not loaded. Please check the application configuration.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SettingsWindow settingsWindow = new(App.CurrentSettings)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            settingsWindow.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        // Close Button Logic
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Maximize/Restore Button Logic
        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        // Minimize Button Logic
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

    }
}