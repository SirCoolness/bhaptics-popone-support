using System;
using System.Collections.Generic;
using System.Linq;
using Bhaptics.Tact;
using MelonLoader;
using UnityEngine;
using Random = System.Random;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class FallingAir
    {
        public static List<int> ActiveEffects = new List<int>();
        public static int FlyingVariants = 10;
        public static int MaxEffectCount = 6;
        
        public static Random Randomizer = new Random();
        
        public static void Execute()
        {
            DequeueCompletedEffects();
            
            var player = Mod.Instance.Data.Players.LocalPlayerContainer;

            var fallStrengthProgress = Mathf.Abs(player.RigidbodyVelocity.y) / 50f;
            var fallSpeedProgress = Mathf.Abs(player.RigidbodyVelocity.y) / 15f;
            var fallCountProgress = Mathf.Abs(player.RigidbodyVelocity.y) / 30f;
            
            float fallStrength = Mathf.Lerp(0.1f, 1f, fallStrengthProgress);
            float fallSpeed = Mathf.Lerp(1f, 0.3f, fallSpeedProgress);

            int effectCount = Mathf.FloorToInt(Mathf.Lerp(1, MaxEffectCount, fallCountProgress));

            // MelonLogger.Log($"[{Logging.StringifyVector3(player.RigidbodyVelocity)}] [{fallStrengthProgress} {fallSpeedProgress} {fallCountProgress}] [{fallStrength} {fallSpeed} {effectCount}] {ActiveEffects.Count}");
            
            int remainingFill = Math.Max(effectCount - ActiveEffects.Count, 0);

            for (int i = 0; i < remainingFill; i++)
            {
                int nextId = GetRandomUnusedEffect();
                string effectName = $"Vest/FallingAir[{nextId}]";
                float vestRot = UnityEngine.Random.Range(0f, 360f) % 360f;
                
                Mod.Instance.Haptics.Player.SubmitRegisteredVestRotation(effectName, effectName, new RotationOption(vestRot, 0f), new ScaleOption(fallStrength, fallSpeed));
                
                ActiveEffects.Add(nextId);
            }
        }

        public static int GetRandomUnusedEffect()
        {
            var range = Enumerable.Range(1, FlyingVariants).Where(i => !ActiveEffects.Contains(i));

            int index = Randomizer.Next(0, FlyingVariants - ActiveEffects.Count);
            return range.ElementAt(index);
        }

        public static void Clear()
        {
            foreach (var activeEffect in ActiveEffects)
            {
                Mod.Instance.Haptics.Player.TurnOff($"Vest/FallingAir[{activeEffect}]");
            }

            DequeueCompletedEffects();
        }

        public static void DequeueCompletedEffects()
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