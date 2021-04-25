using System;
using MelonLoader;
using MelonLoader.ICSharpCode;
using Unity;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using BhapticsPopOne.Haptics.Loaders;
using BhapticsPopOne.Haptics.Patterns;
using Newtonsoft.Json.Linq;

namespace BhapticsPopOne.Haptics
{
    public class PatternManager
    {
        // an effects name is "{gear}/{effect name}"
        
        // The subdirectories of effects
        // this can be used to organize effects based on piece of equipment
        private static HashSet<string> Positions = new HashSet<string>(new[]
        {
            "Vest",
            "Arm",
            "Head",
            "Hand",
            "Foot"
        });

        public static float VestHeight = 0.35f;
        public static float VestCenterOffset = 0.2f;
        
        public static Dictionary<string, Effect> Effects = new Dictionary<string, Effect>();

        private static readonly Dictionary<string, uint> PoolSettings = new Dictionary<string, uint>
        {
            ["Vest/ReceiveTouch"] = 32,
            ["Vest/InitialTouch"] = 8,
            ["Vest/RecoilLevel9001_R"] = 5,
            ["Vest/RecoilLevel9001_L"] = 5,
            ["Vest/RecoilLevel3_R"] = 5,
            ["Vest/RecoilLevel3_L"] = 5,
            ["Vest/RecoilLevel2_R"] = 5,
            ["Vest/RecoilLevel2_L"] = 5,
            ["Vest/RecoilLevel1_R"] = 5,
            ["Vest/RecoilLevel1_L"] = 5,
            ["Arm/RecoilLevel9001_R"] = 5,
            ["Arm/RecoilLevel9001_L"] = 5,
            ["Arm/RecoilLevel3_R"] = 5,
            ["Arm/RecoilLevel3_L"] = 5,
            ["Arm/RecoilLevel2_R"] = 5,
            ["Arm/RecoilLevel2_L"] = 5,
            ["Arm/RecoilLevel1_R"] = 5,
            ["Arm/RecoilLevel1_L"] = 5,
        };
        
        // loads all subdirectories
        // TODO: change to recursive
        public static void LoadPatterns()
        {
            var rawHaptics = LoadInlineFile();
            if (rawHaptics == null)
            {
                MelonLogger.Error("Failed read haptics file");
                Mod.Instance.Disable();
                return;
            }

            JObject parsed;
            try
            {
                parsed = JObject.Parse(rawHaptics);
            } catch (Exception e) {
                MelonLogger.Error($"Error during parsing: {e.Message}");
                Mod.Instance.Disable();
                return;
            }
            
            ImportHaptics(parsed);
            InitializeByteEffects.Init();
        }

        private static void ImportEffect(string key, string label, string contents)
        {
            var effect = new Effect($"{key}/{label}", contents);
            Effects[effect.Name] = effect;
            
            if (PoolSettings.ContainsKey(effect.Name))
                effect.PoolSize = PoolSettings[effect.Name];
            
            if (ConfigLoader.Config.Toggles.ShowLoadedEffects)
                MelonLogger.Msg($"[Pattern Loader] Loaded [{effect.Name}]");
        }
        
        private static void ImportHaptics(JObject hapticsObj)
        {
            foreach (var section in hapticsObj)
            {
                if (!Positions.Contains(section.Key.ToString()))
                    continue;
                
                if (section.Value.Type != JTokenType.Object)
                    continue;

                var v = section.Value.Children();
                foreach (var effect in section.Value.ToObject<JObject>())
                {
                    if (effect.Value.Type != JTokenType.Object)
                        continue;

                    MelonLogger.Msg($"{section.Key} {effect.Key} {effect.Value.ToString()}");
                    //ImportEffect(section.Key, effect.Key, effect.Value.ToString());
                }
            }
        }

        private static string LoadInlineFile()
        {
            string rawHaptics = null;

            using (var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("BhapticsPopOne.haptics.json"))
            using (var streamReader = new StreamReader(stream))
            {
                rawHaptics = streamReader.ReadToEnd();
            }

            if (rawHaptics == null || rawHaptics.Length < 2)
                return null;
            
            return rawHaptics;
        }
    }
}