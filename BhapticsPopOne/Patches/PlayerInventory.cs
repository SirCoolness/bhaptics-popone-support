using System;
using System.Runtime.InteropServices;
using BigBoxVR.BattleRoyale.Models.Shared;
using Harmony;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne.PlayerInventory2
{
    [HarmonyPatch(typeof(PlayerInventory), "TryTakeItem")]
    public class TryTakeItem
    {
        static void Prefix(PlayerLootDropper __instance, GameObject lootItemObject, Handedness handedness)
        {
            // MelonLogger.Log(ConsoleColor.Magenta, $"TRY {handedness.ToString()} {lootItemObject.name}");

            var lootItem = lootItemObject.GetComponent<LootItem>();
            if (lootItem == null)
            {
                MelonLogger.Log("cant find loot imtem");
                return;
            }

            // MelonLogger.Log(ConsoleColor.Cyan, $"Grab {__instance.netId} {lootItem.CanPlayerGrab(__instance.netId)}");
        }
    }
    
    [HarmonyPatch(typeof(LootItem), "RpcOnPreGrabbed")]
    public class RpcOnPreGrabbed
    {
        static void Prefix(LootItem __instance, uint playerNetId, Handedness handedness)
        {
            // MelonLogger.Log(ConsoleColor.Cyan, $"Grab g {playerNetId} {handedness.ToString()}");
        }
    }
    
    // [HarmonyPatch(typeof(PlayerInventory), "IfArmorHasSpace")]
    // public class IfArmorHasSpace
    // {
    //     static void Postfix(PlayerLootDropper __instance, ref bool __result, InventoryItemClass itemClass)
    //     {
    //         MelonLogger.Log(ConsoleColor.Magenta, $"CMD {itemClass.ToString()} {__result}");
    //     }
    // }
}