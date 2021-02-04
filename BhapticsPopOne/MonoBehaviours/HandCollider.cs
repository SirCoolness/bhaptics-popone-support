using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Aws.GameLift.Server.Model;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.Patterns;
using MelonLoader;
using UnityEngine;
using UnhollowerRuntimeLib;

namespace BhapticsPopOne.MonoBehaviours
{
    public class HandCollider : MonoBehaviour
    {
        public static float width = 0.115f;
        
        public HandCollider(System.IntPtr ptr) : base(ptr) {}

        public Handedness Hand;
        public uint OwnerID;

        public static int Layer = 19; // punch

        private void OnTriggerEnter(Collider other)
        {
            if (other?.transform == null)
                return;

            var target = other.transform.GetComponent<HandCollider>();
            if (target != null)
                HandlePunch(other, target);
        }

        private void HandlePunch(Collider other, HandCollider target)
        {
            if (target.Hand == Hand && target.OwnerID == OwnerID)
                return;

            var owner = PlayerContainer.Find(OwnerID);
            var otherOwner = PlayerContainer.Find(target.OwnerID);
            
            if (otherOwner == null || owner == null)
                return;
            
            if (owner.Inventory.NetworkequipIndex != 0)
                return;
            
            if (!owner.isLocalPlayer)
                return;
            
            if (!ConfigLoader.Config.Clapping && otherOwner.isLocalPlayer)
                return;
            
            if (!ConfigLoader.Config.HighFive && owner != otherOwner)
                return;
            
            HighFive.Execute(Hand);
            MelonLogger.Log($"Playing high five {Hand.ToString()}");
        }

        public static void BindToTransform(Transform dest, Handedness hand, uint netId)
        {
            var exists = dest.gameObject.GetComponent<HandCollider>() != null;
            if (exists)
                return;

            var handCollider = dest.gameObject.AddComponent<HandCollider>();
            handCollider.Hand = hand;
            handCollider.OwnerID = netId;
            
            // var gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            // gameObject.name = "HandCollider";
            // gameObject.transform.localScale = new Vector3(width, width, width);

            // gameObject.layer = Layer;

            // gameObject.transform.parent = dest;
            // gameObject.transform.localPosition = Vector3.zero;

            // DestroyImmediate(gameObject.GetComponent<Rigidbody>());

            // var collider = gameObject.GetComponent<SphereCollider>();
            // collider.radius = width / 2f;
            // collider.isTrigger = true;

            // var handCollider = gameObject.AddComponent<HandCollider>();
            // handCollider.Hand = hand;
            // handCollider.OwnerID = netId;

            // var sphereProxy = gameObject.AddComponent<CustomPhysicsObjectProxy>();
            // sphereProxy.UseBounce = false;
            // sphereProxy.PhysicsObjectType = PhysicsObjectType.Dynamic;
            // sphereProxy.ColliderUpdatePolicy = ColliderUpdatePolicy.All;

            // if (ConfigLoader.Config.ShowHighFiveRegion && PlayerContainer.Find(netId)?.isLocalPlayer == true)
            // {
            // var meshRenderer = gameObject.GetComponent<MeshRenderer>();
            // meshRenderer.material = null;
            // }

            // AddRB(dest, hand, netId);
        }

        // private static void AddRB(Transform dest, Handedness hand, uint netId)
        // {
        //     var handRB = new GameObject("HandRB");
        //
        //     handRB.transform.localScale = new Vector3(width, width, width);
        //     
        //     handRB.layer = Layer;
        //
        //     handRB.transform.parent = dest;
        //     handRB.transform.localPosition = Vector3.zero;
        //
        //
        //     var collider = handRB.AddComponent<SphereCollider>();
        //
        //     collider.radius = width / 2f;
        //     collider.isTrigger = false;
        //     
        //     var rb = handRB.AddComponent<Rigidbody>();
        //     rb.isKinematic = true;
        //     rb.useGravity = false;
        //     rb.detectCollisions = false;
        //     
        //     var sphereProxy = handRB.AddComponent<CustomPhysicsObjectProxy>();
        //     sphereProxy.UseBounce = false;
        //     sphereProxy.PhysicsObjectType = PhysicsObjectType.Dynamic;
        //     sphereProxy.ColliderUpdatePolicy = ColliderUpdatePolicy.All;
        //
        //     var target = handRB.AddComponent<HighFiveTarget>();
        //     target.Hand = hand;
        //     target.OwnerID = netId;
        //     
        //     BattleRoyaleExtensions.DrawBounds(collider.bounds, Color.blue);
        // }
    }
}