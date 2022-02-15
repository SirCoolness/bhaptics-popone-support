using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using MelonLoader;

namespace BhapticsPopOne.Haptics.EffectManagers
{
    public class EffectEventsDispatcher
    {
        private static Dictionary<string, Tuple<Func<bool>, Action>> CallbackMap = new Dictionary<string, Tuple<Func<bool>, Action>>();
        
        private static HashSet<string> activeEffects = new HashSet<string>();
        
        // avoid creating a new instance every update
        private static List<string> inactiveKeys = new List<string>();

        public static void RegisterEffect(System.Guid id, Func<bool> checkFn, Action onStop)
        {
            CallbackMap[id.ToString()] = new Tuple<Func<bool>, Action>(checkFn, onStop);
        }

        public static void RegisterPlay(Guid id)
        {
            if (!CallbackMap.ContainsKey(id.ToString()))
                return;
            
            activeEffects.Add(id.ToString());
        }

        private static void FinalizeInactiveKeys(List<string> inactiveKeys)
        {
            foreach (var inactiveKey in inactiveKeys)
            {
                activeEffects.Remove(inactiveKey);

                CallbackMap[inactiveKey].Item2.Invoke();
            }
        }

        public static void FixedUpdate()
        {
            if (activeEffects.Count <= 0)
                return;

            foreach (var activeEffect in activeEffects)
            {
                if (CallbackMap[activeEffect].Item1())
                    continue;
                
                inactiveKeys.Add(activeEffect);
            }

            if (inactiveKeys.Count <= 0)
                return;
            
            FinalizeInactiveKeys(inactiveKeys);
            
            inactiveKeys.Clear();
        }
    }
}