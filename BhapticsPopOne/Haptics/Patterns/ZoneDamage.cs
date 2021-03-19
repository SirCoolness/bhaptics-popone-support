using System.Collections.Generic;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using BigBoxVR.BattleRoyale.Models.Shared;
using UnityEngine;

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
            if (info.Weapon == InventoryItemType.ThrowableZoneGrenade)
            {
                if (ConfigLoader.Config.EffectToggles.Vest.ZoneDamage)
                    EffectPlayer.Play("Vest/ZoneDamage", new Effect.EffectProperties
                    {
                        Strength = 0.75f,
                        Time = 0.4f
                    });   
            }
            else
            {
                if (ConfigLoader.Config.EffectToggles.Vest.ZoneDamage)
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
                EffectPlayer.Stop("Vest/ZonePassive");
                WasPlaying = false;
            }
        }

        public static void Clear()
        {
            ActiveSources.Clear();
        }

        public static void OnFixedUpdate()
        {
            if (!ConfigLoader.Config.EffectToggles.Vest.InsideZone)
                return;
            
            if (!Active)
                return;

            EffectPlayer.Play("Vest/ZonePassive", new Effect.EffectProperties
            {
                Strength = 0.1f
            });
            WasPlaying = true;
        }
    }
}