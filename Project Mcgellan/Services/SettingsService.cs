using Project_Mcgellan;
using System;
using System.IO;
using System.Text.Json;

namespace Project_Mcgellan.Services
{
    public static class SettingsService
    {
        private static readonly string appDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "ProjectMcgellan");
        private static readonly string settingsPath = Path.Combine(appDir, "settings.json");

        public static Settings Load()
        {
            try
            {
                if (!Directory.Exists(appDir))
                    Directory.CreateDirectory(appDir);

                if (!File.Exists(settingsPath))
                {
                    var defaultSettings = new Settings();
                    Save(defaultSettings);
                    return defaultSettings;
                }

                string json = File.ReadAllText(settingsPath);
                return JsonSerializer.Deserialize<Settings>(json) ?? new Settings();
            }
            catch
            {
                return new Settings(); // fallback on error
            }
        }

        public static void Save(Settings settings)
        {
            if (!Directory.Exists(appDir))
                Directory.CreateDirectory(appDir);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(settings, options);

            // atomic write
            string tmpFile = settingsPath + ".tmp";
            File.WriteAllText(tmpFile, json);
            File.Copy(tmpFile, settingsPath, overwrite: true);
            File.Delete(tmpFile);
        }
    }
}
