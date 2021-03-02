using System.Collections.Generic;
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
                PatternManager.Effects["Vest/ZoneDamage"]?.Play(new Effect.EffectProperties
                {
                    Strength = 0.30f + Mathf.Clamp(((float)info.Damage / 6f) * 0.5f, 0f, 0.5f),
                    Time = 0.4f
                });   
            }
            else
            {
                PatternManager.Effects["Vest/ZoneDamage"]?.Play(new Effect.EffectProperties
                {
                    Strength = 0.20f + Mathf.Clamp(((float)info.Damage / 4f) * 0.35f, 0f, 0.35f)
                });   
            }
        }

        public static void SetActive(ZoneSource source, bool active)
        {
            ActiveSources[source] = active;
            
            if (!Active && WasPlaying)
            {
                PatternManager.Effects["Vest/ZonePassive"]?.Stop();
                WasPlaying = false;
            }
        }

        public static void Clear()
        {
            ActiveSources.Clear();
        }

        public static void OnFixedUpdate()
        {
            if (!Active)
                return;
            
            if (WasPlaying && PatternManager.Effects["Vest/ZonePassive"]?.isPlaying == true)
                return;
                
            PatternManager.Effects["Vest/ZonePassive"]?.Play(new Effect.EffectProperties
            {
                Strength = 0.1f
            });
            WasPlaying = true;
        }
    }
}