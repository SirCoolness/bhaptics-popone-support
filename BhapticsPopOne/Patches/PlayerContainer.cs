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

            // var left = __instance.Avatar.HandLeft;
            // var right = __instance.Avatar.HandRight;

            // var lp = left.GetComponent<CustomPhysicsObjectProxy>();
            // var rp = right.GetComponent<CustomPhysicsObjectProxy>();
            //
            // MonoBehaviour.Destroy(lp);
            // MonoBehaviour.Destroy(rp);
            //
            // var lc = left.GetComponent<CapsuleCollider>();
            // var rc = right.GetComponent<CapsuleCollider>();
            //
            // if (lc != null)
            // {
            //     // var bounds = lc.bounds;
            //     var center = lc.center;
            //     var size = new Vector3(lc.radius, lc.height, lc.radius);
            //     float radius = lc.radius;
            //     
            //     MonoBehaviour.Destroy(lc);
            //
            //     var boxC = left.gameObject.AddComponent<BoxCollider>();
            //     boxC.size = size;
            //     // boxC.radius = radius;
            //     boxC.center = center;
            //     boxC.isTrigger = true;
            // }
            //
            // if (rc != null)
            // {
            //     var center = rc.center;
            //     var size = new Vector3(rc.radius, rc.height, rc.radius);
            //     float radius = lc.radius;
            //     
            //     MonoBehaviour.Destroy(rc);
            //
            //     var boxC = right.gameObject.AddComponent<BoxCollider>();
            //     boxC.size = size;
            //     // boxC.radius = radius;
            //     boxC.center = center;
            //     boxC.isTrigger = true;
            // }
            //
            // left.gameObject.AddComponent<CustomPhysicsObjectProxy>();
            // right.gameObject.AddComponent<CustomPhysicsObjectProxy>();

            // MelonLogger.Log($"OnContainerComponentReady {Logging.StringifyVector3(__instance.Avatar.HandLeftAttachPoint.transform.position)} {__instance.Data.DisplayName} {__instance.Avatar.IsAvatarReady}");
            // HandCollider.BindToTransform(left, Handedness.Left, __instance.netId);
            // HandCollider.BindToTransform(right, Handedness.Right, __instance.netId);
            //
            // DestructibleCollisionHelp.BindToTransform(left, Handedness.Left, __instance.netId);
            // DestructibleCollisionHelp.BindToTransform(right, Handedness.Right, __instance.netId);
            //
            // TouchCollider.BindToTransform(left);
            // TouchCollider.BindToTransform(right);
            AddHandReference.AddHandsToPlayer(__instance);
        }
    }
}
