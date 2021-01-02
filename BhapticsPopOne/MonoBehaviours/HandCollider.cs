using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using BhapticsPopOne.Haptics.Patterns;
using MelonLoader;
using UnityEngine;
using UnhollowerRuntimeLib;

namespace BhapticsPopOne.MonoBehaviours
{
    public class HandCollider : MonoBehaviour
    {
        public static float width = 0.115f;
        public static float height = 0.25f;
        
        public HandCollider(System.IntPtr ptr) : base(ptr) {}

        public Handedness Hand;
        public uint OwnerID;

        public static int Layer = 19;
            

        private void OnTriggerEnter(Collider other)
        {
            // MelonLogger.Log(other.name);
            if (other?.transform == null)
                return;

            var target = other.transform.GetComponent<HighFiveTarget>();
            if (target != null)
                HandlePunch(other, target);
        }

        private void HandlePunch(Collider other, HighFiveTarget target)
        {
            // MelonLogger.Log(other.name);

            if (target.Hand == Hand && target.OwnerID == OwnerID)
                return;

            var owner = PlayerContainer.Find(OwnerID);
            var otherOwner = PlayerContainer.Find(target.OwnerID);
            
            if (otherOwner == null || owner == null)
                return;
            
            if (owner.Inventory.NetworkequipIndex != 0)
                return;
            

            MelonLogger.Log($"High Five: [{owner.Data.DisplayName} {Hand.ToString()}] -> [{otherOwner.Data.DisplayName} {target.Hand}]");
            if (!owner.isLocalPlayer)
                return;
            
            HighFive.Execute(Hand);
        }

        public static void BindToTransform(Transform dest, Handedness hand, uint netId)
        {
            var exists = dest.Find("HandCollider") != null;
            if (exists)
                return;
            
            // var punch = dest.GetComponentInParent<Punch>();
            // if (punch == null)
            //     return;
            
            var gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gameObject.name = "HandCollider";
            gameObject.transform.localScale = new Vector3(width, width, width);

            gameObject.layer = Layer;

            gameObject.transform.parent = dest;
            gameObject.transform.localPosition = Vector3.zero;
            // gameObject.transform.position = dest.position;

            // var marker = gameObject.AddComponent<DebugMarker>();
            // marker._size = width;
            // marker.SetSize();
            
            DestroyImmediate(gameObject.GetComponent<Rigidbody>());
            
            var collider = gameObject.GetComponent<SphereCollider>();
            // collider.height = HandCollider.height;
            collider.radius = width / 2f;
            collider.isTrigger = true;

            var handCollider = gameObject.AddComponent<HandCollider>();
            handCollider.Hand = hand;
            handCollider.OwnerID = netId;

            // var rb = gameObject.AddComponent<Rigidbody>();
            // rb.isKinematic = true;
            // rb.useGravity = false;
            
            var sphereProxy = gameObject.AddComponent<CustomPhysicsObjectProxy>();
            sphereProxy.UseBounce = false;
            sphereProxy.PhysicsObjectType = PhysicsObjectType.Dynamic;
            sphereProxy.ColliderUpdatePolicy = ColliderUpdatePolicy.All;

            var meshRenderer = gameObject.GetComponent<MeshRenderer>();
            // meshRenderer.material = null;
            
            AddRB(dest, hand, netId);
        }

        private static void AddRB(Transform dest, Handedness hand, uint netId)
        {
            var handRB = new GameObject("HandRB");

            handRB.transform.localScale = new Vector3(width, width, width);
            
            handRB.layer = Layer;

            handRB.transform.parent = dest;
            handRB.transform.localPosition = Vector3.zero;


            var collider = handRB.AddComponent<SphereCollider>();
            // collider.height = HandCollider.height;

            collider.radius = width / 2f;
            collider.isTrigger = false;
            
            var rb = handRB.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.detectCollisions = false;
            
            var sphereProxy = handRB.AddComponent<CustomPhysicsObjectProxy>();
            sphereProxy.UseBounce = false;
            sphereProxy.PhysicsObjectType = PhysicsObjectType.Dynamic;
            sphereProxy.ColliderUpdatePolicy = ColliderUpdatePolicy.All;

            var target = handRB.AddComponent<HighFiveTarget>();
            target.Hand = hand;
            target.OwnerID = netId;
            
            BattleRoyaleExtensions.DrawBounds(collider.bounds, Color.blue);
        }
    }
}