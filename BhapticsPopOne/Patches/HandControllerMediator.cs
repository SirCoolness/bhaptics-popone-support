using System;
using System;
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
            MelonLogger.Log(ConsoleColor.Blue, "ClimbingHand : " + value);
        }
    }
}
