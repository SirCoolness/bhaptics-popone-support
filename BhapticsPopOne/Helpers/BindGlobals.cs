using System;
using System.Collections.Generic;
using BhapticsPopOne.Haptics.Patterns;
using BhapticsPopOne.Utils;
using BigBoxVR.BattleRoyale.Models.Shared;
using MelonLoader;

namespace BhapticsPopOne.Helpers
{
    public class BindGlobals
    {
        public static bool BindGoyfs()
        {
            bool[] bindStatuses = new[]
            {
                GoyfsHelper.TryAddListener<BattleZoneInOutSignal, bool, bool>(
                    (bool isOutsideZone, bool willTakeDamage) =>
                    {
                        ZoneDamage.SetActive(ZoneDamage.ZoneSource.BattleZone, willTakeDamage);
                    }),
                GoyfsHelper.TryAddListener<ZoneGrenadeInOutSignal, bool>(
                    (bool inside) =>
                    {
                        ZoneDamage.SetActive(ZoneDamage.ZoneSource.ZoneGrenade, inside);
                    }),
                GoyfsHelper.TryAddListener<PlayerContainerAddedSignal, PlayerContainer>(OnPlayerAdded),
                GoyfsHelper.TryAddListener<LocalFirearmFiredSignal, uint, FirearmInfo, bool>(FirearmFired)
            };

            PlayerWasHitSignal.AddLocalListener(GoyfsHelper.ConvertAction<uint, DamageableHitInfo>(PlayerWasHit));
            FirearmPrimeCompleteSignal.AddLocalListener(GoyfsHelper.ConvertAction<uint, int>(FirearmPrimeComplete));
            FirearmInsertAmmoCompleteSignal.AddLocalListener(GoyfsHelper.ConvertAction<uint>(FirearmInsertAmmoComplete));

            List<int> failures = new List<int>();
            for (var i = 0; i < bindStatuses.Length; i++)
            {
                if (bindStatuses[i])
                    continue;
                
                failures.Add(i);
            }

            if (failures.Count > 0)
            {
                MelonLogger.Error($"Failed to binding to Goyfs: [{String.Join(", ", failures)}]");
            }

            return failures.Count == 0;
        }

        private static void OnPlayerAdded(PlayerContainer container)
        {
            if (container.transform.root != container.transform || container.Avatar?.Rig == null || !container.Data.IsReady)
                return;
            
            if (container.Avatar?.Rig != null && container.Avatar.Rig.GetComponent<VelocityTracker>() == null)
            {
                container.Avatar.Rig.gameObject.AddComponent<VelocityTracker>();
                
                if (container.isLocalPlayer)
                    Haptics.Patterns.MeleeVelocity.LoadTrackers();
            }
            
            AddHandReference.AddHandsToPlayer(container);
        }
        
        private static void PlayerWasHit(uint netId, DamageableHitInfo info)
        {
            var container = Mod.Instance.Data.Players.LocalPlayerContainer;

            if (info.Source == HitSourceCategory.Melee)
                KatanaHit.Execute(container, info);
            else if (info.Source == HitSourceCategory.Firearm)
                PlayerHit.Execute(info);
            else if (info.Source == HitSourceCategory.BattleZone)
                ZoneDamage.Hit(info);
            else if (info.Source == HitSourceCategory.Explosive)
            {
                if (info.Weapon == InventoryItemType.ThrowableZoneGrenade)
                    ZoneDamage.Hit(info);
                else if (info.Weapon == InventoryItemType.ThrowableGrenade)
                    PlayerHit.Execute(info);
            }
            else if (info.Source == HitSourceCategory.Falling)
                FallDamage.Execute(-info.Damage, info.Power);
            
            if (info.ArmorBroke)
                Health.ShieldBreak();
        }

        private static void FirearmPrimeComplete(uint netId, int prime)
        {
            // kinda lazy
            // TODO: properly handle reload for both hands
            if (Mod.Instance.Data.Players.LocalPlayerContainer.Firearm.FirearmUsableBehaviour.Type == InventoryItemType.FirearmShotgunMatador)
                return;
            
            ReloadWeapon.Execute(FirearmState.Prime, prime, Mod.Instance.Data.Players.LocalPlayerContainer.Firearm.FirearmUsableBehaviour.LastReloadIndex);
        }

        private static void FirearmFired(uint netId, FirearmInfo info, bool dominant)
        {
            if (Mod.Instance.Data.Players.LocalPlayerContainer.netId == netId)
                FirearmFire.Execute(info, dominant);
        }
        
        private static void FirearmInsertAmmoComplete(uint netId)
        {
            if (Mod.Instance.Data.Players.LocalPlayerContainer.netId == netId)
                ReloadWeapon.Execute(FirearmState.Prime, 0, 0);
        }
    }
}