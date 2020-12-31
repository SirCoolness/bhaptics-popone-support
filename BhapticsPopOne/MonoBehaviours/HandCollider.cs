using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MelonLoader;
using UnityEngine;
using UnhollowerRuntimeLib;

namespace BhapticsPopOne.MonoBehaviours
{
    public class HandCollider : MonoBehaviour
    {
        public static float width = 0.1f;
        public static float height = 0.25f;
        
        public HandCollider(System.IntPtr ptr) : base(ptr) {}

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
            var otherPunch = other.transform.GetComponentInParent<Punch>();
            if (otherPunch != null)
                HandlePunch(other, otherPunch);
        }

        private void HandlePunch(Collider other, Punch otherPunch)
        {
            MelonLogger.Log($"High Five: [{Punch.netIdentity.netId} {Punch.handedness.ToString()}] [{otherPunch.netIdentity.netId} {otherPunch.handedness.ToString()}]");
        }

        public static void BindToTransform(Transform dest)
        {
            var exists = dest.Find("HandCollider_Test") != null;
            if (exists)
                return;
            
            var punch = dest.GetComponentInParent<Punch>();
            if (punch == null)
                return;
            
            var gameObject = new GameObject("HandCollider_Test");
        
            gameObject.transform.parent = dest;
            gameObject.transform.localPosition = Vector3.zero;
            // gameObject.transform.position = dest.position;

            var marker = gameObject.AddComponent<DebugMarker>();
            marker._size = width;
            marker.SetSize();
            
            var collider = gameObject.AddComponent<SphereCollider>();
            // collider.height = HandCollider.height;
            collider.radius = width;
            collider.isTrigger = true;

            gameObject.AddComponent<HandCollider>();
            
            var rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
            
            var sphereProxy = gameObject.AddComponent<CustomPhysicsObjectProxy>();
            sphereProxy.UseBounce = false;
            sphereProxy.PhysicsObjectType = PhysicsObjectType.Dynamic;
            sphereProxy.ColliderUpdatePolicy = ColliderUpdatePolicy.All;
        }
    }
}