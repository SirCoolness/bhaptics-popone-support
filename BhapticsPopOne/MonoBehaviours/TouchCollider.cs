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
            "male_a_rig:clavicle_l",
        };
        
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
            var exists = dest.gameObject.GetComponent<TouchCollider>() != null;
            if (exists)
                return;
            
            dest.gameObject.AddComponent<TouchCollider>();
        }
    }
}