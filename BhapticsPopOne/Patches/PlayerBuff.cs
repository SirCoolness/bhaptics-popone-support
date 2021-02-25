using System;
using System.Collections;
using System.Runtime.InteropServices;
using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.Patterns;
using BigBoxVR;
using Harmony;
using MelonLoader;
using UnhollowerBaseLib;

namespace BhapticsPopOne
{
    // TODO: fix
    // [HarmonyPatch(typeof(PlayerBuff), "OnBuffStateChanged")]
    // public class OnBuffStateChanged
    // {
    //     static void Prefix(PlayerBuff __instance, InventorySlot.BuffRecord record, BuffState state)
    //     {
    //         var local = Mod.Instance.Data.Players.LocalPlayerContainer;
    //         if (__instance.container == null || local == null)
    //         {
    //             // MelonLogger.LogWarning("container is null");
    //             return;
    //         }
    //         
    //         if (__instance.container != local)
    //             return;
    //
    //         
    //         var name = __instance.model?.Info?.name;
    //         if (name == null)
    //         {
    //             // MelonLogger.LogWarning("cant find buff info");
    //             return;
    //         }
    //
    //         if (name == "EnergyDrink")
    //         {
    //             DrinkSoda.Execute(__instance.model.Info.TimeToApply, state);
    //         } else if (name == "Banana")
    //         {
    //             PatternManager.EatBanana(state);
    //         }
    //     }
    // }
}
