using Harmony;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne.Punch2
{
    [HarmonyPatch(typeof(Punch), "OnTriggerEnter")]
    public class OnTriggerEnter
    {
        // using postfix to detect if destroyed 
        static void Prefix(Punch __instance, Collider col)
        {
            // if (!__instance.playerContainer.isLocalPlayer)
            // {
            //     // MelonLogger.Log("1");
            //     return;
            // }
            //
            var dest = col.GetComponentInParent<DestructibleSceneItem>();
            if (dest == null || !dest.CanHit)
            {
                // MelonLogger.Log("2");
                return;
            }
            
            MelonLogger.Log($"[PUNCH] Player punched destructible. {__instance.handedness.ToString()} {__instance.netIdentity.netId} {__instance.netIdentity.isLocalPlayer}");
        }

    }
}