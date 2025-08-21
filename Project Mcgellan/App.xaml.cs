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

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SettingsLoad settingsLoad = new();

            string windowSize = settingsLoad.GetWindowSize();
            string theme = settingsLoad.GetTheme();
            string facName = settingsLoad.GetFacName();
            string facId = settingsLoad.GetFacId().ToString();

            int windowWidth = int.Parse(windowSize.Split('x')[0]);
            int windowHeight = int.Parse(windowSize.Split('x')[1]);

            if(this.MainWindow is MainWindow MainWindow)
            {
                MainWindow.CurrentWidth = windowWidth;
                MainWindow.CurrentHeight = windowHeight;
                MainWindow.CurrentTheme = theme;
                MainWindow.CurrentFacilityName = facName;
                MainWindow.CurrentFacilityId = facId;
            }
            // Theme resource selection logic can be improved if you want to apply it at runtime.
        }
    }

}
