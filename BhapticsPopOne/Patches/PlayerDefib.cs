using System;
using BigBoxVR;
using Harmony;
using MelonLoader;
using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.Patterns;
using BigBox.PopOne.Unity;
using UnityEngine;

namespace BhapticsPopOne.Patches
{
    [HarmonyPatch(typeof(PlayerDefib), "State", MethodType.Setter)]
    public class OnDefibStateChanged
    {
        // defibrillator charging with haptics
        static void Prefix(PlayerDefib __instance, PlayerDefib.DefibState value)
        {
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

    [HarmonyPatch(typeof(PlayerDefib), "UserCode_RpcReviveEffects")]
    public class UserCode_RpcReviveEffects
    {
        static void Prefix(PlayerDefib __instance, uint revivedPlayerNetId, Vector3 position, Vector3 lookAt)
        {
            if (!PlayerContainer.Find(revivedPlayerNetId).isLocalPlayer)
                return;
            
            PlayerRevived.Execute(position, lookAt);
        }
    }
}
