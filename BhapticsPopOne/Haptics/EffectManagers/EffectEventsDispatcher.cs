using System;
using System.Collections.Generic;
using Bhaptics.Tact;
using System.Collections.Specialized;
using MelonLoader;

namespace BhapticsPopOne.Haptics.EffectManagers
{
    public class EffectEventsDispatcher
    {
        public static Dictionary<string, Action> OnEffectStop = new Dictionary<string, Action>();
        
        private static HashSet<string> previousActiveKeys = new HashSet<string>();

        public static void Init()
        {
            Mod.Instance.Haptics.Player.StatusReceived += StatusReceived;
        }
        
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

        private static HashSet<string> FindRemoved(List<string> src, HashSet<string> previous)
        {
            HashSet<string> res = previous;

            foreach (var s in src)
            {
                res.Remove(s);
            }
            
            return res;
        }
    }
}