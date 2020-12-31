using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using BhapticsPopOne.Haptics.Patterns;
using MelonLoader;
using UnityEngine;
using UnhollowerRuntimeLib;

namespace BhapticsPopOne.MonoBehaviours
{
    public class DestructibleCollisionHelp : MonoBehaviour
    {
        public DestructibleCollisionHelp(System.IntPtr ptr) : base(ptr) {}

        private Punch _punch;

        private Punch Punch
        {
            get
            {
                if (_punch != null)
                    return _punch;

                _punch = GetComponentInParent<Punch>();
                return _punch;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var destructible = other.GetComponentInParent<DestructibleSceneItem>();
            if (destructible != null)
                HandleBreak(other, destructible);
        }

        private void HandleBreak(Collider other, DestructibleSceneItem destructibleSceneItem)
        {
            MelonLogger.Log("Destructible hit");
            if (destructibleSceneItem == null)
            {
                MelonLogger.LogWarning("cant find scene item");
                return;
            }

            if (PlayerContainer.Find(Punch.netIdentity.netId)?.isLocalPlayer != true)
            {
                MelonLogger.Log("not local");
                return;
            }

            var handHelper = Mod.Instance.Data.Players.LocalHandHelper;

            if (!handHelper.brokenDestructibles.ContainsKey(destructibleSceneItem.SceneId))
            {
                handHelper.brokenDestructibles.Add(destructibleSceneItem.SceneId, new List<Handedness>());
                MelonLogger.Log("adding");
            }
            
            handHelper.brokenDestructibles[destructibleSceneItem.SceneId].Add(Punch.handedness);
            MelonLogger.Log("punch added");
        }
        
        public static void BindToTransform(Transform dest)
        {
            if (dest.GetComponent<DestructibleCollisionHelp>() != null)
                return;
            
            var punch = dest.GetComponent<Punch>();
            if (punch == null)
                return;

            dest.gameObject.AddComponent<DestructibleCollisionHelp>();
        }
    }
}