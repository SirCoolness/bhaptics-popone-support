﻿using System;
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
    [HarmonyPatch(typeof(PlayerBuffUsableBehaviour), "State", MethodType.Setter)]
    public class OnBuffStateChanged
    {
        static void Prefix(PlayerBuffUsableBehaviour __instance, BuffState value)
        {
            if (!(__instance?.playerContainer?.isLocalPlayer == true))
            {
                // MelonLogger.LogWarning("container is null");
                return;
            }
            
            if (__instance.PlayerUsable.EquippedSlot.ItemType == InventoryItemType.BuffEnergyDrink || __instance.PlayerUsable.EquippedSlot.ItemType == InventoryItemType.BuffShieldDrink)
            {
                DrinkSoda.Execute(__instance.Info.TimeToApply, value);
            } else if (__instance.PlayerUsable.EquippedSlot.ItemType == InventoryItemType.BuffBanana)
            {
                PatternManager.EatBanana(value);
            }
        }
    }
}