using Project_Mcgellan.Services;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Project_Mcgellan
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static Settings? CurrentSettings { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            CurrentSettings = SettingsService.Load() ?? new Settings();
            ThemeService.ApplyTheme(CurrentSettings.Theme);

            if (this.MainWindow is MainWindow MainWindow)
            {
                MainWindow.CurrentWidth = CurrentSettings.WindowWidth;
                MainWindow.CurrentHeight = CurrentSettings.WindowHeight;
                MainWindow.CurrentTheme = CurrentSettings.Theme;
                MainWindow.CurrentFacilityName = CurrentSettings.FacilityName;
                MainWindow.CurrentFacilityId = CurrentSettings.FacilityID;
            }
            // Theme resource selection logic can be improved if you want to apply it at runtime.
        }
    }

}
