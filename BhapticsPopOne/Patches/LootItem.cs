using System;
using System.Runtime.InteropServices;
using BhapticsPopOne.Haptics.Patterns;
using BigBoxVR.BattleRoyale.Models.Shared;
using Harmony;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne.LootItem2
{
    [HarmonyPatch(typeof(LootItem), "FlyItem")]
    public class FlyItem
    {
        static void Prefix(LootItem __instance, PlayerContainer container, Handedness handedness)
        {
            if (!container.isLocalPlayer)
                return;
            
            PickupItem.Execute(handedness);
        }
    }
    
    [HarmonyPatch(typeof(LootItem), "EjectWithVelocity")]
    public class EjectWithVelocity
    {
        static void Prefix(LootItem __instance, Vector3 ejectVelocity)
        {
            MelonLogger.Log(__instance.netIdentity.isLocalPlayer);
            if (!__instance.netIdentity.isLocalPlayer)
                return;
            
            DropItem.Execute();
        }
    }
}