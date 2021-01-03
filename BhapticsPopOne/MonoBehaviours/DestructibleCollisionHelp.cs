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

        public Handedness Hand;
        public uint OwnerID;
        
        private void OnTriggerEnter(Collider other)
        {
            var destructible = other.GetComponentInParent<DestructibleSceneItem>();
            if (destructible != null)
                HandleBreak(other, destructible);
        }

        private void HandleBreak(Collider other, DestructibleSceneItem destructibleSceneItem)
        {
            // MelonLogger.Log("Destructible hit");
            if (destructibleSceneItem == null)
            {
                // MelonLogger.LogWarning("cant find scene item");
                return;
            }

            var Player = PlayerContainer.Find(OwnerID);
            if (Player?.isLocalPlayer != true)
            {
                // MelonLogger.Log("not local");
                return;
            }

            var handHelper = Mod.Instance.Data.Players.LocalHandHelper;

            if (!handHelper.brokenDestructibles.ContainsKey(destructibleSceneItem.SceneId))
            {
                handHelper.brokenDestructibles.Add(destructibleSceneItem.SceneId, new List<Handedness>());
                // MelonLogger.Log("adding");
            }
            
            handHelper.brokenDestructibles[destructibleSceneItem.SceneId].Add(Hand);
            // MelonLogger.Log("punch added");
        }
        
        public static void BindToTransform(Transform dest, Handedness hand, uint netId)
        {
            if (dest.GetComponent<DestructibleCollisionHelp>() != null)
                return;
            
            var punch = dest.GetComponent<Punch>();
            if (punch == null)
                return;

            var helper = dest.gameObject.AddComponent<DestructibleCollisionHelp>();
            helper.Hand = hand;
            helper.OwnerID = netId;
        }
    }
}