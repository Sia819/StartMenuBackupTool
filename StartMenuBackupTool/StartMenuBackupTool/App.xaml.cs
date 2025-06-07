using System.Configuration;
using System.Data;
using System.Windows;
using StartMenuBackupTool.Services;

namespace StartMenuBackupTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // Initialize language settings on application startup
            LanguageManager.Instance.InitializeLanguage();
        }
    }

}
