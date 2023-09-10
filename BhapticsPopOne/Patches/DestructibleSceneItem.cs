using BhapticsPopOne.Haptics.Patterns;
using HarmonyLib;
using MelonLoader;
using UnityEngine;
using Il2Cpp;
using Il2CppBigBoxVR;

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
    
            var handHelper = Mod.Instance.Data.Players.LocalHandHelper;
            if (!handHelper.brokenDestructibles.ContainsKey(__instance.SceneId))
            {
                // MelonLogger.Log("cannot find key");
                return;
            }
    
            foreach (var handedness in handHelper.brokenDestructibles[__instance.SceneId])
            {
                DestructibleHit.Execute(handedness);
            }
        }
    }
}
