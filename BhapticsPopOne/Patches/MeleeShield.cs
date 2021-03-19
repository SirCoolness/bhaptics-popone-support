using BhapticsPopOne.Haptics.Patterns;
using Harmony;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne
{
    [HarmonyPatch(typeof(MeleeShield), "ShieldActive", MethodType.Setter)]
    public class ShieldActiveSetter
    {
        public static void Postfix(MeleeShield __instance, bool value)
        {
            PlayerContainer container;
            if (!PlayerContainer.TryFind(__instance.melee.container.netId, out container))
                return;
            
            if (!container.isLocalPlayer)
                return;
            
            KatanaShield.Execute(value);
        }
    }

    [HarmonyPatch(typeof(MeleeShield), "QueueShieldHitHaptic")]
    public class QueueShieldHitHaptic
    {
        public static void Postfix(MeleeShield __instance)
        {
            PlayerContainer container;
            if (!PlayerContainer.TryFind(__instance.melee.container.netId, out container))
                return;
            
            if (!container.isLocalPlayer)
                return;
            
            KatanaShield.Block();
        }
    }
}