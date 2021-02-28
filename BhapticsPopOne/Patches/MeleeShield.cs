using BhapticsPopOne.Haptics.Patterns;
using Harmony;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne
{
    [HarmonyPatch(typeof(MeleeShield), "ShieldActive", MethodType.Setter)]
    public class ShieldActiveSetter
    {
        public static void Prefix(MeleeShield __instance, bool value)
        {
            if (!__instance.melee.container.isLocalPlayer)
                return;
            
            KatanaShield.Execute(value);
        }
    }

    [HarmonyPatch(typeof(MeleeShield), "QueueShieldHitHaptic")]
    public class QueueShieldHitHaptic
    {
        public static void Prefix(MeleeShield __instance)
        {
            if (!__instance.melee.container.isLocalPlayer)
                return;
            
            KatanaShield.Block();
        }
    }
}