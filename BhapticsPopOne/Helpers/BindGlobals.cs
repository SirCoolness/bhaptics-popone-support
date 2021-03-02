using System;
using System.Collections.Generic;
using BhapticsPopOne.Haptics.Patterns;
using BhapticsPopOne.Utils;
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
                GoyfsHelper.TryAddListener<PlayerContainerAddedSignal, PlayerContainer>(OnPlayerAdded)
            };

            List<int> failures = new List<int>();
            for (var i = 0; i < bindStatuses.Length; i++)
            {
                if (bindStatuses[i])
                    continue;
                
                failures.Add(i);
            }

            if (failures.Count > 0)
            {
                MelonLogger.LogError($"Failed to binding to Goyfs: [{String.Join(", ", failures)}]");
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
    }
}