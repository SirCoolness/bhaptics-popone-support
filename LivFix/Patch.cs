using System;
using System.Collections.Generic;
using Harmony;
using MelonLoader;
using BhapticsPopOne.Utils;
using UnityEngine;

namespace LivFix
{
    public class LivFix : MelonMod
    {
        public override void OnApplicationStart()
        {
            Patreon.Run(); // (●'◡'●)
        }
    }

    [HarmonyPatch(typeof(LIV.SDK.Unity.LIV), "OnSDKActivate")]
    public class OnSDKActivate
    {
        static void Postfix(LIV.SDK.Unity.LIV __instance)
        {
            __instance._fixPostEffectsAlpha = false;
            __instance._spectatorLayerMask = (__instance._spectatorLayerMask.value & ~(1 << 7)) | (0 << 7);
            __instance._spectatorLayerMask = (__instance._spectatorLayerMask.value & ~(1 << 5)) | (1 << 5);
        }
    }

    [HarmonyPatch(typeof(PlayerAvatar), "UpdateSkinRenderStates")]
    public class UpdateSkinRenderStates
    {
        static void Postfix(PlayerAvatar __instance)
        {
            GameAwake.OnPlayerAdded(__instance.Container);
        }
    }

    [HarmonyPatch(typeof(BattleContextView), "Awake")]
    public class GameAwake
    {
        static void Postfix()
        {
            if (!BindGoyfs())
                MelonLogger.Error("Failed to bind to Goyfs");
        }
        
        public static bool BindGoyfs()
        {
            bool[] bindStatuses = new bool[]
            {
            };

            List<int> failures = new List<int>();
            for (var i = 0; i < bindStatuses.Length; i++)
            {
                if (bindStatuses[i])
                    continue;
                
                failures.Add(i);
            }

            if (failures.Count > 0)
            {
                MelonLogger.Error($"Failed to binding to Goyfs: [{String.Join(", ", failures)}]");
            }

            return failures.Count == 0;
        }
        public static void OnPlayerAdded(PlayerContainer container)
        {
            if (container.transform.root != container.transform || container.Avatar?.Rig == null || !container.Data.IsReady)
                return;

            LocalProperties properties = GoyfsHelper.Get<LocalProperties>();
            if (properties.Container.netId != container.netId)
                return;
            
            if (properties.Container.Avatar?.SkinObj == null)
                return;

            var renderer = properties.Container.Avatar.SkinObj.GetComponentInChildren<MeshRenderer>();
            renderer.gameObject.layer = 7;
        }
        
    }
}