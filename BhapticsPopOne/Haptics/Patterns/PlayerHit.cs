using System;
using Bhaptics.Tact;
using BigBoxVR.BattleRoyale.Models.Shared;
using MelonLoader;
using UnityEngine;

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
            
            if (Array.Exists(BulletWeapons, el => el == info.Weapon))
                BulletHit(info, -angle, 0f);
            else if (Array.Exists(BulletWeapons, el => el == info.Weapon))
                ExplosionHit(info, -angle);
            // Mod.Instance.Haptics.Player.SubmitRegisteredVestRotation("Vest/TestBullet", new RotationOption(-angle, 0));
        }

        public static void BulletHit(DamageableHitInfo info, float angle, float yOffset)
        {
            if (Array.Exists(Shotguns, el => el == info.Weapon))
            {
                // TODO: add shotgun effect random variants
                Mod.Instance.Haptics.Player.SubmitRegisteredVestRotation("Vest/BulletHit_Shotgun", new RotationOption(angle, 0));
                return;
            }

            if (info.Damage > 100)
                Mod.Instance.Haptics.Player.SubmitRegisteredVestRotation("Vest/BulletHit_HighDamage", new RotationOption(angle, 0));
            else if (info.Damage > 19)
                Mod.Instance.Haptics.Player.SubmitRegisteredVestRotation("Vest/BulletHit_Level2", new RotationOption(angle, 0));
            else
                Mod.Instance.Haptics.Player.SubmitRegisteredVestRotation("Vest/BulletHit_Level1", new RotationOption(angle, 0));
        }

        public static void ExplosionHit(DamageableHitInfo info, float angle)
        {
            // TODO: add level in between
            if (info.Damage > 65)
                Mod.Instance.Haptics.Player.SubmitRegisteredVestRotation("Vest/ExplosionHit_Level2", new RotationOption(angle, 0f));
            else
            {
                Mod.Instance.Haptics.Player.SubmitRegisteredVestRotation("Vest/ExplosionHit_Level1", new RotationOption(angle, 0f));
            }
        }
    }
}