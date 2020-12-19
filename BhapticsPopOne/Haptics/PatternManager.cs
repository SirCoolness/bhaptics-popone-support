using System;
using Bhaptics.Tact;
using MelonLoader;
using MelonLoader.ICSharpCode;
using Unity;
using UnityEngine;
using System.IO;
using BigBox.PopOne.Unity;
using System.Collections;

namespace BhapticsPopOne.Haptics
{
    // TODO: NO
    public class PatternManager
    {
        // an effects name is "{gear}/{effect name}"
        
        // The subdirectories of effects
        // this can be used to organize effects based on piece of equipment
        public static string[] subdirectories = new[]
        {
            "Vest",
            "Arm",
            "Head"
        };

        public static float VestHeight = 0.7f;

        // loads all subdirectories
        // TODO: change to recursive
        public static void LoadPatterns()
        {
            foreach (var subdirectory in subdirectories)
            {
                LoadSubDirectory(subdirectory);
            }
        }
        
        // loads a set of patterns in a subdirectory
        // prefixes them with that subdirectory
        public static void LoadSubDirectory(string subdirectory)
        {
            string baseDir = Directory.GetCurrentDirectory() + @"\Mods\BhapticsPopOne\Effects" + $"\\{subdirectory}";
            var files = Directory.GetFiles(baseDir);
            
            foreach (var file in files)
            {
                var label = Path.GetFileNameWithoutExtension(file);
                Mod.Instance.Haptics.Player.RegisterTactFileStr($"{subdirectory}/{label}", System.IO.File.ReadAllText(file));
            }
            
            MelonLogger.Log("Loaded patterns");
        }
        
        public static void TestPattern()
        {
            byte[] play = 
            {
                100, 100, 100, 100, 100,
                100, 100, 100, 100, 100,
                100, 100, 100, 100, 100,
                100, 100, 100, 100, 100
            };
        
            Mod.Instance.Haptics.Player.Submit("Bytes", PositionType.All, play, 50);
        }
        
        public static void ZoneHit()
        {
            Mod.Instance.Haptics.Player.SubmitRegistered("Vest/ZoneDamage", 0.25f);
        }

        public static void FallDamage()
        {
            Mod.Instance.Haptics.Player.SubmitRegistered("Vest/ExplosionUp3_both", 0.15f);
        }

        public static void DrinkSoda(BuffState state)
        {
            if (state != BuffState.Consumed)
                return;
            
            Mod.Instance.Haptics.Player.SubmitRegistered("Vest/ConsumeEnergyDrink");
        }
        
        public static void EatBanana(BuffState state)
        { 
            if (state == BuffState.Consumed)
                Mod.Instance.Haptics.Player.SubmitRegistered("Vest/EatBanana");
        }

        public static void LowHealthHeartbeat()
        {
            Mod.Instance.Haptics.Player.SubmitRegistered("Vest/HeartbeatMultiple");
        }

        public static void FlyingAir()
        {
            if (!Mod.Instance.Haptics.Player.IsPlaying("Vest/FlyingAir"))
            {
                Mod.Instance.Haptics.Player.SubmitRegistered("Vest/FlyingAir");
            }
        }

        public static void FallingAir()
        {
            if (!Mod.Instance.Haptics.Player.IsPlaying("Vest/FallingAir"))
            {
                Mod.Instance.Haptics.Player.SubmitRegistered("Vest/FallingAir");
            }
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
        }

        public static void DuringPod()
        {
            if (!Mod.Instance.Haptics.Player.IsPlaying("Vest/DuringPod"))
            {
                Mod.Instance.Haptics.Player.SubmitRegistered("Vest/DuringPod");
            }
        }

        public static void FallingPod()
        {
            if (!Mod.Instance.Haptics.Player.IsPlaying("Vest/FallingAir"))
            {
                Mod.Instance.Haptics.Player.SubmitRegistered("Vest/FallingAir");
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
                Mod.Instance.Haptics.Player.SubmitRegistered("Vest/Climbing_L");
            }

            if (value == Handedness.Right)
            {
                Mod.Instance.Haptics.Player.SubmitRegistered("Arm/Climbing_R");
                Mod.Instance.Haptics.Player.SubmitRegistered("Vest/Climbing_R");
            }

        }
    }
}