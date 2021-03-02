using System;
using System.Collections.Generic;
using Aws.GameLift.Server.Model;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.EffectHelpers;
using BhapticsPopOne.Haptics.EffectManagers;
using BhapticsPopOne.Haptics.Patterns;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne
{
    public class SendTouch : MonoBehaviour
    {
        public SendTouch(System.IntPtr ptr) : base(ptr) {}

        public VelocityTracker velocityTracker;
        
        private HashSet<int> activeParts = new HashSet<int> {};
        public Handedness hand;

        private void OnTriggerEnter(Collider other)
        {
            if (activeParts.Contains(other.gameObject.GetInstanceID()) 
                || other?.transform?.root.GetComponent<DamageableSkeleton>() == null
                || Mod.Instance.Data.Players.LocalPlayerContainer.Avatar.Rig == other.transform.root)
                return;

            bool firstTime = activeParts.Count == 0;
            
            activeParts.Add(other.gameObject.GetInstanceID());
            
            if (!firstTime)
                return;
            
            var targetTracker = other.transform.root.GetComponent<VelocityTracker>();

            if (targetTracker == null)
                return;

            var targetVelocity = targetTracker.Velocity;
            var localVelocity = velocityTracker.Velocity;

            var relativeVelocity = targetVelocity - localVelocity;

            var magnitude = Vector3.Magnitude(relativeVelocity);

            if (magnitude > 1.35f)
            {
                if (ConfigLoader.Config.EffectToggles.Arms.PlayerTouchVelocity)
                    EffectPlayer.Play($"Arm/HighVSendInitialTouch{HapticUtils.HandExt(hand)}", new Effect.EffectProperties
                    {
                        Strength = 0.5f + Mathf.Min(1f, ((magnitude - 1.35f) / 2.5f)) * 0.5f,
                        OnComplete = OnCompleteArm
                    }); 
                
                EffectPlayer.Play($"Hand/HighVSendInitialTouch{HapticUtils.HandExt(hand)}", new Effect.EffectProperties
                {
                    Strength = 0.5f + Mathf.Min(1f, ((magnitude - 1.35f) / 2.5f)) * 0.5f,
                    OnComplete = OnCompleteHand
                }); 
            }
            else
            {
                if (ConfigLoader.Config.EffectToggles.Arms.PlayerTouchVelocity)
                    EffectPlayer.Play($"Arm/SendInitialTouch{HapticUtils.HandExt(hand)}", new Effect.EffectProperties
                    {
                        Time = 0.275f,
                        Strength = Mathf.Min(1f, magnitude / 1.35f),
                        OnComplete = OnCompleteArm
                    });
                
                EffectPlayer.Play($"Hand/SendInitialTouch{HapticUtils.HandExt(hand)}", new Effect.EffectProperties
                {
                    Time = 0.275f,
                    Strength = Mathf.Min(1f, magnitude / 1.35f),
                    OnComplete = OnCompleteHand
                });
            }
        }

        private void OnCompleteArm()
        {
            if (!ConfigLoader.Config.EffectToggles.Arms.PlayerTouch)
                return;
            
            if (activeParts.Count > 0)
                EffectLoopRegistry.Start($"Arm/SendTouch{HapticUtils.HandExt(hand)}");
        }
        
        private void OnCompleteHand()
        {
            if (activeParts.Count > 0)
                EffectLoopRegistry.Start($"Hand/SendTouch{HapticUtils.HandExt(hand)}");
        }

        private void OnTriggerExit(Collider other)
        {
            if (activeParts.Count <= 0)
                return;
            
            activeParts.Remove(other.gameObject.GetInstanceID());

            if (activeParts.Count <= 0)
            {
                EffectLoopRegistry.Stop($"Arm/SendTouch{HapticUtils.HandExt(hand)}");
                EffectLoopRegistry.Stop($"Hand/SendTouch{HapticUtils.HandExt(hand)}");
            }
        }

        public static void BindToTransform(Transform dest, Handedness hand)
        {
            var exists = dest?.gameObject.GetComponent<SendTouch>() != null;
            if (exists)
                return;
            
            var sendTouch = dest.gameObject.AddComponent<SendTouch>();
            sendTouch.hand = hand;
            if (dest.GetComponent<VelocityTracker>() == null)
                sendTouch.velocityTracker = dest.gameObject.AddComponent<VelocityTracker>();
            else
                sendTouch.velocityTracker = dest.GetComponent<VelocityTracker>();
        }
    }
}