using System.Collections.Generic;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.ConfigManager.ConfigElements;
using BhapticsPopOne.Haptics.EffectHelpers;
using Il2CppBigBoxVR.BattleRoyale.Models.Shared;
using UnityEngine;

using Il2Cpp;
namespace BhapticsPopOne.Haptics.Patterns
{
    public class ZoneDamage
    {
        private static bool Active
        {
            get
            {
                foreach (var activeSourcesValue in ActiveSources.Values)
                    if (activeSourcesValue)
                        return true;

                return false;
            }
        }

        private static bool WasPlaying = false;

        public enum ZoneSource
        {
            ZoneGrenade,
            BattleZone
        }
        
        public static Dictionary<ZoneSource, bool> ActiveSources = new Dictionary<ZoneSource, bool>();
        
        public static void Hit(DamageableHitInfo info)
        {
            if (ConfigLoader.Config.Toggles.ZoneBackMassageMode)
                return;
            
            if (info.Weapon == InventoryItemType.ThrowableZoneGrenade)
            {
                if (DynConfig.Toggles.Vest.ZoneDamage)
                    EffectPlayer.Play("Vest/ZoneDamage", new Effect.EffectProperties
                    {
                        Strength = 0.75f,
                        Time = 0.4f
                    });   
            }
            else
            {
                if (DynConfig.Toggles.Vest.ZoneDamage)
                    EffectPlayer.Play("Vest/ZoneDamage", new Effect.EffectProperties
                    {
                        // Strength = 0.20f + Mathf.Clamp(((float)info.Damage / 4f) * 0.5f, 0f, 0.5f)
                        Strength = 0.75f
                    });   
            }
        }

        public static void SetActive(ZoneSource source, bool active)
        {
            ActiveSources[source] = active;
            
            if (!Active && WasPlaying)
            {
                if (!ConfigLoader.Config.Toggles.ZoneBackMassageMode)
                    EffectPlayer.Stop("Vest/ZonePassive");
                else
                    EffectPlayer.Stop("Vest/ZoneDamage_OLD");
                WasPlaying = false;
            }
        }

        public static void Clear()
        {
            ActiveSources.Clear();
        }

        public static void OnFixedUpdate()
        {
            if (!DynConfig.Toggles.Vest.InsideZone)
                return;
            
            if (!Active)
                return;

            if (!ConfigLoader.Config.Toggles.ZoneBackMassageMode)
                EffectPlayer.Play("Vest/ZonePassive", new Effect.EffectProperties
                {
                    Strength = 0.1f
                });
            else
                EffectPlayer.Play("Vest/ZoneDamage_OLD");
            
            WasPlaying = true;
        }
    }
}