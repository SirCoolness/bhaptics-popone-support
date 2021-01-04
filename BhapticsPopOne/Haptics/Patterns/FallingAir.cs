using System;
using System.Collections.Generic;
using System.Linq;
using Bhaptics.Tact;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectManagers;
using MelonLoader;
using UnityEngine;
using Random = System.Random;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class FallingAir
    {
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

        public static void Execute()
        {
            var player = Mod.Instance.Data.Players.LocalPlayerContainer;
            var absVelocity = Mathf.Abs(player.RigidbodyVelocity.y);
            Execute(absVelocity);
        }
        
        public static void Execute(float absVelocity)
        {
            if (FlyingVariants == 0 || MaxEffectCount == 0 || StrengthTarget == 0 || SpeedTarget == 0 || ConcurrentTarget == 0)
                return;
            
            // calc progress
            var fallStrengthProgress = Mathf.Min(absVelocity / StrengthTarget, 1f);
            var fallSpeedProgress = Mathf.Min(absVelocity / SpeedTarget, 1f);
            var fallCountProgress = Mathf.Min(absVelocity / ConcurrentTarget, 1f);
            
            // calculate effect parameters
            float fallStrength = Mathf.Clamp(Mathf.Lerp(0.1f, 1f, fallStrengthProgress) * StrengthMultiplier, 0f, 1f);
            float fallSpeed = Mathf.Lerp(1f, 0.3f, fallSpeedProgress);
            int effectCount = Mathf.FloorToInt(Mathf.Lerp(1, MaxEffectCount, fallCountProgress));

            // MelonLogger.Log($"[{Logging.StringifyVector3(player.RigidbodyVelocity)}] [{fallStrengthProgress} {fallSpeedProgress} {fallCountProgress}] [{fallStrength} {fallSpeed} {effectCount}] {ActiveEffects.Count}");

            EffectManager.DispatchEffect(effectCount, (name) =>
            {
                Mod.Instance.Haptics.Player.SubmitRegisteredVestRotation(
                    name, 
                    name, 
                    new RotationOption(ProceduralEffect.RandomVestRotation, 0f), 
                    new ScaleOption(fallStrength, fallSpeed)
                );
            });
        }

        // clear playing effects
        public static void Clear()
        {
            EffectManager.Clear();
        }
    }
}