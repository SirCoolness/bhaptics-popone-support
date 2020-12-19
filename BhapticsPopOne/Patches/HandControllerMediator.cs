using System;
using BhapticsPopOne.Haptics;
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
}
