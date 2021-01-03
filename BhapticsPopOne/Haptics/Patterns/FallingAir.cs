using System;
using System.Collections.Generic;
using System.Linq;
using Bhaptics.Tact;
using BhapticsPopOne.ConfigManager;
using MelonLoader;
using UnityEngine;
using Random = System.Random;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class FallingAir
    {
        private static List<int> ActiveEffects = new List<int>();
        private static Random Randomizer = new Random();
        
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
            DequeueCompletedEffects();
            
            if (FlyingVariants == 0 || MaxEffectCount == 0 || StrengthTarget == 0 || SpeedTarget == 0 || ConcurrentTarget == 0)
                return;
            
            // calc progress
            var fallStrengthProgress = absVelocity / StrengthTarget;
            var fallSpeedProgress = absVelocity / SpeedTarget;
            var fallCountProgress = absVelocity / ConcurrentTarget;
            
            // calculate effect parameters
            float fallStrength = Mathf.Clamp(Mathf.Lerp(0.1f, 1f, fallStrengthProgress) * StrengthMultiplier, 0f, 1f);
            float fallSpeed = Mathf.Lerp(1f, 0.3f, fallSpeedProgress);
            int effectCount = Mathf.FloorToInt(Mathf.Lerp(1, MaxEffectCount, fallCountProgress));

            // MelonLogger.Log($"[{Logging.StringifyVector3(player.RigidbodyVelocity)}] [{fallStrengthProgress} {fallSpeedProgress} {fallCountProgress}] [{fallStrength} {fallSpeed} {effectCount}] {ActiveEffects.Count}");
            
            // find remaining effects that need to be filled out
            int remainingFill = Math.Max(effectCount - ActiveEffects.Count, 0);

            for (int i = 0; i < remainingFill; i++)
            {
                int nextId = GetRandomUnusedEffect();
                if (nextId == -1)
                    break;
                
                string effectName = $"Vest/FallingAir[{nextId}]";
                float vestRot = UnityEngine.Random.Range(0f, 360f) % 360f;
                
                Mod.Instance.Haptics.Player.SubmitRegisteredVestRotation(
                    effectName, 
                    effectName, 
                    new RotationOption(vestRot, 0f), 
                    new ScaleOption(fallStrength, fallSpeed)
                    );
                
                ActiveEffects.Add(nextId);
            }
        }

        // clear playing effects
        public static void Clear()
        {
            foreach (var activeEffect in ActiveEffects)
            {
                Mod.Instance.Haptics.Player.TurnOff($"Vest/FallingAir[{activeEffect}]");
            }

            DequeueCompletedEffects();
        }
        
        // get unused effect
        private static int GetRandomUnusedEffect()
        {
            var range = Enumerable.Range(1, FlyingVariants).Where(i => !ActiveEffects.Contains(i));

            if (FlyingVariants - ActiveEffects.Count <= 0)
                return -1;
            
            int index = Randomizer.Next(0, FlyingVariants - ActiveEffects.Count);
            return range.ElementAt(index);
        }

        // remove effect id from list of active effects
        private static void DequeueCompletedEffects()
        {
            List<int> readyToRemove = new List<int>();
            foreach (var activeEffect in ActiveEffects)
            {
                if (!Mod.Instance.Haptics.Player.IsPlaying($"Vest/FallingAir[{activeEffect}]"))
                {
                    readyToRemove.Add(activeEffect);
                }
            }
            
            foreach (var s in readyToRemove)
            {
                ActiveEffects.Remove(s);
            }
        }
    }
}