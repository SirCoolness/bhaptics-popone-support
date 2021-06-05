using System;
using System.Collections.Generic;
using Harmony;
using MelonLoader;
using BhapticsPopOne.Utils;
using BigBoxVR;
using UnhollowerBaseLib;
using UnityEngine;

namespace LivFix
{
    public class LivFix : MelonMod
    {
        private static readonly string CategoryPref = "Liv Fix";
        public static MelonPreferences_Entry<bool> HideGameAvatar;
        
        public override void OnApplicationStart()
        {
            Patreon.Run(); // (●'◡'●)
            
            LoadConfig();
        }
        
        private static void LoadConfig()
        {
            var cat = MelonPreferences.GetCategory(CategoryPref);
            if (cat == null)
                cat = MelonPreferences.CreateCategory(CategoryPref);

            if ((HideGameAvatar = cat.GetEntry<bool>("HideGameAvatar")) == null)
                HideGameAvatar = (MelonPreferences_Entry<bool>)cat.CreateEntry<bool>("HideGameAvatar", true);
        }
    }

    [HarmonyPatch(typeof(LIV.SDK.Unity.LIV), "OnSDKActivate")]
    public class OnSDKActivate
    {
        static void Postfix(LIV.SDK.Unity.LIV __instance)
        {
            __instance._fixPostEffectsAlpha = false;
            // __instance._spectatorLayerMask = (__instance._spectatorLayerMask.value & ~(1 << 7)) | (0 << 7);
            __instance._spectatorLayerMask = (__instance._spectatorLayerMask.value & ~(1 << 5)) | (1 << 5);
        }
    }

    [HarmonyPatch(typeof(PlayerAvatar), "UpdateSkinRenderStates")]
    public class UpdateSkinRenderStates
    {
        static void Postfix(PlayerAvatar __instance)
        {
            if (__instance == null)
                return;
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

            if (LivFix.HideGameAvatar.Value || true)
            {
                properties.Container.Avatar.SkinObj.GetComponentInChildren<MeshRenderer>().gameObject.layer = 7;
                return;
            }
            
            var renderer = properties.Container.Avatar.SkinObj.GetComponentInChildren<MeshRenderer>();
            var parent = renderer.transform.parent;
            // var go = GameObject.Instantiate(renderer.gameObject, parent);

            string name = properties.Container.Avatar.activePresetName;
            
            AvatarSkinsData.Preset preset = properties.Container.Avatar.avatarSkins.enabledPresetCache[name];
            if (preset == null)
            {
                MelonLogger.Error($"Failed to get preset for {properties.Container.Avatar.activePresetName}");
                return;
            }

            if (!preset.Enabled)
            {
                MelonLogger.Error($"Preset {preset.Name} is not enabled");
                return;
            }
            
            MelonLogger.Msg($"{preset.Name} {preset.Enabled} {preset.ThirdPerson == null}");
            BigBoxAddressablesUtil.LoadAsync<GameObject>(preset.ThirdPerson, GoyfsHelper.ConvertAction<GameObject>(SpawnHead));
            
            MelonLogger.Msg(
                $"{properties.Container.Avatar.activePresetName}");
        }

        private static void SpawnHead(GameObject o)
        {
            LocalProperties properties = GoyfsHelper.Get<LocalProperties>();

            if (o.name == "BundleItem")
                return;
            MelonLogger.Msg($"Prefab {o.name}");
            var go = MemoryUtil.Instantiate<GameObject>(o, null, true);

            go.name = "Worked";
            go.transform.position = properties.Container.Avatar.SkinObj.transform.position + new Vector3(0, 0, -2f);

            MelonLogger.Msg("me");

            MelonLogger.Msg("v");
            try
            {
                BigBoxVR.AvatarSkinning.GetRootAndTransform(skin: null, dstRoot: out var dst, dstBones: out var dstBones);
                MelonLogger.Msg("1");
            }
            catch (Exception e)
            {
                MelonLogger.Error(e);
            }
            
            try
            {
                BigBoxVR.AvatarSkinning.GetRootAndTransform(skin: (UnityEngine.GameObject)go, dstRoot: out var dst, dstBones: out var dstBones);
                MelonLogger.Msg("2");
            }
            catch (Exception e)
            {
                MelonLogger.Error(e);
            }
            
            try
            {
                BigBoxVR.AvatarSkinning.GetRootAndTransform(skin: o, dstRoot: out var  dst, dstBones: out var dstBones);
                MelonLogger.Msg("3");
            }
            catch (Exception e)
            {
                MelonLogger.Error(e);
            }

            MelonLogger.Msg($"z");
            // AvatarSkinning.RemapBones(properties.Container.Avatar.dicBones, properties.Container.transform.root, ref dst, ref dstBones);

            // MelonLogger.Msg($"{dst.name}");
        }
        
    }
    
    
    [HarmonyPatch(typeof(BigBoxLODList), "Schedule")]
    public class Schedule
    {
        static void Prefix(BigBoxLODManager __instance, ref bool frustumCull)
        {
            frustumCull = false;
        }
    }
}