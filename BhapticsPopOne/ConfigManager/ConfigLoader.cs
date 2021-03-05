using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BhapticsPopOne.ConfigManager.ConfigElements;
using Il2CppSystem.Collections.Generic;
using MelonLoader;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization.NodeDeserializers;

namespace BhapticsPopOne.ConfigManager
{
    public class ConfigLoader
    {
        public static Config Config { get; private set; }

        public static string DefaultConfigPath => FileHelpers.RootDirectory + @"\settings.yml";
        public static string RememberMeSettings => FileHelpers.RootDirectory + @"\configupdatesettings";

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
                    result = ShowConfigPrompt();
                
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

        private static ConfigUpdateChoice ShowConfigPrompt()
        {
            var form = new System.Windows.Forms.Form
            {
                Width = 240,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "bHaptics mod",
                StartPosition = FormStartPosition.CenterScreen
            };

            Label label = new Label
            {
                Text = "Config needs to be regenerated.\nWould you like to back it up first?",
                Top = 20,
                Left = 30,
                Width = 200,
                Height = 30
            };
            
            CheckBox checkBox = new CheckBox
            {
                Text = "remember my answer",
                Top = 50,
                Left = 30,
                Width = 160
            };
            
            Button confirmation = new Button
            {
                Text = "Overwrite",
                DialogResult = DialogResult.OK,
                Top = 80,
                Left = 30
            };
            confirmation.Click += (sender, e) => { form.Close(); };
            
            Button backup = new Button
            {
                Text = "Backup",
                DialogResult = DialogResult.Yes,
                Top = 110,
                Left = 30
            };
            confirmation.Click += (sender, e) => { form.Close(); };

            form.Controls.Add(label);
            form.Controls.Add(checkBox);
            form.Controls.Add(backup);
            form.Controls.Add(confirmation);
            
            form.AcceptButton = confirmation;
            form.CancelButton = backup;
            
            var res = form.ShowDialog();

            return new ConfigUpdateChoice
            {
                Backup = res == DialogResult.Yes,
                Remember = checkBox.Checked
            };
        }

        private struct ConfigUpdateChoice
        {
            public bool Remember;
            public bool Backup;
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