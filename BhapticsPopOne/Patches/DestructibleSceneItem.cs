using BhapticsPopOne.Haptics.Patterns;
using Harmony;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne.DestructibleSceneItem2
{
    [HarmonyPatch(typeof(DestructibleSceneItem), "LocalDestructItem")]
    public class LocalDestructItem
    {
        // unfortunately this is the only method available to the client
        static void Prefix(DestructibleSceneItem __instance, Vector3 damagePoint, Vector3 forward, uint owner)
        {
            var container = PlayerContainer.Find(owner);
            
            if (!container.isLocalPlayer)
                return;

            float leftDist = Vector3.Distance(container.Avatar.HandLeft.position, damagePoint);
            float rightDist = Vector3.Distance(container.Avatar.HandRight.position, damagePoint);


            if (leftDist > rightDist)
            {
                DestructibleHit.Execute(Handedness.Right);
            }
            else
            {
                DestructibleHit.Execute(Handedness.Left);
            }
        }
    }
}