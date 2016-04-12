using System.Configuration;

namespace Ignite.Rules
{
    public class SettingsLoader
    {
        public static string LoadSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}