using System;
using BhapticsPopOne.Haptics.Patterns;
using BhapticsPopOne.Helpers;
using MelonLoader;
using UnityEngine;
using Il2Cpp;

namespace BhapticsPopOne
{
    public class MeleeVelocity : MonoBehaviour
    {
        public MeleeVelocity(System.IntPtr ptr) : base(ptr) {}

        public VelocityTracker Target;
        public Handedness Hand;
        
        void FixedUpdate()
        {
            Haptics.Patterns.MeleeVelocity.Execute(Hand, Target.Velocity);
            
            Shaker.HandVelocity(Hand, Target.Velocity);
            Harmonica.HandVelocity(Hand, Target.Velocity);
        }
    }
}