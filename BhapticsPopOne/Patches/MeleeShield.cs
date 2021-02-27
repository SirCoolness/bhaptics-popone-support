using BhapticsPopOne.Haptics.Patterns;
using Harmony;
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

    [HarmonyPatch(typeof(MeleeShield), "GetHit")]
    public class GetHit
    {
        public static void Prefix(MeleeShield __instance, DamageableHitInfo info, Collider impactCollider)
        {
            if (!__instance.melee.container.isLocalPlayer)
                return;
            
            KatanaShield.Block();
        }
    }
}