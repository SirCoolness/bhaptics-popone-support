using System;
using System.Collections.Generic;
using System.Linq;
using Bhaptics.Tact;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using BhapticsPopOne.Haptics.EffectManagers;
using MelonLoader;
using UnityEngine;
using Random = System.Random;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class FallingAir
    {
        private static float BaselineVelocity = 0f;
        private static float TargetMultiplier = 1f;
        
        private static ProceduralEffect EffectManager = new ProceduralEffect
        {
            EffectPrefix = "Vest/FallingAir",
            Variants = 10
        };
        
        private static int FlyingVariants => Mathf.Max(ConfigLoader.Config.FallEffect.EffectVariants, 0);
        private static int MaxEffectCount => Mathf.Clamp(ConfigLoader.Config.FallEffect.MaxConcurrentEffectCount, 0, FlyingVariants);
        private static float StrengthTarget => Mathf.Max(0f, ConfigLoader.Config.FallEffect.StrengthTarget);
        private static float SpeedTarget => Mathf.Max(0f, ConfigLoader.Config.FallEffect.SpeedTarget);
        private static float ConcurrentTarget => Mathf.Max(0f, ConfigLoader.Config.FallEffect.ConcurrentTarget);
        private static float StrengthMultiplier => Mathf.Max(0f, ConfigLoader.Config.FallEffect.StrengthMultiplier);

        public static void Execute(bool wasFlying)
        {
            if (!DynConfig.Toggles.Vest.FallingWind)
                return;
            
            var player = Mod.Instance.Data.Players.LocalPlayerContainer;
            var absVelocity = Mathf.Abs(player.RigidbodyVelocity.y);
            Execute(absVelocity, wasFlying);
        }
        
        public static void Execute(float absVelocity, bool wasFlying)
        {
            if (!DynConfig.Toggles.Vest.FallingWind)
                return;
            
            if (FlyingVariants == 0 || MaxEffectCount == 0 || StrengthTarget == 0 || SpeedTarget == 0 || ConcurrentTarget == 0)
                return;

            if (wasFlying)
            {
                BaselineVelocity = 5f;
                TargetMultiplier = 1f;
            }

            var velocity = (BaselineVelocity + absVelocity) * TargetMultiplier;
            // calc progress
            var fallStrengthProgress = Mathf.Min(velocity / StrengthTarget, 1f);
            var fallSpeedProgress = Mathf.Min(velocity / SpeedTarget, 1f);
            var fallCountProgress = Mathf.Min(velocity / ConcurrentTarget, 1f);
            
            // calculate effect parameters
            float fallStrength = Mathf.Clamp(Mathf.Lerp(0.1f, 1f, fallStrengthProgress) * StrengthMultiplier, 0f, 1f);
            float fallSpeed = Mathf.Lerp(1f, 0.3f, fallSpeedProgress);
            int effectCount = Mathf.FloorToInt(Mathf.Lerp(1, MaxEffectCount, fallCountProgress));

            EffectManager.DispatchEffect(effectCount, (name) =>
            {
                EffectPlayer.Play(
                    name, 
                    new Effect.EffectProperties
                    {
                        XRotation = ProceduralEffect.RandomVestRotation,
                        Strength = fallStrength,
                        Time = fallSpeed
                    }
                );
            });
        }

        // clear playing effects
        public static void Clear()
        {
            BaselineVelocity = 0f;
            TargetMultiplier = 1f;
            EffectManager.Clear();
        }
    }
}