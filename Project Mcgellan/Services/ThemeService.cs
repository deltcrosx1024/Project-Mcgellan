using System;
using System.Windows;

namespace Project_Mcgellan.Services
{
    public static class ThemeService
    {
        public static void ApplyTheme(string themeKey)
        {
            try
            {
                // Correct Pack URI format
                var dict = new ResourceDictionary
                {
                    Source = new Uri(
                        $"pack://application:,,,/Project_Mcgellan;component/Themes/{themeKey}.xaml",
                        UriKind.Absolute)
                };

                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(dict);

                if (Application.Current.Resources.Contains(themeKey))
                {
                    Application.Current.Resources["AppBackgroundBrush"] =
                        Application.Current.Resources[themeKey];
                }
                else
                {
                    // fallback if themeKey not found
                    Application.Current.Resources["AppBackgroundBrush"] =
                        Application.Current.Resources["Default-Dark"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to apply theme '{themeKey}': {ex.Message}",
                    "Theme Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                // Optional: fallback dictionary
                Application.Current.Resources["AppBackgroundBrush"] =
                    Application.Current.Resources["Default-Dark"];
            }
        }
    }
}
