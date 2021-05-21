using System;
using System.Collections.Generic;
using BhapticsPopOne.Haptics.EffectManagers;
using MelonLoader;

using RotationOption = MelonLoader.bHaptics.RotationOption;
using ScaleOption = MelonLoader.bHaptics.ScaleOption;

namespace BhapticsPopOne.Haptics.EffectHelpers
{
    public class Effect
    {
        public string Name { get; private set; }
        public bool Initialized { get; private set; } = false;
        private string Contents { get; set; }

        public bool isPlaying => _activeEffects.Count > 0;
        public bool isAllPlaying => _effectNames.Count <= _activeEffects.Count;

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
        
        private readonly HashSet<System.Guid> _effectNames = new HashSet<System.Guid>();
        private readonly HashSet<System.Guid> _activeEffects = new HashSet<System.Guid>();
        private readonly Dictionary<System.Guid, Action> _onEffectStop = new Dictionary<System.Guid, Action>();
        private readonly System.Guid _fallbackID = new System.Guid();

        public Effect(string name, string content, bool register = true)
        {
            Name = name;
            Contents = content;
            
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

        public void Play(bool clear = false)
        {
            Play(EffectProperties.Default, clear);
        }
        
        public void Play(EffectProperties properties, bool clear = false)
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
            foreach (var activeEffect in _activeEffects)
            {
                Mod.Instance.Haptics.Player.TurnOff(activeEffect.ToString());
                _onEffectStop[activeEffect].Invoke();
                _onEffectStop[activeEffect] = DefaultOnStop;
            }
            
            _activeEffects.Clear();
        }
        
        private void ResizePool(bool force = false)
        {
            if (!Initialized && !force)
                return;

            if (PoolSize <= _effectNames.Count)
                return;

            uint toAdd = PoolSize - (uint)_effectNames.Count;
            
            for (uint i = 0; i < toAdd; i++)
            {
                var id = Guid.NewGuid();
                _effectNames.Add(id);

                Mod.Instance.Haptics.Player.RegisterTactFileStr(id.ToString(), Contents);
            }
            
            foreach (var id in _effectNames)
            {
                EffectEventsDispatcher.OnEffectStop[id.ToString()] = () => { OnEffectStopLabel(id); };
                _onEffectStop[id] = DefaultOnStop;
            }
        }

        private void SubmitEffect(System.Guid id, EffectProperties properties)
        {
            _onEffectStop[id] = properties.OnComplete;
            
            Mod.Instance.Haptics.Player.SubmitRegistered(
                id.ToString(), 
                id.ToString(), 
                new ScaleOption(properties.Strength, properties.Time),
                new RotationOption(properties.XRotation, properties.YOffset)
            );

            _activeEffects.Add(id);
            EffectEventsDispatcher.RegisterPlay(id);
        }

        private bool GetAvailableEffect(out System.Guid res)
        {
            if (_activeEffects.Count >= PoolSize)
            {
                res = _fallbackID;
                return false;
            }
            
            foreach (var effectId in _effectNames)
                if (!_activeEffects.Contains(effectId))
                {
                    res = effectId;
                    return true;
                }

            res = _fallbackID;
            return false;
        }

        private void OnEffectStopLabel(System.Guid id)
        {
            _activeEffects.Remove(id);
            _onEffectStop[id].Invoke();
            _onEffectStop[id] = DefaultOnStop;
        }
        
        private void DefaultOnStop()
        {}
        
        public class EffectProperties
        {
            public float XRotation { get; internal set; } = 0f;
            public float YOffset { get; internal set; } = 0f;
            public float Time { get; internal set; } = 1f;
            public float Strength { get; internal set; } = 1f;
            public Action OnComplete { get; internal set; } = () => { }; 
            
            public static EffectProperties Default => new EffectProperties();
        }
    }
}