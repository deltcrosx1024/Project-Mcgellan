using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Project_Mcgellan
{
    public class SettingsLoad
    {
        private readonly string _settingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.xml");

        public string GetWindowSize()
        {
            try
            {
                XDocument doc = XDocument.Load(_settingsFilePath);
                XElement? windowWidthElement = doc.Descendants("WindowWidth").FirstOrDefault();
                XElement? windowHeightElement = doc.Descendants("WindowHeight").FirstOrDefault();
                if (windowWidthElement != null && windowHeightElement != null)
                {
                    return $"{windowWidthElement.Value}x{windowHeightElement.Value}";
                }
                else
                {
                    Console.WriteLine("WindowWidth or WindowHeight element not found in the settings file.");
                    return "800x600";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading the settings: {ex.Message}");
                return "800x600";
            }
        }

        public string GetTheme()
        {
            try
            {
                XDocument doc = XDocument.Load(_settingsFilePath);
                XElement? themeElement = doc.Descendants("BackgroundTheme").FirstOrDefault();
                if (themeElement != null)
                {
                    return themeElement.Value;
                }
                else
                {
                    Console.WriteLine("BackgroundTheme element not found in the settings file.");
                    return "Light";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading the settings: {ex.Message}");
                return "Light";
            }
        }

        public string GetFacName()
        {
            try
            {
                XDocument doc = XDocument.Load(_settingsFilePath);
                XElement? facNameElement = doc.Descendants("FacilityName").FirstOrDefault();
                if (facNameElement != null)
                {
                    return facNameElement.Value;
                }
                else
                {
                    Console.WriteLine("FacilityName element not found in the settings file.");
                    return "Default Faculty";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading the settings: {ex.Message}");
                return "Default Faculty";
            }
        }

        public int GetFacId()
        {
            try
            {
                XDocument doc = XDocument.Load(_settingsFilePath);
                XElement? facIdElement = doc.Descendants("FacilityId").FirstOrDefault();
                if (facIdElement != null && int.TryParse(facIdElement.Value, out int id))
                {
                    return id;
                }
                else
                {
                    Console.WriteLine("FacilityId element not found or invalid in the settings file.");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading the settings: {ex.Message}");
                return 0;
            }
        }
    }
}