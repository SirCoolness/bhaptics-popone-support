﻿using BhapticsPopOne.Haptics.EffectHelpers;
using MelonLoader;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class KatanaShield
    {
        private static bool Active = false;
        private static bool WasPlaying = false;
        
        public static void Execute(bool active)
        {
            Active = active;
            
            if (!Active && WasPlaying)
            {
                EffectPlayer.Stop("Arm/MeleeShield");
                EffectPlayer.Stop("Hand/MeleeShield");
                WasPlaying = false;
            }
        }

        public static void Block()
        {
            EffectPlayer.Play("Arm/MeleeBlock", new Effect.EffectProperties
            {
                Strength = 1f,
                Time = 0.08f
            });
            
            EffectPlayer.Play("Hand/MeleeBlock", new Effect.EffectProperties
            {
                Strength = 1f,
                Time = 0.08f
            });
        }

        public static void FixedUpdate()
        {
            if (!Active)
                return;

            EffectPlayer.Play("Arm/MeleeShield", new Effect.EffectProperties
            {
                Strength = 0.15f
            });
            EffectPlayer.Play("Hand/MeleeShield", new Effect.EffectProperties
            {
                Strength = 0.15f
            });
            
            WasPlaying = true;
        }
    }
}