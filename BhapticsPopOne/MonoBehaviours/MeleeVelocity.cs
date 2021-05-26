using System;
using BhapticsPopOne.Haptics.Patterns;
using MelonLoader;
using UnityEngine;

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
        }
    }
}