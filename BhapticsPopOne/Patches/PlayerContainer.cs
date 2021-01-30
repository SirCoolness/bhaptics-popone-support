using System;
using BhapticsPopOne.Data;
using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.Patterns;
using BhapticsPopOne.MonoBehaviours;
using BigBoxVR.BattleRoyale.Models.Shared;
using Harmony;
using MelonLoader;
using Mirror;
using UnityEngine;

namespace BhapticsPopOne.Patches.PlayerContainer2
{
    [HarmonyPatch(typeof(PlayerContainer), "HandlePlayerHit")]
    public class HandlePlayerHit
    {
        static void Prefix(PlayerContainer __instance, DamageableHitInfo info)
        {
            if (__instance != Mod.Instance.Data.Players.LocalPlayerContainer)
                return;

            if (info.Source == HitSourceCategory.Bot || info.Source == HitSourceCategory.Player)
            {
                PlayerHit.Execute(info);
            }
            else if (info.Source == HitSourceCategory.BattleZone)
                PatternManager.ZoneHit();
            else if (info.Source == HitSourceCategory.Falling)
                FallDamage.Execute(-info.Damage, info.Power);
            else
                PatternManager.TestPattern();

            if (info.ArmorBroke)
            {
                PatternManager.ShieldBreak();
            }
        }
    }

    [HarmonyPatch(typeof(PlayerContainer), "CmdStartFistBump")]
    public class CmdStartFistBump
    {
        static void Prefix(PlayerContainer __instance, Handedness handedness)
        {
            if (!__instance.isLocalPlayer)
                return;

            if (handedness == Handedness.Right)
                FistBump.lastPunchhandR = handedness;
            else if (handedness == Handedness.Left)
                FistBump.lastPunchhandL = handedness;
        }
    }
    
    [HarmonyPatch(typeof(PlayerContainer), "CmdStopFistBump")]
    public class CmdStopFistBump
    {
        static void Prefix(PlayerContainer __instance, Handedness handedness)
        {
            if (!__instance.isLocalPlayer)
                return;

            if (handedness == Handedness.Right)
                FistBump.lastPunchhandR = Handedness.Unknown;
            else if (handedness == Handedness.Left)
                FistBump.lastPunchhandL = Handedness.Unknown;
        }
    }
    
    [HarmonyPatch(typeof(PlayerContainer), "OnContainerComponentReady")]
    public class OnContainerComponentReady
    {
        static void Postfix(PlayerContainer __instance)
        {
            if (__instance.transform.root != __instance.transform || __instance.Avatar?.Rig == null || !__instance.Data.IsReady)
                return;
            
            // MelonLogger.Log($"OnContainerComponentReady {Logging.StringifyVector3(__instance.Avatar.HandLeftAttachPoint.transform.position)} {__instance.Data.DisplayName} {__instance.Avatar.IsAvatarReady}");
            HandCollider.BindToTransform(__instance.Avatar.HandLeftAttachPoint, Handedness.Left, __instance.netId);
            HandCollider.BindToTransform(__instance.Avatar.HandRightAttachPoint, Handedness.Right, __instance.netId);
            
            DestructibleCollisionHelp.BindToTransform(__instance.Avatar.HandLeft, Handedness.Left, __instance.netId);
            DestructibleCollisionHelp.BindToTransform(__instance.Avatar.HandRight, Handedness.Right, __instance.netId);
            
            // TouchCollider.BindToTransform(__instance.Avatar.HandLeft);
            // TouchCollider.BindToTransform(__instance.Avatar.HandRight);
        }
    }
}
