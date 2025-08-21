using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace Project_Mcgellan
{
    public class SettingsUpdater
    {
        private readonly string _settingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.xml");

        public void UpdateWindowSize(string windowResolution)
        {
            try
            {
                var parts = windowResolution.Split('x');
                if (parts.Length != 2)
                {
                    Console.WriteLine("Invalid window resolution format.");
                    return;
                }

                XDocument doc = XDocument.Load(_settingsFilePath);

                XElement? windowWidthElement = doc.Descendants("WindowWidth").FirstOrDefault();
                XElement? windowHeightElement = doc.Descendants("WindowHeight").FirstOrDefault();

                if (windowWidthElement != null && windowHeightElement != null)
                {
                    windowWidthElement.Value = parts[0];
                    windowHeightElement.Value = parts[1];
                    doc.Save(_settingsFilePath);
                }
                else
                {
                    MessageBox.Show("WindowWidth or WindowHeight element not found in the settings file.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the settings: {ex.Message}");
            }
        }

        public void UpdateTheme(string theme)
        {
            try
            {
                XDocument doc = XDocument.Load(_settingsFilePath);
                XElement? themeElement = doc.Descendants("BackgroundTheme").FirstOrDefault();

                if (themeElement != null)
                {
                    themeElement.Value = theme;
                    doc.Save(_settingsFilePath);
                }
                else
                {
                    MessageBox.Show("BackgroundTheme element not found in the settings file.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the theme: {ex.Message}");
            }
        }

        public void UpdateFacility(string facname, string facid)
        {
            try
            {
                XDocument doc = XDocument.Load(_settingsFilePath);

                XElement? facilityName = doc.Descendants("FacilityName").FirstOrDefault();
                XElement? facilityId = doc.Descendants("FacilityId").FirstOrDefault();

                if (facilityName != null && facilityId != null)
                {
                    facilityName.Value = facname;
                    facilityId.Value = facid;
                    doc.Save(_settingsFilePath);
                }
                else
                {
                    MessageBox.Show("FacilityName or FacilityId element not found in the settings file.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the facility: {ex.Message}");
            }
        }
    }
}