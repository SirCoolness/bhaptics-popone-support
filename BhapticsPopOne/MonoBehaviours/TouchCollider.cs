using System;
using System.Linq;
using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.EffectHelpers;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne
{
    public class TouchCollider : MonoBehaviour
    {
        public TouchCollider(System.IntPtr ptr) : base(ptr) {}
     
        public static float width = 0.115f;

        public static int Layer = 7; // punch

        public static string[] filter = new[]
        {
            "male_a_rig:spine_01",
            "male_a_rig:spine_02",
            "male_a_rig:spine_03",
            "male_a_rig:clavicle_l",
            "male_a_rig:clavicle_l",
        };
        
        private void OnTriggerEnter(Collider other)
        {
            // if (!filter.Contains(other.name))
                // return;
            
            // MelonLogger.Log($"{other.name}");
        }

        private void OnTriggerStay(Collider other)
        {
            if (!filter.Contains(other.name))
                return;
            
            var vestRef = Mod.Instance.Data.Players.VestReference();
            if (vestRef == null)
            {
                MelonLogger.LogWarning("Cant the reference transform for the vest.");
                return;
            }

            var direction = (transform.position - vestRef.position).normalized;
            var forward = Quaternion.Euler(0, -90f, 0) * vestRef.forward;
            var angle = -BhapticsUtils.Angle(forward, direction);

            var relativeY = transform.position.y - (vestRef.position.y + PatternManager.VestCenterOffset);
            var localizedY = Mathf.Clamp(relativeY / PatternManager.VestHeight, -0.5f, 0.5f);
            
            // MelonLogger.Log($"{Mathf.Clamp((transform.position.y - (vestRef.position.y + PatternManager.VestCenterOffset)) / PatternManager.VestHeight, -0.5f, 0.5f)} {transform.position.y -  (vestRef.position.y + PatternManager.VestCenterOffset)}");
            var effect = PatternManager.Effects["Vest/ReceiveTouch"];
            effect.Play(new Effect.EffectProperties
            {
                Strength = 0.25f,
                Time = Time.fixedDeltaTime,
                XRotation = angle,
                YOffset = localizedY
            });
        }

        public static void BindToTransform(Transform dest)
        {
            var exists = dest.Find("TouchCollider") != null;
            if (exists)
                return;
            
            var gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gameObject.name = "TouchCollider";
            gameObject.transform.localScale = new Vector3(width, width, width);

            gameObject.layer = Layer;

            gameObject.transform.parent = dest;
            gameObject.transform.localPosition = Vector3.zero;

            DestroyImmediate(gameObject.GetComponent<Rigidbody>());
            
            var collider = gameObject.GetComponent<SphereCollider>();
            collider.radius = width / 2f;
            collider.isTrigger = true;

            gameObject.AddComponent<TouchCollider>();

            var sphereProxy = gameObject.AddComponent<CustomPhysicsObjectProxy>();
            sphereProxy.UseBounce = false;
            sphereProxy.PhysicsObjectType = PhysicsObjectType.Dynamic;
            sphereProxy.ColliderUpdatePolicy = ColliderUpdatePolicy.All;
        }
    }
}