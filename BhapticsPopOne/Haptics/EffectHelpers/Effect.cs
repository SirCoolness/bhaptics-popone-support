using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Bhaptics.Tact;
using Il2CppSystem.Linq;
using MelonLoader;
using PlayFab.Json;

namespace BhapticsPopOne.Haptics.EffectHelpers
{
    public class Effect
    {
        public class EffectProperties
        {
            public float XRotation { get; internal set; } = 0f;
            public float YOffset { get; internal set; } = 0f;
            public float Time { get; internal set; } = 1f;
            public float Strength { get; internal set; } = 1f;
            
            public static EffectProperties Default => new EffectProperties();
        }

        public string Name { get; private set; }
        public Project Contents { get; private set; }
        public bool Initialized { get; private set; } = false;
        private uint _poolSize = 1;
        public uint PoolSize
        {
            get { return _poolSize; }
            set
            {
                _poolSize = value;
                ResizePool();
            }
        }
        
        private readonly HashSet<System.Guid> EffectNames = new HashSet<System.Guid>();
        private readonly HashSet<System.Guid> ActiveEffects = new HashSet<System.Guid>();

        public Effect(string name, string path, bool register = true)
        {
            Name = name;
            Contents = Project.ToProject(JSONObject.Parse(ReadFile(path)).AsObject["project"].AsObject);

            if (register)
                Register();
        }

        public void Register()
        {
            if (Initialized)
                return;
            
            ResizePool(true);
            
            Initialized = true;
        }

        public void Play([Optional] EffectProperties properties, bool clear = false)
        {
            DequeueCompletedEffects();
            
            if (clear)
                Stop();

            var effectId = GetAvailableEffect();
            if (!effectId.HasValue)
                return;
            
            SubmitEffect(effectId.Value, properties);
        }

        public void Stop()
        {
            foreach (var activeEffect in ActiveEffects)
                Mod.Instance.Haptics.Player.TurnOff(activeEffect.ToString());
            
            ActiveEffects.Clear();
        }
        
        private void ResizePool(bool force = false)
        {
            if (!Initialized && !force)
                return;

            if (PoolSize <= EffectNames.Count)
                return;

            uint toAdd = PoolSize - (uint)EffectNames.Count;
            
            for (uint i = 0; i < toAdd; i++)
            {
                var id = System.Guid.NewGuid();
                EffectNames.Add(id);
                
                Mod.Instance.Haptics.Player.Register(id.ToString(), Contents);
            }
        }

        private void SubmitEffect(System.Guid id, EffectProperties _properties)
        {
            EffectProperties properties = EffectProperties.Default;
            if (_properties != null)
                properties = _properties;
            
            Mod.Instance.Haptics.Player.SubmitRegisteredVestRotation(
                id.ToString(), 
                id.ToString(), 
                new RotationOption(properties.XRotation, properties.YOffset), 
                new ScaleOption(properties.Strength, properties.Time)
            );

            ActiveEffects.Add(id);
        }

        private void DequeueCompletedEffects()
        {
            var readyToRemove = new HashSet<System.Guid>();
            foreach (var activeEffect in ActiveEffects)
                if (!Mod.Instance.Haptics.Player.IsPlaying(activeEffect.ToString()))
                    readyToRemove.Add(activeEffect);
            
            foreach (var s in readyToRemove)
                ActiveEffects.Remove(s);
        }

        private System.Guid? GetAvailableEffect()
        {
            if (ActiveEffects.Count >= PoolSize)
                return null;
            
            foreach (var effectName in EffectNames)
                if (!ActiveEffects.Contains(effectName))
                    return effectName;

            return null;
        }
        
        private static string ReadFile(string path)
        {
            return System.IO.File.ReadAllText(path);
        }
    }
}