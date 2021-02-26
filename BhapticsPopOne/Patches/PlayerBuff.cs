using System;
using System.Collections;
using System.Runtime.InteropServices;
using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.Patterns;
using BigBoxVR;
using BigBoxVR.BattleRoyale.Models.Shared;
using Harmony;
using MelonLoader;
using UnhollowerBaseLib;

namespace BhapticsPopOne
{
    [HarmonyPatch(typeof(PlayerBuff), "OnBuffStateChanged")]
    public class OnBuffStateChanged
    {
        static void Prefix(PlayerBuff __instance, InventorySlot.BuffRecord record, BuffState state)
        {
            if (!__instance.container.isLocalPlayer)
            {
                // MelonLogger.LogWarning("container is null");
                return;
            }
            
            MelonLogger.Log(state.ToString());

            var name = __instance.EquippedSlot.ItemType;
            MelonLogger.Log(name);
            
            if (__instance.EquippedSlot.ItemType == InventoryItemType.BuffEnergyDrink || __instance.EquippedSlot.ItemType == InventoryItemType.BuffShieldDrink)
            {
                DrinkSoda.Execute(__instance.usableBehaviour.Info.TimeToApply, state);
            } else if (__instance.EquippedSlot.ItemType == InventoryItemType.BuffBanana)
            {
                PatternManager.EatBanana(state);
            }
        }
    }
}
