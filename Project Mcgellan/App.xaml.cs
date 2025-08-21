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

            Current.MainWindow.Width = windowWidth;
            Current.MainWindow.Height = windowHeight;


            if(this.MainWindow is MainWindow mainWindow)
            {
                mainWindow.CurrentTheme = theme;
                mainWindow.CurrentFacilityName = facName;
                mainWindow.CurrentFacilityId = facId;
            }
            // Theme resource selection logic can be improved if you want to apply it at runtime.
        }
    }

}
