using System;
using System.Collections.Generic;
using System.Linq;
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
            // MelonLogger.Log(other.name);
            
            if (other.transform.GetComponent<HighFiveTarget>() != null)
                HandlePunch(other);
        }

        private void HandlePunch(Collider other)
        {
            var otherPunch = other.GetComponentInParent<Punch>();
            
            MelonLogger.Log(other.name);
            MelonLogger.Log($"High Five: [{Punch.netIdentity.netId} {Punch.handedness.ToString()}] [{otherPunch.netIdentity.netId} {otherPunch.handedness.ToString()}]");
        }

        public static void BindToTransform(Transform dest)
        {
            var exists = dest.Find("HandCollider") != null;
            if (exists)
                return;
            
            var punch = dest.GetComponentInParent<Punch>();
            if (punch == null)
                return;
            
            var gameObject = new GameObject("HandCollider");

            gameObject.layer = 3;

            gameObject.transform.parent = dest;
            gameObject.transform.localPosition = Vector3.zero;
            // gameObject.transform.position = dest.position;

            var marker = gameObject.AddComponent<DebugMarker>();
            marker._size = width;
            marker.SetSize();
            
            var collider = gameObject.AddComponent<SphereCollider>();
            // collider.height = HandCollider.height;
            collider.radius = width / 2f;
            collider.isTrigger = true;

            gameObject.AddComponent<HandCollider>();

            // var rb = gameObject.AddComponent<Rigidbody>();
            // rb.isKinematic = true;
            // rb.useGravity = false;
            
            var sphereProxy = gameObject.AddComponent<CustomPhysicsObjectProxy>();
            sphereProxy.UseBounce = false;
            sphereProxy.PhysicsObjectType = PhysicsObjectType.Dynamic;
            sphereProxy.ColliderUpdatePolicy = ColliderUpdatePolicy.All;
            
            AddRB(dest);
            TestMesh(dest);
        }

        private static void AddRB(Transform dest)
        {
            var handRB = new GameObject("HandRB");

            handRB.layer = 3;
            
            handRB.transform.parent = dest;
            handRB.transform.localPosition = Vector3.zero;
            
            var collider = handRB.AddComponent<SphereCollider>();
            // collider.height = HandCollider.height;
            collider.radius = width / 2f;
            collider.isTrigger = false;
            
            var rb = handRB.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
            
            var sphereProxy = handRB.AddComponent<CustomPhysicsObjectProxy>();
            sphereProxy.UseBounce = false;
            sphereProxy.PhysicsObjectType = PhysicsObjectType.Dynamic;
            sphereProxy.ColliderUpdatePolicy = ColliderUpdatePolicy.All;

            handRB.AddComponent<HighFiveTarget>();
        }

        private static void TestMesh(Transform dest)
        {
            var gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            gameObject.transform.parent = dest;
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            
            // var materials = Resources.FindObjectsOfTypeAll<Material>();
            // Resources.Load<Material>("MainMenu--Batched");


            var meshRenderer = gameObject.GetComponent<MeshRenderer>();
            meshRenderer.material = Resources.Load<Material>("MainMenu--Batched");
        }
        
    }
}