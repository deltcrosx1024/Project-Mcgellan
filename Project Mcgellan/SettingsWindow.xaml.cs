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
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
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
                // Apply settings logic here
                ComboBoxItem SelectedResolution = (ComboBoxItem)ResolutionComboBox.SelectedItem;
                ComboBoxItem SelectedTheme = (ComboBoxItem)ThemeComboBox.SelectedItem;

                String facname = FacilityNameInput.Text;
                String facid = FacilityIdInput.Text;
                String FacLocInp = FacilityLocated.Text;

                SettingsUpdater updater = new SettingsUpdater();

                String WindowResolution = SelectedResolution.ToString();

                updater.UpdateWindowSize(WindowResolution);

                updater.UpdateTheme(SelectedTheme.ToString());

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
            //code goes here
            String _SettingsPath = "Settings.xml";
            XElement doc = XElement.Load(_SettingsPath);

            XElement width = doc.Descendants("WindowWidth").FirstOrDefault() as XElement;
            XElement height = doc.Descendants("WindowHeight").FirstOrDefault() as XElement;

            String resolution = $"{width.ToString()}x{height.ToString()}";

            MessageBox.Show(resolution);

        }
    }
}