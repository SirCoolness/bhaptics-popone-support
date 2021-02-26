using System;
using System.Collections.Generic;
using BhapticsPopOne.Haptics.EffectHelpers;
using UnityEngine;

namespace BhapticsPopOne.Haptics.EffectManagers
{
    public class EffectLoopRegistry
    {
        private static Dictionary<string, EffectTimingData> effects = new Dictionary<string, EffectTimingData>();
        
        public static void Register(string name, ByteEffect effect)
        {
            effects.Add(name, new EffectTimingData
            {
                Effect = effect,
                Playing = false,
                CurrentStep = 0,
                NextInvoke = 0
            });
        }

        public static void Start(string name)
        {
            var effect = effects[name];
            effect.Playing = true;
            effect.CurrentStep = 0;
            effect.NextInvoke = GetCurrentTime();
            effect.Timeout = 0;
        }
        
        public static void Start(string name, uint duration)
        {
            Start(name);
            
            var effect = effects[name];
            effect.Timeout = GetCurrentTime() + duration;
        }
        
        public static void Stop(string name)
        {
            var effect = effects[name];
            effect.Playing = false;
            effect.CurrentStep = 0;
        }

        public static void Update()
        {
            uint time = GetCurrentTime();

            foreach (var effectTimingData in effects)
            {
                Play(effectTimingData.Key, effectTimingData.Value, time);
            }
        }

        public static void LevelInit()
        {
            foreach (var effectTimingData in effects)
            {
                Stop(effectTimingData.Key);
            }
        }

        private static void Play(string key, EffectTimingData effect, uint time)
        {
            if (!effect.Playing || effect.NextInvoke > time)
                return;

            if (effect.Timeout != 0 && time > effect.Timeout)
            {
                Stop(key);
                return;
            }
            
            Mod.Instance.Haptics.Player.Submit(key, effect.Effect.Position, effect.Current.State, (int)effect.Current.Length);
            effect.NextInvoke = time + (effect.Current.Length + effect.Current.Delay);
            effect.Next();
        }

        private static uint GetCurrentTime()
        {
            return (uint) (Time.time * 1000f);
        }

        private class EffectTimingData
        {
            public ByteEffect Effect;
            public uint NextInvoke;
            
            public int CurrentStep = 0;
            public ByteEffect.Step Current => Effect.Steps[CurrentStep];

            public bool Playing = false;
            public uint Timeout = 0;

            public void Next()
            {
                CurrentStep = (CurrentStep + 1) % Effect.Steps.Length;
            }
        }
    }
}