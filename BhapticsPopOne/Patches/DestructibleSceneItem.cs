﻿using Harmony;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne.DestructibleSceneItem2
{
    [HarmonyPatch(typeof(DestructibleSceneItem), "GetHit")]
    public class GetHit
    {
        // using postfix to detect if destroyed 
        static void Prefix(DestructibleSceneItem __instance, DamageableHitInfo info, Collider impactCollider)
        {
            if (impactCollider == null)
            {
                return;
            }

            MelonLogger.Log($"[DESTROY] {__instance.name} {info.Damage} {__instance.IsDestroyed} {impactCollider.name}");
        }
        
        // // using postfix to detect if destroyed 
        // static void Postfix(DestructibleSceneItem __instance, DamageableHitInfo info, Collider impactCollider)
        // {
        //     MelonLogger.Log($"[DESTROY] [POST] {__instance.name} {info.Damage} {__instance.IsDestroyed}");
        // }
    }
}