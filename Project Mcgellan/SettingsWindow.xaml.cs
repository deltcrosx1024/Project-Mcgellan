using Project_Mcgellan.Services;
using Project_Mcgellan.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Project_Mcgellan
{
    public partial class SettingsWindow : Window
    {

        private Settings _SettingsCopy = SettingsService.Load();
        private SettingsViewModel _vm;

        public SettingsWindow(Settings CurrentSettings)
        {
            InitializeComponent();
            _vm = new SettingsViewModel(CurrentSettings);
            DataContext = _vm;
        }

        public void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.HasErrors)
            {
                MessageBox.Show("Please fix validation errors before saving.",
                                "Invalid Settings",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            var newSettings = _vm.ToSettings();

            SettingsService.Save(newSettings);
            App.CurrentSettings = newSettings;
            ThemeService.ApplyTheme(newSettings.Theme);

            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                mainWin.Width = newSettings.WindowWidth;
                mainWin.Height = newSettings.WindowHeight;
            }

            this.DialogResult = true;
        }

        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RestoreDefaults_Click(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show("Are you sure you want to restore default settings?",
                                          "Confirm Reset",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);

            if (confirm == MessageBoxResult.Yes)
            {
                var defaultSettings = new Settings(); // defaults from Settings.cs
                _vm.Resolution = $"{defaultSettings.WindowWidth}x{defaultSettings.WindowHeight}";
                _vm.Theme = defaultSettings.Theme;
                _vm.FacilityName = defaultSettings.FacilityName;
                _vm.FacilityId = defaultSettings.FacilityID;
            }
        }


    }
}