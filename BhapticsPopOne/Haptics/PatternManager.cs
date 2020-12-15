using System;
using Bhaptics.Tact;
using MelonLoader;
using UnityEngine;
using System.IO;

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

        // loads all subdirectories
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
            Mod.Instance.Haptics.Player.SubmitRegistered("Vest/Electric1", 0.25f);
            Mod.Instance.Haptics.Player.SubmitRegistered("Vest/Electric1_back", 0.25f);
        }
        
        public static void BulletHit(DamageableHitInfo info)
        {
            byte[] play = 
            {
                100, 100, 100, 100, 100,
                100, 100, 100, 100, 100,
                100, 100, 100, 100, 100,
                100, 100, 100, 100, 100
            };
        
            Mod.Instance.Haptics.Player.Submit("Bytes", PositionType.All, play, (int)(100 * Math.Min(info.Power / 10f, 10f)));
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
    }
}