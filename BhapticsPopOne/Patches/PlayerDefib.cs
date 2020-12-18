using System;
using BigBoxVR;
using Harmony;
using MelonLoader;
using BhapticsPopOne.Haptics;
using BigBox.PopOne.Unity;

namespace BhapticsPopOne.Patches
{
    [HarmonyPatch(typeof(PlayerDefib), "State", MethodType.Setter)]
    public class OnDefibStateChanged
    {
        // defibrillator charging with haptics
        static void Prefix(HandControllerMediator __instance, PlayerDefib.DefibState value)
        {
            MelonLogger.Log(ConsoleColor.Blue, "Defib : " + value);

            if (value == PlayerDefib.DefibState.Rubbing)
            {
                PatternManager.RubbingDefib();
            }

            if (value == PlayerDefib.DefibState.Charged)
            {
                PatternManager.ChargedDefib();
            }
        }
    }
}
