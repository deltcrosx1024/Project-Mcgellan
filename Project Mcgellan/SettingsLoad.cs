using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class SettingsLoad
{
    private readonly string _settingsFilePath = "Settings.xml";
    public string GetWindowSize()
    {
        try
        {
            XDocument doc = XDocument.Load(_settingsFilePath);
            XElement windowWidthElement = doc.Descendants("WindowWidth").FirstOrDefault();
            XElement windowHeightElement = doc.Descendants("WindowHeight").FirstOrDefault();
            if (windowWidthElement != null && windowHeightElement != null)
            {
                return $"{windowWidthElement.Value}x{windowHeightElement.Value}";
            }
            else
            {
                Console.WriteLine("WindowWidth or WindowHeight element not found in the settings file.");
                return "800x600"; // Default size
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading the settings: {ex.Message}");
            return "800x600"; // Default size
        }
    }
    public string GetTheme()
    {
        try
        {
            XDocument doc = XDocument.Load(_settingsFilePath);
            XElement themeElement = doc.Descendants("BackgroundTheme").FirstOrDefault();
            if (themeElement != null)
            {
                return themeElement.Value;
            }
            else
            {
                Console.WriteLine("BackgroundTheme element not found in the settings file.");
                return "Light"; // Default theme
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading the settings: {ex.Message}");
            return "Light"; // Default theme
        }
    }

    public string GetFacName()
    {
        try
        {
            XDocument doc = XDocument.Load(_settingsFilePath);
            XElement facNameElement = doc.Descendants("FacilityName").FirstOrDefault();
            if (facNameElement != null)
            {
                return facNameElement.Value;
            }
            else
            {
                Console.WriteLine("FacName element not found in the settings file.");
                return "Default Faculty"; // Default faculty name
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading the settings: {ex.Message}");
            return "Default Faculty"; // Default faculty name
        }
    }

    public int GetFacId()
    {
        try
        {
            XDocument doc = XDocument.Load(_settingsFilePath);
            XElement facIdElement = doc.Descendants("FacilityId").FirstOrDefault();
            if (facIdElement != null)
            {
                return int.Parse(facIdElement.Value);
            }
            else
            {
                Console.WriteLine("FacId element not found in the settings file.");
                return 0; // Default faculty ID
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading the settings: {ex.Message}");
            return 0; // Default faculty ID
        }
    }
}
