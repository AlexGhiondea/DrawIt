using System.Configuration;

namespace DrawIt
{
    public delegate bool TryValueConvertor<T>(string key, out T value);

    public static class Configuration
    {
        public static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
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

        public static void SaveSetting(string key, string value)
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (configuration.AppSettings.Settings[key] == null)
            {
                configuration.AppSettings.Settings.Add(key, value);
            }
            else
            {
                configuration.AppSettings.Settings[key].Value = value;
            }
            configuration.Save(ConfigurationSaveMode.Full, true);
        }
    }
}
