﻿using BhapticsPopOne.Haptics.Patterns;
using BigBoxVR.BattleRoyale.Models.Shared;
using HarmonyLib;
using MelonLoader;

namespace BhapticsPopOne.Patches.ShakeConsumablePatched
{
    [HarmonyPatch(typeof(ShakeConsumable), "HandleShake")]
    public class HandleShake
    {
        public static void Prefix(ShakeConsumable __instance, InventorySlot equippedSlot)
        {
            if (__instance.UsableBehaviour.PlayerUsable.netId != Mod.Instance.Data.Players.LocalPlayerContainer.netId)
                return;
            
            if (equippedSlot.Info.Type != InventoryItemType.BuffShieldShaker)
                return;
            
            Shaker.HandleShake(__instance.previousLocalPosition);
        }
    }
}