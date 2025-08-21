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
        readonly SettingsLoad settingsLoad = new();
        readonly SettingsUpdater settingsUpdater = new();

        public string? CurrentTheme { get; set; }
        public string? CurrentFacilityName { get; set; }
        public string? CurrentFacilityId { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            string theme = settingsLoad.GetTheme();
            CurrentTheme = theme;
            if (theme == "Default-Light")
            {
                this.Resources.MergedDictionaries.Clear();
                this.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("Themes/LightTheme.xaml", UriKind.Relative) });
            }
            else if (theme == "Default-Dark")
            {
                this.Resources.MergedDictionaries.Clear();
                this.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative) });
            }
            else if (theme == "System-Default")
            {
                this.Resources.MergedDictionaries.Clear();
                this.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("Themes/SystemDefaultTheme.xaml", UriKind.Relative) });
            }
            else if (theme == "Custom-Theme")
            {
                this.Resources.MergedDictionaries.Clear();
                this.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("Themes/CustomTheme.xaml", UriKind.Relative) });
            }
            else
            {
                MessageBox.Show("Invalid theme selected. Defaulting to Light theme.");
                this.Resources.MergedDictionaries.Clear();
                this.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("Themes/LightTheme.xaml", UriKind.Relative) });
            }
        }

        public void SettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new();
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
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        // Minimize Button Logic
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}