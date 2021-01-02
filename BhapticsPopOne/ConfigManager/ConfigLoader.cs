using System;
using System.IO;
using System.Runtime.InteropServices;
using BhapticsPopOne.ConfigManager.ConfigElements;
using MelonLoader;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization.NodeDeserializers;

namespace BhapticsPopOne.ConfigManager
{
    public class ConfigLoader
    {
        private static Config _config;
        public static Config Config => _config;

        public static string DefaultConfigPath => FileHelpers.RootDirectory + @"\settings.yml";

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
                _config = Config.DefaultConfig;
                WriteConfig(_config, path);
                return;
            }

            bool updated;
            _config = ParseConfig(configContents, out updated);

            if (updated)
            {
                MelonLogger.LogWarning($"!!!CONFIG UPDATED!!! Please verify your settings ({path}).");
                _config = Config.DefaultConfig;
                WriteConfig(_config, path);
                return;
            }
        }

        public static Config ParseConfig(string contents)
        {
            bool contentsUpdated;

            return ParseConfig(contents, out contentsUpdated);
        }

        public static Config ParseConfig(string contents, out bool contentsUpdated)
        {
            contentsUpdated = false;
            
            var input = new StringReader(contents);

            var deserializer = new DeserializerBuilder()
                .Build();
            
            var config = deserializer.Deserialize<Config>(input);
            contentsUpdated = config.Version != Config.CurrentVersion.ToString();
            
            return config;
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
    }
}