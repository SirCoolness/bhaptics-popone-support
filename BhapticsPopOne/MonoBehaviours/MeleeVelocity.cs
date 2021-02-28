using System;
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
        }
    }
}