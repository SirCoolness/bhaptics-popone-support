using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Bhaptics.Tact;
using BhapticsPopOne.Haptics.EffectManagers;
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
            public Action OnComplete { get; internal set; } = () => { }; 
            
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

        public bool isPlaying => ActiveEffects.Count > 0;
        public bool isAllPlaying => EffectNames.Count <= ActiveEffects.Count;

        private readonly HashSet<System.Guid> EffectNames = new HashSet<System.Guid>();
        private readonly HashSet<System.Guid> ActiveEffects = new HashSet<System.Guid>();
        private readonly Dictionary<System.Guid, Action> OnEffectStop = new Dictionary<System.Guid, Action>();
        private static System.Guid FallbackID { get; set; } = new System.Guid();

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
            if (clear)
                Stop();

            Guid effectId;
            if (!GetAvailableEffect(out effectId))
                return;

            SubmitEffect(effectId, properties);
        }

        public void Stop()
        {
            foreach (var activeEffect in ActiveEffects)
                Mod.Instance.Haptics.Player.TurnOff(activeEffect.ToString());
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

                EffectEventsDispatcher.OnEffectStop[id.ToString()] = () => { OnEffectStopLabel(id); };
                OnEffectStop[id] = DefaultOnStop;
                
                Mod.Instance.Haptics.Player.Register(id.ToString(), Contents);
            }
        }

        private void SubmitEffect(System.Guid id, EffectProperties _properties)
        {
            EffectProperties properties = EffectProperties.Default;
            if (_properties != null)
                properties = _properties;

            OnEffectStop[id] = properties.OnComplete;
            
            Mod.Instance.Haptics.Player.SubmitRegisteredVestRotation(
                id.ToString(), 
                id.ToString(), 
                new RotationOption(properties.XRotation, properties.YOffset), 
                new ScaleOption(properties.Strength, properties.Time)
            );

            ActiveEffects.Add(id);
        }

        private bool GetAvailableEffect(out System.Guid res)
        {
            if (ActiveEffects.Count >= PoolSize)
            {
                res = FallbackID;
                return false;
            }
            
            foreach (var effectId in EffectNames)
                if (!ActiveEffects.Contains(effectId))
                {
                    res = effectId;
                    return true;
                }

            res = FallbackID;
            return false;
        }

        private void OnEffectStopLabel(System.Guid id)
        {
            ActiveEffects.Remove(id);
            OnEffectStop[id].Invoke();
            OnEffectStop[id] = DefaultOnStop;
        }
        
        private void DefaultOnStop()
        {}

        private static string ReadFile(string path)
        {
            return System.IO.File.ReadAllText(path);
        }
    }
}