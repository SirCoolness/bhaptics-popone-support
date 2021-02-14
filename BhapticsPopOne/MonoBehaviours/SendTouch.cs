using System;
using System.Collections.Generic;
using Aws.GameLift.Server.Model;
using BhapticsPopOne.Haptics;
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
            MelonLogger.Log(ConsoleColor.Magenta, other.name);
            if (activeParts.Contains(other.gameObject.GetInstanceID()) 
                || other.transform.root.GetComponent<DamageableSkeleton>() == null
                || Mod.Instance.Data.Players.LocalPlayerContainer.Avatar.Rig == other.transform.root)
                return;

            activeParts.Add(other.gameObject.GetInstanceID());
            
            if (activeParts.Count > 1)
                return;
            
            PatternManager.Effects[$"Arm/SendInitialTouch{HapticUtils.HandExt(hand)}"]?.Play();
            EffectLoopRegistry.Start($"Arm/SendTouch{HapticUtils.HandExt(hand)}");
        }

        private void OnTriggerExit(Collider other)
        {
            if (activeParts.Count <= 0)
                return;
            
            activeParts.Remove(other.gameObject.GetInstanceID());
            
            if (activeParts.Count <= 0)
                EffectLoopRegistry.Stop($"Arm/SendTouch{HapticUtils.HandExt(hand)}");

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