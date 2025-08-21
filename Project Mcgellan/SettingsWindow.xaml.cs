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
    public partial class SettingsWindow : Window, INotifyPropertyChanged
    {
        private bool _isApplyEnabled;
        public bool IsApplyEnabled
        {
            get => _isApplyEnabled;
            set
            {
                _isApplyEnabled = value;
                OnPropertyChanged(nameof(IsApplyEnabled));
            }
        }

        public SettingsWindow()
        {
            InitializeComponent();
            DataContext = this;
            LoadSettingsFromXml();
            AttachChangeHandlers();
        }

        private void LoadSettingsFromXml()
        {
            try
            {
                if (!File.Exists("Settings.xml")) return;

                XElement doc = XElement.Load("Settings.xml");
                string resolution = $"{doc.Element("WindowWidth")?.Value}x{doc.Element("WindowHeight")?.Value}";
                string theme = doc.Element("Theme")?.Value ?? "Default-Dark";
                string facilityName = doc.Element("FacilityName")?.Value ?? string.Empty;
                string facilityId = doc.Element("FacilityId")?.Value ?? string.Empty;

                // Set ComboBox and TextBox values
                ResolutionComboBox.SelectedItem = ResolutionComboBox.Items.Cast<ComboBoxItem>()
                    .FirstOrDefault(item => item.Content.ToString() == resolution);
                ThemeComboBox.SelectedItem = ThemeComboBox.Items.Cast<ComboBoxItem>()
                    .FirstOrDefault(item => item.Content.ToString() == theme);
                FacilityNameInput.Text = facilityName;
                FacilityIdInput.Text = facilityId;

                IsApplyEnabled = false; // Disable Apply button initially
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading settings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AttachChangeHandlers()
        {
            ResolutionComboBox.SelectionChanged += (s, e) => IsApplyEnabled = true;
            ThemeComboBox.SelectionChanged += (s, e) => IsApplyEnabled = true;
            FacilityNameInput.TextChanged += (s, e) => IsApplyEnabled = true;
            FacilityIdInput.TextChanged += (s, e) => IsApplyEnabled = true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedResolution = (ResolutionComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "1280x720";
                var selectedTheme = (ThemeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Default-Dark";
                string facname = FacilityNameInput.Text;
                string facid = FacilityIdInput.Text;

                SettingsUpdater updater = new();
                updater.UpdateWindowSize(selectedResolution);
                updater.UpdateTheme(selectedTheme);
                updater.UpdateFacility(facname, facid);

                IsApplyEnabled = false; // Disable Apply button after applying settings

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
                XElement? width = doc.Descendants("WindowWidth").FirstOrDefault();
                XElement? height = doc.Descendants("WindowHeight").FirstOrDefault();
                string resolution = $"{width?.Value}x{height?.Value}";
                MessageBox.Show(resolution);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading settings: {ex.Message}");
            }
        }

        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}