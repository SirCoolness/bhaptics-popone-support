#if PORT_DISABLE
using System;
using BigBoxVR;
using Harmony;
using MelonLoader;
using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.Patterns;
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
                Defib.RubbingDefib();
            else if (value == PlayerDefib.DefibState.Charged)
                Defib.ChargedDefib();
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
#endif