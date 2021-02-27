using BhapticsPopOne.Haptics.Patterns;
using Harmony;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne
{
    [HarmonyPatch(typeof(PlayerMelee), "OnWoosh")]
    public class OnWoosh
    {
        public static void Prefix(PlayerMelee __instance)
        {
            if (!__instance.isLocalPlayer)
                return;
            
            MelonLogger.Log("Slicing started");
            
            Haptics.Patterns.MeleeVelocity.IsSlicing = true;
        }
    }

    [HarmonyPatch(typeof(PlayerMelee), "OnHitDamageable")]
    public class OnHitDamageable
    {
        public static void Prefix(PlayerMelee __instance, IDamageable damageable, Collider col, Vector3 swipeDirection,
            float multiplier)
        {
            if (!__instance.isLocalPlayer)
                return;
            
            MeleeDamageableHit.Execute();
        }
    }
}