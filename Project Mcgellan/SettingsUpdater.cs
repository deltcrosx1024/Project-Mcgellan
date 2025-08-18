using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class SettingsUpdater
{
    private readonly string _settingsFilePath = "Settings.xml";

    public void UpdateWindowSize(string WindowResolution)
    {
        try
        {
            XDocument doc = XDocument.Load(_settingsFilePath);

            XElement windowWidthElement = doc.Descendants("WindowWidth").FirstOrDefault();
            XElement windowHeightElement = doc.Descendants("WindowHeight").FirstOrDefault();

            if (windowWidthElement != null && windowHeightElement != null)
            {
                windowWidthElement.Value = (string)XElement.Parse(WindowResolution.Split('x')[0]);
                windowHeightElement.Value = (string)XElement.Parse(WindowResolution.Split('x')[1]);

                doc.Save(_settingsFilePath);

                Console.WriteLine("Window size updated successfully.");
            }

            else
            {
                Console.WriteLine("WindowWidth or WindowHeight element not found in the settings file.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while updating the settings: {ex.Message}");
        }
    }

    public void UpdateTheme(string theme)
    {
        try
        {
            XDocument doc = XDocument.Load(_settingsFilePath);

            XElement themeElement = doc.Descendants("BackgroundTheme").FirstOrDefault();

            if (themeElement != null)
            {
                themeElement.Value = (string)XElement.Parse(theme);

                doc.Save(_settingsFilePath);

                Console.WriteLine("Theme updated successfully.");
            }
            else
            {
                Console.WriteLine("BackgroundTheme element not found in the settings file.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while updating the theme: {ex.Message}");
        }
    }

    public void UpdateFacility(string facname, string facid)
    {
        try
        {
            XDocument doc = XDocument.Load(_settingsFilePath);

            XElement FacilityName = doc.Descendants("FacilityName").FirstOrDefault();
            XElement FacilityID = doc.Descendants("FacilityId").FirstOrDefault();

            if (FacilityName != null && FacilityID != null)
            {
                FacilityName.Value = (string)XElement.Parse(facname);
                FacilityID.Value = (string)XElement.Parse(facid);
            }
            else
            {
                Console.WriteLine("FacilityName or FacilityId element not found in the settings file.");
            }

            doc.Save(_settingsFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while updating the facility: {ex.Message}");

        }

    }
}
