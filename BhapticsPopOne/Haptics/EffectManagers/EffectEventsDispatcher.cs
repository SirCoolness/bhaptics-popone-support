using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using MelonLoader;

namespace BhapticsPopOne.Haptics.EffectManagers
{
    public class EffectEventsDispatcher
    {
        public static Dictionary<string, Action> OnEffectStop = new Dictionary<string, Action>();
        
        private static HashSet<string> previousActiveKeys = new HashSet<string>();
        private static HashSet<string> activeEffects = new HashSet<string>();

        public static void RegisterPlay(Guid id)
        {
            activeEffects.Add(id.ToString());
        }

        public static void Init()
        {
#if PORT_DISABLE
            Mod.Instance.Haptics.Player.StatusReceived += StatusReceived;
#endif
        }

#if PORT_DISABLE
        public static void StatusReceived(PlayerResponse playerStatus)
        {
            var removed = FindRemoved(playerStatus.ActiveKeys, previousActiveKeys);
            previousActiveKeys = new HashSet<string>(playerStatus.ActiveKeys);
            
            foreach (var s in removed)
            {
                if (!OnEffectStop.ContainsKey(s))
                    continue;

                OnEffectStop[s].Invoke();
            }
        }
#endif

        private static void OnUpdate(List<string> activeKeys)
        {
            var removed = FindRemoved(activeKeys, previousActiveKeys);
            previousActiveKeys = new HashSet<string>(activeKeys);

            foreach (var s in removed)
            {
                activeKeys.Remove(s);
                
                if (!OnEffectStop.ContainsKey(s))
                    continue;

                OnEffectStop[s].Invoke();
            }
        }
        
        private static HashSet<string> FindRemoved(List<string> src, HashSet<string> previous)
        {
            HashSet<string> res = previous;

            foreach (var s in src)
            {
                res.Remove(s);
            }
            
            return res;
        }

        public static void FixedUpdate()
        {
            var activeKeys = new List<string>();
            
            foreach (var activeEffect in activeEffects)
            {
                if (!Mod.Instance.Haptics.Player.IsPlaying(activeEffect))
                    continue;
                
                activeKeys.Add(activeEffect);
            }
            
            OnUpdate(activeKeys);
        }
    }
}