using System;
using System.Collections;
using System.Runtime.InteropServices;
using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.Patterns;
using BigBoxVR;
using BigBoxVR.BattleRoyale.Models.Shared;
using HarmonyLib;
using MelonLoader;
using UnhollowerBaseLib;

namespace BhapticsPopOne
{
    [HarmonyPatch(typeof(PlayerBuffUsableBehaviour), "State", MethodType.Setter)]
    public class OnBuffStateChanged
    {
        static void Prefix(PlayerBuffUsableBehaviour __instance, BuffState value)
        {
            if (!(__instance?.playerContainer?.isLocalPlayer == true))
            {
                return;
            }

            if (__instance.PlayerUsable.EquippedSlot.ItemType == InventoryItemType.BuffEnergyDrink || __instance.PlayerUsable.EquippedSlot.ItemType == InventoryItemType.BuffShieldDrink)
            {
                DrinkSoda.Execute(__instance.PlayerUsable.EquippedSlot.ItemType, __instance.Info.TimeToApply, value);
            } else if (__instance.PlayerUsable.EquippedSlot.ItemType == InventoryItemType.BuffBanana)
            {
                Food.EatBanana(value);
            } else if (__instance.PlayerUsable.EquippedSlot.ItemType == InventoryItemType.BuffShieldShaker)
            {
                Shaker.Consume(value);
            }
        }
    }
}
