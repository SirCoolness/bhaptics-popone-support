using System;
using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.Patterns;
using Il2CppBigBoxVR;
using HarmonyLib;
using MelonLoader;
using Il2Cpp;

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

            Climbing.Execute(value);
        }
    }
}
