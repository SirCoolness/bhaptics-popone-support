using System;
using Bhaptics.Tact;
using MelonLoader;
using MelonLoader.ICSharpCode;
using Unity;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using BhapticsPopOne.Haptics.Loaders;
using BhapticsPopOne.Haptics.Patterns;

namespace BhapticsPopOne.Haptics
{
    // TODO: NO
    public class PatternManager
    {
        public static string RootDirectory => FileHelpers.RootDirectory + @"\Effects";
        // an effects name is "{gear}/{effect name}"
        
        // The subdirectories of effects
        // this can be used to organize effects based on piece of equipment
        public static string[] subdirectories = new[]
        {
            "Vest",
            "Arm",
            "Head",
            "Hand",
            "Foot"
        };

        public static float VestHeight = 0.35f;
        public static float VestCenterOffset = 0.2f;
        
        public static Dictionary<string, Effect> Effects = new Dictionary<string, Effect>();

        public static readonly Dictionary<string, uint> PoolSettings = new Dictionary<string, uint>
        {
            ["Vest/ReceiveTouch"] = 32,
            ["Vest/InitialTouch"] = 8,
            ["Vest/RecoilLevel9001_R"] = 4,
            ["Vest/RecoilLevel9001_L"] = 4,
            ["Vest/RecoilLevel3_R"] = 4,
            ["Vest/RecoilLevel3_L"] = 4,
            ["Vest/RecoilLevel2_R"] = 4,
            ["Vest/RecoilLevel2_L"] = 4,
            ["Vest/RecoilLevel1_R"] = 4,
            ["Vest/RecoilLevel1_L"] = 4,
        };
        
        // loads all subdirectories
        // TODO: change to recursive
        public static void LoadPatterns()
        {
            foreach (var subdirectory in subdirectories)
            {
                LoadSubDirectory(subdirectory);
            }
            
            InitializeByteEffects.Init();
        }
        
        // loads a set of patterns in a subdirectory
        // prefixes them with that subdirectory
        public static void LoadSubDirectory(string subdirectory)
        {
            string baseDir = RootDirectory + $"\\{subdirectory}";
            var files = Directory.GetFiles(baseDir);

            foreach (var file in files)
            {
                var label = Path.GetFileNameWithoutExtension(file);

                var effect = new Effect($"{subdirectory}/{label}", file);
                Effects[effect.Name] = effect;

                if (PoolSettings.ContainsKey(effect.Name))
                    effect.PoolSize = PoolSettings[effect.Name];

                Mod.Instance.Haptics.Player.Register(effect.Name, effect.Contents);

                if (ConfigLoader.Config.ShowLoadedEffects)
                    MelonLogger.Log($"[Pattern Loader] Loaded [{effect.Name}]");
            }
        }
    }
}