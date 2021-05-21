using System;
using System.Runtime.InteropServices;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using BigBoxVR.BattleRoyale.Models.Shared;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class FirearmFire
    {
        private static float _effectStrength => Mathf.Clamp(ConfigLoader.Config.VestRecoil, 0f, 1f);
        
        public static void Execute(FirearmInfo info, bool dominant)
        {
            var item = info.Type;
            var type = info.Class;
            var local = Mod.Instance.Data.Players.LocalPlayerContainer;

            if (local == null)
                return;
            
            var handed = dominant ? local.Data.DominantHand : local.Data.ClimbingHand;
            var twoHanded = local.Data.TwoHand;

            string effectExtension = HapticUtils.HandExt(handed);
            string otherEffectExtension = HapticUtils.HandExt(handed == Handedness.Right ? Handedness.Left : Handedness.Right);
            
            float offhandIntensity = ConfigLoader.Config.OffhandRecoilStrength;

            string effectName = "RecoilLevel0";
            float effectTime = 1f;
            float handTime = 0f;
            float handStrength = 1f;

            if (item == InventoryItemType.FirearmAssaultM60)
            {
                effectName = "RecoilLevel9001";
                handTime = 0.4f;
                effectTime = 0.5f;
            }
            else if (item == InventoryItemType.FirearmSniperAWP || item == InventoryItemType.FirearmPistol357)
            {
                effectName = "RecoilLevel9001";
                handTime = 0.4f;
            }
            else if (type == FirearmClass.SMG)
            {
                effectName = "RecoilLevel1";
                handTime = 0.125f;
                handStrength = 0.9f;
            }
            else if (type == FirearmClass.Pistol || type == FirearmClass.AR)
            {
                effectName = "RecoilLevel2";
                handTime = 0.2f;
            }
            else if (type == FirearmClass.Sniper || type == FirearmClass.Shotgun)
            {
                effectName = "RecoilLevel3";
                handTime = 0.3f;
            }
            
            if (effectName == "RecoilLevel0")
                return;
            
            if (DynConfig.Toggles.Vest.Recoil)
                EffectPlayer.Play($"Vest/{effectName}{effectExtension}", new Effect.EffectProperties
                {
                    Strength = _effectStrength,
                    Time = effectTime
                });
            
            if (DynConfig.Toggles.Arms.Recoil)
                EffectPlayer.Play($"Arm/{effectName}{effectExtension}", new Effect.EffectProperties
                {
                    Time = effectTime
                });
            
            if (DynConfig.Toggles.Hands.Recoil)
                EffectPlayer.Play($"Hand/Recoil{effectExtension}", new Effect.EffectProperties
                {
                    Strength = handStrength,
                    Time = effectTime * handTime
                });
            
            if (twoHanded)
            {
                if (DynConfig.Toggles.Vest.Recoil)
                    EffectPlayer.Play($"Vest/{effectName}{otherEffectExtension}", new Effect.EffectProperties
                    {
                        Strength = _effectStrength * offhandIntensity,
                        Time = effectTime
                    });
                
                if (DynConfig.Toggles.Arms.Recoil)
                    EffectPlayer.Play($"Arm/{effectName}{otherEffectExtension}", new Effect.EffectProperties
                    {
                        Strength = offhandIntensity,
                        Time = effectTime
                    });
                
                if (DynConfig.Toggles.Hands.Recoil)
                    EffectPlayer.Play($"Hand/Recoil{otherEffectExtension}", new Effect.EffectProperties
                    {
                        Strength = offhandIntensity * handStrength,
                        Time = effectTime * handTime
                    });
            }
        }
    }
}
