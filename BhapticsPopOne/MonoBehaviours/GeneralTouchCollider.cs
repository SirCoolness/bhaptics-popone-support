using System;
using System.Collections.Generic;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.EffectHelpers;
using BhapticsPopOne.Haptics.Patterns;
using MelonLoader;
using Unity.Mathematics;
using UnityEngine;
using Il2Cpp;

using PositionType = bHapticsLib.PositionID;

namespace BhapticsPopOne
{
    public class GeneralTouchCollider : MonoBehaviour
    {
        public GeneralTouchCollider(System.IntPtr ptr) : base(ptr) {}

        public static Dictionary<string, PositionType> filter = new Dictionary<string, PositionType>
        {
            ["male_a_rig:foot_l"] = PositionType.FootLeft, 
            ["male_a_rig:calf_l"] = PositionType.FootLeft, 
            ["male_a_rig:foot_r"] = PositionType.FootRight, 
            ["male_a_rig:calf_r"] = PositionType.FootRight, 
            ["male_a_rig:head"] = PositionType.Head
        };
        
        public VelocityTracker velocityTracker;

        private Dictionary<PositionType, HashSet<string>> activeParts = new Dictionary<PositionType, HashSet<string>>
        {
            [PositionType.Head] = new HashSet<string>(),
            [PositionType.FootLeft] = new HashSet<string>(),
            [PositionType.FootRight] = new HashSet<string>(),
        };
        
        private Dictionary<PositionType, Action<float>> onEnterActionMap = new Dictionary<PositionType, Action<float>>();
        
        private Dictionary<PositionType, Action> onPlayActionMap = new Dictionary<PositionType, Action>();

        private void Awake()
        {
            onEnterActionMap[PositionType.Head] = EnterHeadEffect;
            onEnterActionMap[PositionType.FootLeft] = EnterFootLEffect;
            onEnterActionMap[PositionType.FootRight] = EnterFootREffect;

            onPlayActionMap[PositionType.Head] = PlayHeadEffect;
            onPlayActionMap[PositionType.FootLeft] = PlayFootLEffect;
            onPlayActionMap[PositionType.FootRight] = PlayFootREffect;
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
            if (DynConfig.Toggles.Face.PlayerTouchVelocity)
                EffectPlayer.Play("Head/InitialTouch", new Effect.EffectProperties
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
            if (!DynConfig.Toggles.Feet.PlayerTouchVelocity)
                return;
            
            if (magnitude > 1.35f)
            {
                EffectPlayer.Play($"Foot/HighVInitialTouch{HapticUtils.HandExt(foot)}", new Effect.EffectProperties
                {
                    Strength = 0.5f + Mathf.Min(1f, ((magnitude - 1.35f) / 2.5f)) * 0.5f,
                });
            }
            else
            {
                EffectPlayer.Play($"Foot/InitialTouch{HapticUtils.HandExt(foot)}", new Effect.EffectProperties
                {
                    Time = 0.275f,
                    Strength = Mathf.Min(1f, magnitude / 1.35f),
                });
            }
        }
        
        private static void PlayHeadEffect()
        {
            if (DynConfig.Toggles.Face.PlayerTouch)
                EffectPlayer.Play("Head/ReceiveTouch");
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
            if (DynConfig.Toggles.Feet.PlayerTouch)
                EffectPlayer.Play($"Foot/ReceiveTouch{HapticUtils.HandExt(foot)}");
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