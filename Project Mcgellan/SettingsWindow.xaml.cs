using System;
using System.Collections.Generic;
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
        public SettingsWindow()
        {
            InitializeComponent();
        }

        public void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedResolution = (ResolutionComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "1280x720";
                var selectedTheme = (ThemeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Default-Dark";
                string facname = FacilityNameInput.Text;
                string facid = FacilityIdInput.Text;

                SettingsUpdater updater = new SettingsUpdater();
                updater.UpdateWindowSize(selectedResolution);
                updater.UpdateTheme(selectedTheme);
                updater.UpdateFacility(facname, facid);

                MessageBox.Show("Settings have been applied successfully.", "Settings Updated", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while applying settings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Test_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XElement doc = XElement.Load("Settings.xml");
                XElement width = doc.Descendants("WindowWidth").FirstOrDefault();
                XElement height = doc.Descendants("WindowHeight").FirstOrDefault();
                string resolution = $"{width?.Value}x{height?.Value}";
                MessageBox.Show(resolution);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading settings: {ex.Message}");
            }
        }
    }
}