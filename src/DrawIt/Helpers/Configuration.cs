using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;

namespace DrawIt
{
    public delegate bool TryValueConvertor<T>(string key, out T value);

    public static class Configuration
    {
        private static Dictionary<string, string> s_settings = LoadSettings();

        private static Dictionary<string, string> LoadSettings()
        {
            var configFile = GetConfigFilePath();

            // does the config file exists?
            if (!File.Exists(configFile))
            {
                // the folder does not exist, not point in trying to load the settings file.
                return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            }

            try
            {
                var dict = FileHelpers.LoadObjectFromDisk<Dictionary<string, string>>(configFile);
                return dict;
            }
            catch
            {
                // could not load the dictionary, use a new one.
                return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            }
        }

        private static void SaveSettings()
        {
            string configFile = GetConfigFilePath();

            try
            {
                FileHelpers.SaveObjectToDisk(configFile, s_settings);
            }
            catch
            {
                // do nothing..
            }
        }

        static string GetConfigFilePath()
        {
            //compute the config file
            string localAppDataRoot = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string drawItFolder = Path.Combine(localAppDataRoot, "DrawIt");

            // does the DrawIt folder exist?
            if (!Directory.Exists(drawItFolder))
            {
                Directory.CreateDirectory(drawItFolder);
            }

            return Path.Combine(drawItFolder, "DrawIt.config");
        }

        public static string GetSetting(string key)
        {
            string value;
            if (s_settings.TryGetValue(key, out value))
            {
                return value;
            }
            return null;
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

        public static void SetSetting(string key, string value)
        {
            s_settings[key] = value;
        }

        public static void SaveToDisk()
        {
            SaveSettings();
        }
    }
}
