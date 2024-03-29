﻿using System;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using Il2CppBigBoxVR.BattleRoyale.Models.Shared;
using MelonLoader;
using UnityEngine;
using Random = UnityEngine.Random;
using Il2Cpp;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class PlayerHit
    {
        private static InventoryItemType[] BulletWeapons = new[]
        {
            InventoryItemType.FirearmPistol357,
            InventoryItemType.FirearmPistolPx4,
            InventoryItemType.FirearmPistol1911,
            InventoryItemType.FirearmSMGP90,
            InventoryItemType.FirearmSMGMP5,
            InventoryItemType.FirearmSMGUMP,
            InventoryItemType.FirearmAssaultCX4,
            InventoryItemType.FirearmAssaultMK18,
            InventoryItemType.FirearmAssaultAKM,
            InventoryItemType.FirearmShotgunDT11,
            InventoryItemType.FirearmShotgunM1014,
            InventoryItemType.FirearmSniperSako85,
            InventoryItemType.FirearmSniperAWP,
            InventoryItemType.FirearmAssaultM60,
        };

        private static InventoryItemType[] ExplosionItems = new[]
        {
            InventoryItemType.ThrowableGrenade
        };

        private static InventoryItemType[] Shotguns = new[]
        {
            InventoryItemType.FirearmShotgunM590, // ¯\_(ツ)_/¯ (cool hotkey, win + .)
            InventoryItemType.FirearmShotgunDT11,
            InventoryItemType.FirearmShotgunM1014
        };

        public static void Execute(DamageableHitInfo info)
        {
            var vestRef = Mod.Instance.Data.Players.VestReference();
            if (vestRef == null)
            {
                MelonLogger.Warning("Cant the reference transform for the vest.");
                return;
            }

            Vector3 forward = Quaternion.Euler(0, -90f, 0) * vestRef.forward;
            var angle = BhapticsUtils.Angle(forward, -info.Forward);

            float offsetY = Mathf.Clamp((info.ImpactPosition.y - (vestRef.position.y + PatternManager.VestCenterOffset)) / PatternManager.VestHeight, -0.5f, 0.5f);
            
            if (info.Source == HitSourceCategory.Firearm)
                BulletHit(info, -angle, offsetY);
            else if (info.Source == HitSourceCategory.Explosive || info.Source == HitSourceCategory.ExplodingBarrel)
                ExplosionHit(info, -angle);
        }

        public static void BulletHit(DamageableHitInfo info, float angle, float offsetY)
        {
            if (Array.Exists(Shotguns, el => el == info.Weapon))
            {
                // TODO: add shotgun effect random variants
                if (DynConfig.Toggles.Vest.BulletHit)
                    EffectPlayer.Play("Vest/BulletHit_Shotgun", new Effect.EffectProperties
                    {
                        XRotation = angle
                    });
                return;
            }

            if (DynConfig.Toggles.Face.BulletHit && info.HeadImpact)
                EffectPlayer.Play($"Head/HeadshotHit_[{Random.RandomRangeInt(1, 5)}]");

            if (DynConfig.Toggles.Vest.BulletHit)
            {
                if (ConfigLoader.Config.Toggles.DetailedBulletHits)
                    NormalBullet(info, angle, offsetY);
                else
                    HardBullet(info, angle, offsetY);
            }
        }

        public static void ExplosionHit(DamageableHitInfo info, float angle)
        {
            var damage = info.Damage * -1;

            if (DynConfig.Toggles.Face.ExplosionHit && info.HeadImpact)
                EffectPlayer.Play($"Head/ExplosionHit_[{Random.RandomRangeInt(1, 3)}]");

            // TODO: add level in between
            // TODO: You can use ScaleOption!
            if (damage > 60)
            {
                if (DynConfig.Toggles.Vest.ExplosionHit)
                    EffectPlayer.Play("Vest/ExplosionHit_Level2", new Effect.EffectProperties
                    {
                        XRotation = angle
                    });
            }
            else
            {
                if (DynConfig.Toggles.Vest.ExplosionHit)
                    EffectPlayer.Play("Vest/ExplosionHit_Level1", new Effect.EffectProperties
                    {
                        XRotation = angle
                    });
            }
        }

        private static void HardBullet(DamageableHitInfo info, float angle, float offsetY)
        {
            var damage = info.Damage * -1;
            
            if (damage > 60)
                EffectPlayer.Play("Vest/BulletHit_Pierce_V2", new Effect.EffectProperties
                {
                    XRotation = angle,
                    YOffset = offsetY,
                    Time = 0.125f
                });
            else
                EffectPlayer.Play("Vest/BulletHit_V2", new Effect.EffectProperties
                {
                    XRotation = angle,
                    YOffset = offsetY,
                    Time = 0.125f
                });
        }

        private static void NormalBullet(DamageableHitInfo info, float angle, float offsetY)
        {
            var damage = info.Damage * -1;

            if (damage > 75)
                EffectPlayer.Play("Vest/BulletHit_HighDamage", new Effect.EffectProperties
                {
                    XRotation = angle,
                    YOffset = offsetY
                });
            else if (damage > 19)
                EffectPlayer.Play("Vest/BulletHit_Level2", new Effect.EffectProperties
                {
                    XRotation = angle,
                    YOffset = offsetY
                });
            else
                EffectPlayer.Play("Vest/BulletHit_Level1", new Effect.EffectProperties
                {
                    XRotation = angle,
                    YOffset = offsetY
                });
        }
    }
}