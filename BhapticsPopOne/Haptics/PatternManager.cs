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

        public static void EatBanana(BuffState state)
        { 
            if (state != BuffState.Consumed)
                return;
            
            Mod.Instance.Haptics.Player.SubmitRegistered("Vest/ConsumeItem");
            Mod.Instance.Haptics.Player.SubmitRegistered("Vest/BananaHeal");
        }

        public static void LowHealthHeartbeat()
        {
            Mod.Instance.Haptics.Player.SubmitRegistered("Vest/HeartbeatMultiple");
        }
        
        public static void EnteringPod()
        {
            if (!Mod.Instance.Haptics.Player.IsPlaying("Vest/EnteringPod"))
            {
                Mod.Instance.Haptics.Player.SubmitRegistered("Vest/EnteringPod");
            }
            
        }
        public static void LaunchingPod()
        {
            if (!Mod.Instance.Haptics.Player.IsPlaying("Vest/LaunchingPod"))
            {
                Mod.Instance.Haptics.Player.SubmitRegistered("Vest/LaunchingPod");
            }
            
            if (!Mod.Instance.Haptics.Player.IsPlaying("Foot/LaunchingPod"))
            {
                Mod.Instance.Haptics.Player.SubmitRegistered("Foot/LaunchingPod");
            }
        }

        public static void DuringPod()
        {
            if (!Mod.Instance.Haptics.Player.IsPlaying("Vest/DuringPod"))
            {
                Mod.Instance.Haptics.Player.SubmitRegistered("Vest/DuringPod");
            }
            
            if (!Mod.Instance.Haptics.Player.IsPlaying("Foot/DuringPod"))
            {
                Mod.Instance.Haptics.Player.SubmitRegistered("Foot/DuringPod");
            }
        }
        
        public static void RubbingDefib()
        {
            if (!Mod.Instance.Haptics.Player.IsPlaying("Arm/RubbingDefib_B"))
            {
                Mod.Instance.Haptics.Player.SubmitRegistered("Arm/RubbingDefib_B");
            }

            if (!Mod.Instance.Haptics.Player.IsPlaying("Vest/RubbingDefib"))
            {
                Mod.Instance.Haptics.Player.SubmitRegistered("Vest/RubbingDefib");
            }
        }

        public static void ChargedDefib()
        {
            if (!Mod.Instance.Haptics.Player.IsPlaying("Arm/ChargedDefib_B"))
            {
                Mod.Instance.Haptics.Player.SubmitRegistered("Arm/ChargedDefib_B");
            }

            if (!Mod.Instance.Haptics.Player.IsPlaying("Vest/ChargedDefib"))
            {
                Mod.Instance.Haptics.Player.SubmitRegistered("Vest/ChargedDefib");
            }
        }

        public static void Climbing(Handedness value)
        {
            if(value == Handedness.Left)
            {
                Mod.Instance.Haptics.Player.SubmitRegistered("Arm/Climbing_L");
                Mod.Instance.Haptics.Player.SubmitRegistered("Hand/Climbing_L");
                if (ConfigLoader.Config.VestClimbEffects)
                    Mod.Instance.Haptics.Player.SubmitRegistered("Vest/Climbing_L");
            }

            if (value == Handedness.Right)
            {
                Mod.Instance.Haptics.Player.SubmitRegistered("Arm/Climbing_R");
                Mod.Instance.Haptics.Player.SubmitRegistered("Hand/Climbing_R");
                if (ConfigLoader.Config.VestClimbEffects)
                    Mod.Instance.Haptics.Player.SubmitRegistered("Vest/Climbing_R");
            }

        }

        public static void ShieldBreak()
        {
            Mod.Instance.Haptics.Player.SubmitRegistered("Vest/ShieldBreak");
        }
        
        public static void ShieldFull()
        {
            Mod.Instance.Haptics.Player.SubmitRegistered("Vest/FullShield");
        }
        
        public static void Victory()
        {
            Mod.Instance.Haptics.Player.SubmitRegistered("Vest/Win");
        }

        public static void Land()
        {
            PatternManager.Effects["Foot/LandOnGround"]?.Play(new Effect.EffectProperties
            {
                Strength = 0.4f,
                Time = 0.12f
            });
        }
    }
}