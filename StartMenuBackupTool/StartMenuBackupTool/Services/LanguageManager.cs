using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Configuration;

namespace StartMenuBackupTool.Services
{
    public class LanguageManager
    {
        private static LanguageManager? _instance;
        public static LanguageManager Instance => _instance ??= new LanguageManager();

        public event EventHandler<CultureInfo>? LanguageChanged;

        private const string LanguageKey = "Language";
        private const string DefaultLanguage = "ko-KR";

        private LanguageManager()
        {
        }

        public CultureInfo CurrentCulture => Thread.CurrentThread.CurrentUICulture;

        public void InitializeLanguage()
        {
            var savedLanguage = GetSavedLanguage();
            SetLanguage(savedLanguage);
        }

        public void SetLanguage(string cultureCode)
        {
            try
            {
                var culture = new CultureInfo(cultureCode);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

                Properties.Resources.Culture = culture;

                SaveLanguage(cultureCode);
                LanguageChanged?.Invoke(this, culture);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to set language to {cultureCode}: {ex.Message}");
                SetLanguage(DefaultLanguage);
            }
        }

        private string GetSavedLanguage()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var setting = config.AppSettings.Settings[LanguageKey];
                return setting?.Value ?? DefaultLanguage;
            }
            catch
            {
                return DefaultLanguage;
            }
        }

        private void SaveLanguage(string cultureCode)
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                
                if (config.AppSettings.Settings[LanguageKey] != null)
                {
                    config.AppSettings.Settings[LanguageKey].Value = cultureCode;
                }
                else
                {
                    config.AppSettings.Settings.Add(LanguageKey, cultureCode);
                }
                
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to save language setting: {ex.Message}");
            }
        }
    }
}