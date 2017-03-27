using System.Configuration;

namespace DrawIt
{
    public delegate bool TryValueConvertor<T>(string key, out T value);

    public static class Configuration
    {
        private static System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public static string GetSetting(string key)
        {
            //var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            return configuration.AppSettings.Settings[key]?.Value;
        }

        public static T GetSettingOrDefault<T>(string key, TryValueConvertor<T> convertor, T defaultValue)
        {
            T value;
            if (!convertor(GetSetting(key), out value))
            {
                return defaultValue;
            }
            return value;
        }

        public static void SetSettingIfNotEmpty(string key, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                SetSetting(key, value);
        }

        public static void SetSetting(string key, string value)
        {
            if (configuration.AppSettings.Settings[key] == null)
            {
                configuration.AppSettings.Settings.Add(key, value);
            }
            else
            {
                configuration.AppSettings.Settings[key].Value = value;
            }
        }

        public static void SaveToDisk()
        {
            configuration.Save(ConfigurationSaveMode.Full, true);
        }
    }
}
