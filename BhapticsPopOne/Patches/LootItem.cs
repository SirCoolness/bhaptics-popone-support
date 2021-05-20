using System;
using System.Runtime.InteropServices;
using BhapticsPopOne.Haptics.Patterns;
using BhapticsPopOne.Utils;
using BigBoxVR.BattleRoyale.Models.Shared;
using Harmony;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne.LootItem2
{
    [HarmonyPatch(typeof(LootItem), "FlyItem")]
    public class FlyItem
    {
        static void Prefix(LootItem __instance, PlayerContainer container, bool dominantHand)
        {
            if (!container.isLocalPlayer)
                return;
    
            LocalProperties properties = GoyfsHelper.Get<LocalProperties>();
            
            // TODO: make better
            Handedness hand;
    
            if (properties.DominantHand == properties.LeftHand)
            {
                hand = dominantHand ? Handedness.Left : Handedness.Right;
            } else if (properties.DominantHand == properties.RightHand)
            {
                hand = dominantHand ? Handedness.Right : Handedness.Left;
            }
            else 
                return;
    
            PickupItem.Execute(hand);
        }
    }
    
    [HarmonyPatch(typeof(LootItem), "EjectWithVelocity")]
    public class EjectWithVelocity
    {
        static void Prefix(LootItem __instance, Vector3 ejectVelocity)
        {
            if (!__instance.netIdentity.isLocalPlayer)
                return;
            
            DropItem.Execute();
        }
    }
}
