using System;
using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.Patterns;
using BigBoxVR;
using Harmony;
using MelonLoader;

namespace BhapticsPopOne.Patches
{
    [HarmonyPatch(typeof(HandControllerMediator), "OnLocalPlayerClimbHandChanged")]
    public class OnLocalPlayerClimbHandChanged
    {
        // arms and vest patterns for climbing
        static void Prefix(HandControllerMediator __instance, uint netId, Handedness value)
        {
            if(value == Handedness.Unknown)
            {
                return;
            }

            PatternManager.Climbing(value);
        }
    }

    [HarmonyPatch(typeof(HandControllerMediator), "OnGripDown")]
    public class OnGripDown
    {
        // arms and vest patterns for climbing
        static void Prefix(HandControllerMediator __instance)
        {
            Handedness hand = Handedness.Unknown;
            
            if (
                Mod.Instance.Data.Players.LocalPlayerContainer.Data.DominantHand == Handedness.Right)
            {
                if (__instance.IsDominantHand)
                {
                    hand = Handedness.Right;
                }
                else
                {
                    hand = Handedness.Left;
                }
            } else if (
                Mod.Instance.Data.Players.LocalPlayerContainer.Data.DominantHand == Handedness.Left)
            {
                if (__instance.IsDominantHand)
                {
                    hand = Handedness.Left;
                }
                else
                {
                    hand = Handedness.Right;
                }
            }

            if (hand == Handedness.Unknown)
            {
                MelonLogger.LogError("Cannot find pickup hand");
                return;
            }
            
            PickupItem.UpdateLastGrip(hand);
        }
    }
}
