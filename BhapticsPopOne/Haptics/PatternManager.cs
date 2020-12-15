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
        public static void LoadPatterns()
        {
            string baseDir = Directory.GetCurrentDirectory() + @"\Mods\BhapticsPopOne\Effects";
            var files = Directory.GetFiles(baseDir);
            
            foreach (var file in files)
            {
                var label = Path.GetFileNameWithoutExtension(file);
                Mod.Instance.Haptics.Player.RegisterTactFileStr(label, System.IO.File.ReadAllText(file));
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
            Mod.Instance.Haptics.Player.SubmitRegistered("Electric1", 0.25f);
            Mod.Instance.Haptics.Player.SubmitRegistered("Electric1_back", 0.25f);
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
            Mod.Instance.Haptics.Player.SubmitRegistered("ExplosionUp3_both", 0.15f);
        }

        public static void DrinkSoda(BuffState state)
        {
            if (state != BuffState.Consumed)
                return;
            
            Mod.Instance.Haptics.Player.SubmitRegistered("ConsumeEnergyDrink");
        }
        
        public static void EatBanana(BuffState state)
        { 
            if (state == BuffState.Consumed)
                Mod.Instance.Haptics.Player.SubmitRegistered("EatBanana");

        }
    }
}