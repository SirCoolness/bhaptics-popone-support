using System;
using System.Collections.Generic;
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

        public static HashSet<string> filter = new HashSet<string>
        {
            "male_a_rig:spine_01",
            "male_a_rig:spine_02",
            "male_a_rig:spine_03",
            "male_a_rig:clavicle_l",
            "male_a_rig:clavicle_r",
        };

        public VelocityTracker velocityTracker;
        
        private HashSet<string> activeParts = new HashSet<string> {};
        private bool TargetEntered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!filter.Contains(other.name) || other.transform.root != Mod.Instance.Data.Players.LocalPlayerContainer.Avatar.Rig)
                return;
            
            activeParts.Add(other.name);
         
            if (TargetEntered)
                return;
            
            TargetEntered = true;
            
            var targetTracker = other.transform.root.GetComponent<VelocityTracker>();

            if (targetTracker == null)
                return;

            var targetVelocity = targetTracker.Velocity;
            var localVelocity = velocityTracker.Velocity;

            targetVelocity.y = 0;
            localVelocity.y = 0;
            
            var relativeVelocity = targetVelocity - localVelocity;

            var magnitude = Vector3.Magnitude(relativeVelocity);
            
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
            
            var effect = PatternManager.Effects["Vest/InitialTouch"];
            effect.Play(new Effect.EffectProperties
            {
                Strength = Math.Min(1f, magnitude / 4f),
                // Strength = 1f,
                // Time = 0.1f + (Math.Min(1f, magnitude / 0.2f) * 0.15f),
                Time = 1f,
                XRotation = angle,
                YOffset = localizedY
            });
        }

        private void OnTriggerExit(Collider other)
        {
            if (!TargetEntered)
                return;
            
            if (!activeParts.Contains(other.name) || other.transform.root != Mod.Instance.Data.Players.LocalPlayerContainer.Avatar.Rig)
                return;

            activeParts.Remove(other.name);
            
            if (activeParts.Count != 0)
                return;

            TargetEntered = false;
        }
        
        private void FixedUpdate()
        {
            if (!TargetEntered || activeParts.Count == 0)
                return;
            
            var vestRef = Mod.Instance.Data.Players.VestReference();
            if (!Mod.Instance.Data.Players.FoundVestRef)
                return;
            
            var direction = (transform.position - vestRef.position).normalized;
            var forward = Quaternion.Euler(0, -90f, 0) * vestRef.forward;
            var angle = -BhapticsUtils.Angle(forward, direction);

            var relativeY = transform.position.y - (vestRef.position.y + PatternManager.VestCenterOffset);
            var localizedY = Mathf.Clamp(relativeY / PatternManager.VestHeight, -0.5f, 0.5f);
            
            PatternManager.Effects["Vest/ReceiveTouch"].Play(new Effect.EffectProperties
            {
                Strength = 0.15f,
                Time = Time.fixedDeltaTime,
                XRotation = angle,
                YOffset = localizedY
            });
        }

        public static void BindToTransform(Transform dest)
        {
            var exists = dest?.gameObject.GetComponent<TouchCollider>() != null;
            if (exists)
                return;
            
            var touchC = dest.gameObject.AddComponent<TouchCollider>();
            if (dest.GetComponent<VelocityTracker>() == null)
                touchC.velocityTracker = dest.gameObject.AddComponent<VelocityTracker>();
            else
                touchC.velocityTracker = dest.GetComponent<VelocityTracker>();
        }
    }
}