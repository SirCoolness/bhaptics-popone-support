using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using BhapticsPopOne.ConfigManager.ConfigElements;
using Il2CppSystem.Collections.Generic;
using MelonLoader;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization.NodeDeserializers;

using ConfigUpdateChoice = BhapticsPopOne.ConfigManager.UpdateGUI.ConfigUpdateChoice;

namespace BhapticsPopOne.ConfigManager
{
    public class ConfigLoader
    {
        public static Config Config { get; private set; }

        public static string DefaultConfigPath => Path.Combine(FileHelpers.RootDirectory, "settings.yml");
        public static string RememberMeSettings => Path.Combine(FileHelpers.RootDirectory, "configupdatesettings");

        public static void InitConfig()
        {
            InitConfig(DefaultConfigPath);
        }

        public static void InitConfig(string path)
        {
            string configContents;

            var exists = ConfigExists(path);

            if (exists)
            {
                configContents = System.IO.File.ReadAllText(path);
            }
            else
            {
                Config = Config.DefaultConfig;
                WriteConfig(Config, path);
                return;
            }

            bool updated = CheckIfUpdated(configContents);

            if (updated)
            {
                ConfigUpdateChoice result;
                
                if (!DefaultOptions(out result))
                    result = UpdateGUI.ShowConfigPrompt();
                
                if (result.Backup)
                    BackupConfig(configContents);
                
                if (result.Remember)
                    RememberSettings(result);
                
                Config = Config.DefaultConfig;
                WriteConfig(Config, path);
            }
            else
            {
                Config = ParseConfig(configContents);
            }
        }
        
        private static Config ParseConfig(string contents)
        {
            var deserializer = new DeserializerBuilder()
                .Build();

            return deserializer.Deserialize<Config>(contents);
        }

        // Lazy check
        private static bool CheckIfUpdated(string contents)
        {
            try
            {
                var config = ParseConfig(contents);

                if (config.Version != Config.CurrentVersion.ToString())
                    return true;
            }
            catch
            {
                return true;
            }

            return false;
        }

        public static bool ConfigExists(string path)
        {
            return File.Exists(path);
        }

        public static void WriteConfig(Config config, string path)
        {
            var serialized = SerializeConfig(config);
            System.IO.File.WriteAllText(path, serialized);
        }

        public static string SerializeConfig(Config config)
        {
            var serializer = new SerializerBuilder().Build();
            return serializer.Serialize(config);
        }

        private static void BackupConfig(string contents)
        {
            var path = FileHelpers.RootDirectory + "\\" + Path.GetFileNameWithoutExtension(DefaultConfigPath) + "-" +
                       DateTimeOffset.Now.ToUnixTimeSeconds() + ".yml";
            
            System.IO.File.WriteAllText(path, contents);
        }

        private static bool DefaultOptions(out ConfigUpdateChoice updateChoice)
        {
            if (!File.Exists(RememberMeSettings))
            {
                updateChoice = new ConfigUpdateChoice();
                return false;
            }
            
            string contents = System.IO.File.ReadAllText(RememberMeSettings);
            
            updateChoice = new ConfigUpdateChoice
            {
                Remember = false,
                Backup = contents != "0"
            };
            
            return true;
        }

        private static void RememberSettings(ConfigUpdateChoice settings)
        {
            System.IO.File.WriteAllText(RememberMeSettings, settings.Backup ? "1" : "0");
        }
    }
}