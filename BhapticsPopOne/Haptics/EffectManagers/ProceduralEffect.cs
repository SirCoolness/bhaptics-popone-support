using System;
using System.Collections.Generic;
using System.Linq;
using BhapticsPopOne.Haptics.EffectHelpers;
using MelonLoader;

namespace BhapticsPopOne.Haptics.EffectManagers
{
    public class ProceduralEffect
    {
        private List<int> ActiveEffects = new List<int>();
        private Random Randomizer = new Random();

        public string EffectPrefix { get; set; }

        public int Variants { get; set; }

        public static float RandomVestRotation => UnityEngine.Random.Range(0f, 360f) % 360f;

        public void DispatchEffect(int maxCount, Action<string> callback)
        {
            DequeueCompletedEffects();
            
            int remainingFill = Math.Max(maxCount - ActiveEffects.Count, 0);

            // find remaining effects that need to be filled out
            for (int i = 0; i < remainingFill; i++)
            {
                int nextId = GetRandomUnusedEffect();
                if (nextId == -1)
                    break;
                
                string effectName = $"{EffectPrefix}[{nextId}]";
                callback(effectName);
                ActiveEffects.Add(nextId);
            }
        }

        // get unused effect
        public int GetRandomUnusedEffect()
        {
            var range = Enumerable.Range(1, Variants).Where(i => !ActiveEffects.Contains(i));

            if (Variants - ActiveEffects.Count <= 0)
                return -1;
            
            int index = Randomizer.Next(0, Variants - ActiveEffects.Count);
            return range.ElementAt(index);
        }

        // remove effect id from list of active effects
        public void DequeueCompletedEffects()
        {
            List<int> readyToRemove = new List<int>();
            foreach (var activeEffect in ActiveEffects)
            {
                if (!EffectPlayer.IsPlaying($"{EffectPrefix}[{activeEffect}]"))
                    readyToRemove.Add(activeEffect);
            }
            
            foreach (var s in readyToRemove)
            {
                ActiveEffects.Remove(s);
            }
        }
        
        // clear playing effects
        public void Clear()
        {
            foreach (var activeEffect in ActiveEffects)
            {
                EffectPlayer.Stop($"{EffectPrefix}[{activeEffect}]");
            }
            DequeueCompletedEffects();
        }
    }
}