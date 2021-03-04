using System;
using Bhaptics.Tact;
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

namespace BhapticsPopOne.Haptics
{
    public class PatternManager
    {
        // an effects name is "{gear}/{effect name}"
        
        // The subdirectories of effects
        // this can be used to organize effects based on piece of equipment
        private static HashSet<string> subdirectories = new HashSet<string>(new[]
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
                MelonLogger.LogError("Failed read haptics file");
                Mod.Instance.Disable();
                return;
            }

            var parsedHaptics = ParseHaptics(rawHaptics);
            if (parsedHaptics == null)
            {
                MelonLogger.LogError("Failed to parse contents");
                Mod.Instance.Disable();
                return;
            }
            
            ImportHaptics(parsedHaptics);
            InitializeByteEffects.Init();
        }

        private static void ImportEffect(string key, string label, JSONObject contents)
        {
            var effect = new Effect($"{key}/{label}", contents);
            Effects[effect.Name] = effect;
            
            if (PoolSettings.ContainsKey(effect.Name))
                effect.PoolSize = PoolSettings[effect.Name];
            
            if (ConfigLoader.Config.Toggles.ShowLoadedEffects)
                MelonLogger.Log($"[Pattern Loader] Loaded [{effect.Name}]");
        }
        
        private static void ImportHaptics(JSONObject hapticsObj)
        {
            foreach (var section in hapticsObj)
            {
                if (!subdirectories.Contains(section.Key))
                    continue;
                
                if (!section.Value.IsObject)
                    continue;

                foreach (var effect in section.Value)
                {
                    if (!effect.Value.IsObject)
                        continue;
                    
                    ImportEffect(section.Key, effect.Key, effect.Value.AsObject);
                }
            }
        }
        
        private static JSONObject ParseHaptics(string rawData)
        {
            return JSONObject.Parse(rawData).AsObject;
        }

        private static string LoadInlineFile()
        {
            string rawHaptics = null;

            var resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach (var resource in resources)
            {
                MelonLogger.Log(resource);
            }

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