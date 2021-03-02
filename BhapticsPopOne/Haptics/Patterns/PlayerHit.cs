using System;
using Bhaptics.Tact;
using BhapticsPopOne.Haptics.EffectHelpers;
using BigBoxVR.BattleRoyale.Models.Shared;
using MelonLoader;
using UnityEngine;
using Random = UnityEngine.Random;

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
            InventoryItemType.FirearmSniperAWP
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
                MelonLogger.LogWarning("Cant the reference transform for the vest.");
                return;
            }

            Vector3 forward = Quaternion.Euler(0, -90f, 0) * vestRef.forward;
            var angle = BhapticsUtils.Angle(forward, -info.Forward);

            float offsetY = Mathf.Clamp((info.ImpactPosition.y - (vestRef.position.y + PatternManager.VestCenterOffset)) / PatternManager.VestHeight, -0.5f, 0.5f);
            
            if (Array.Exists(BulletWeapons, el => el == info.Weapon))
                BulletHit(info, -angle, offsetY);
            else if (Array.Exists(ExplosionItems, el => el == info.Weapon))
                ExplosionHit(info, -angle);
        }

        public static void BulletHit(DamageableHitInfo info, float angle, float offsetY)
        {
            if (Array.Exists(Shotguns, el => el == info.Weapon))
            {
                // TODO: add shotgun effect random variants
                EffectPlayer.Play("Vest/BulletHit_Shotgun", new Effect.EffectProperties
                {
                    XRotation = angle
                });
                return;
            }

            if (info.HeadImpact)
                EffectPlayer.Play($"Head/HeadshotHit_[{Random.RandomRangeInt(1, 5)}]");

            var damage = info.Damage * -1;

            if (damage > 75)
                EffectPlayer.Play("Vest/BulletHit_HighDamage", new Effect.EffectProperties
                {
                    XRotation = angle,
                    YOffset = offsetY
                });
            else if (damage > 19)
                EffectPlayer.Play("Vest/BulletHit_Level2" , new Effect.EffectProperties
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

        public static void ExplosionHit(DamageableHitInfo info, float angle)
        {
            var damage = info.Damage * -1;

            if (info.HeadImpact)
                EffectPlayer.Play($"Head/ExplosionHit_[{Random.RandomRangeInt(1, 3)}]");

            // TODO: add level in between
            // TODO: You can use ScaleOption!
            if (damage > 60)
                EffectPlayer.Play("Vest/ExplosionHit_Level2", new Effect.EffectProperties{
                    XRotation = angle
                });
            else
                EffectPlayer.Play("Vest/ExplosionHit_Level1", new Effect.EffectProperties
                {
                    XRotation = angle
                });
        }
    }
}