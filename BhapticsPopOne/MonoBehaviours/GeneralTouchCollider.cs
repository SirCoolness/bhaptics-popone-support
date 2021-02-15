using System;
using System.Collections.Generic;
using Bhaptics.Tact;
using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.EffectHelpers;
using BhapticsPopOne.Haptics.Patterns;
using MelonLoader;
using Unity.Mathematics;
using UnityEngine;

namespace BhapticsPopOne
{
    public class GeneralTouchCollider : MonoBehaviour
    {
        public GeneralTouchCollider(System.IntPtr ptr) : base(ptr) {}

        public static Dictionary<string, PositionType> filter = new Dictionary<string, PositionType>
        {
            ["male_a_rig:foot_l"] = PositionType.FootL, 
            ["male_a_rig:calf_l"] = PositionType.FootL, 
            ["male_a_rig:foot_r"] = PositionType.FootR, 
            ["male_a_rig:calf_r"] = PositionType.FootR, 
            ["male_a_rig:head"] = PositionType.Head
        };
        
        public VelocityTracker velocityTracker;

        private Dictionary<PositionType, HashSet<string>> activeParts = new Dictionary<PositionType, HashSet<string>>
        {
            [PositionType.Head] = new HashSet<string>(),
            [PositionType.FootL] = new HashSet<string>(),
            [PositionType.FootR] = new HashSet<string>(),
        };
        
        private Dictionary<PositionType, Action<float>> onEnterActionMap = new Dictionary<PositionType, Action<float>>();
        
        private Dictionary<PositionType, Action> onPlayActionMap = new Dictionary<PositionType, Action>();

        private void Awake()
        {
            onEnterActionMap[PositionType.Head] = EnterHeadEffect;
            onEnterActionMap[PositionType.FootL] = EnterFootLEffect;
            onEnterActionMap[PositionType.FootR] = EnterFootREffect;

            onPlayActionMap[PositionType.Head] = PlayHeadEffect;
            onPlayActionMap[PositionType.FootL] = PlayFootLEffect;
            onPlayActionMap[PositionType.FootR] = PlayFootREffect;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!filter.ContainsKey(other.name) 
                || other.transform.root != Mod.Instance.Data.Players.LocalPlayerContainer.Avatar.Rig
                )
                return;
            
            var active = activeParts[filter[other.name]];
            bool firstTime = active.Count == 0; 
                
            active.Add(other.name);
            
            if (!firstTime)
                return;
            
            var targetTracker = other.transform.root.GetComponent<VelocityTracker>();
            
            if (targetTracker == null)
                return;

            var targetVelocity = targetTracker.Velocity;
            var localVelocity = velocityTracker.Velocity;

            targetVelocity.y = 0;
            localVelocity.y = 0;
            
            var relativeVelocity = targetVelocity - localVelocity;

            var magnitude = Vector3.Magnitude(relativeVelocity);

            onEnterActionMap[filter[other.name]].Invoke(magnitude);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!filter.ContainsKey(other.name))
                return;

            if (
                !activeParts[filter[other.name]].Contains(other.name) 
                || other.transform.root != Mod.Instance.Data.Players.LocalPlayerContainer.Avatar.Rig
                )
                return;

            activeParts[filter[other.name]].Remove(other.name);
            
            if (activeParts[filter[other.name]].Count != 0)
                return;
        }

        private void FixedUpdate()
        {
            foreach (var keyValuePair in activeParts)
            {
                if (keyValuePair.Value.Count == 0)
                    continue;
                
                onPlayActionMap[keyValuePair.Key].Invoke();
            }
        }

        #region Actions

        private static void EnterHeadEffect(float magnitude)
        {
            var effect = PatternManager.Effects["Head/InitialTouch"];
            effect.Play(new Effect.EffectProperties
            {
                Strength = Math.Min(1f, magnitude / 1.5f)
            });
        }
        
        private static void EnterFootLEffect(float magnitude)
        {
            EnterFootEffect(magnitude, Handedness.Left);
        }
        
        private static void EnterFootREffect(float magnitude)
        {
            EnterFootEffect(magnitude, Handedness.Right);
        }

        private static void EnterFootEffect(float magnitude, Handedness foot)
        {
            if (magnitude > 1.35f)
            {
                var effect = PatternManager.Effects[$"Foot/HighVInitialTouch{HapticUtils.HandExt(foot)}"];
                effect.Play(new Effect.EffectProperties
                {
                    Strength = 0.5f + Mathf.Min(1f, ((magnitude - 1.35f) / 2.5f)) * 0.5f,
                });
            }
            else
            {
                var effect = PatternManager.Effects[$"Foot/InitialTouch{HapticUtils.HandExt(foot)}"];
                effect.Play(new Effect.EffectProperties
                {
                    Time = 0.275f,
                    Strength = Mathf.Min(1f, magnitude / 1.35f),
                });
            }
        }
        
        private static void PlayHeadEffect()
        {
            var effect = PatternManager.Effects["Head/ReceiveTouch"];
            effect.Play();
        }
        
        private static void PlayFootLEffect()
        {
            PlayFootEffect(Handedness.Left);
        }
        
        private static void PlayFootREffect()
        {
            PlayFootEffect(Handedness.Right);
        }

        private static void PlayFootEffect(Handedness foot)
        {
            var effect = PatternManager.Effects[$"Foot/ReceiveTouch{HapticUtils.HandExt(foot)}"];
            effect.Play();
        }

        #endregion

        public static void BindToTransform(Transform dest)
        {
            var exists = dest?.gameObject.GetComponent<GeneralTouchCollider>() != null;
            if (exists)
                return;
            
            
            var touchC = dest.gameObject.AddComponent<GeneralTouchCollider>();
            if (dest.GetComponent<VelocityTracker>() == null)
                touchC.velocityTracker = dest.gameObject.AddComponent<VelocityTracker>();
            else
                touchC.velocityTracker = dest.GetComponent<VelocityTracker>();
        }
    }
}