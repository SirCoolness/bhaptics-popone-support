using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace BhapticsPopOne.Haptics.EffectHelpers
{
    public class EffectPlayer
    {
        // not sure if this is faster, but we shall see
        private static HashSet<string> UnresolvedCache = new HashSet<string>();
        
        public static void Resize(string name, uint size)
        {
            Effect effect;
            if (!TryResolve(name, out effect))
                return;

            effect.PoolSize = size;
        }

        public static bool IsPlaying(string name)
        {
            Effect effect;
            if (!TryResolve(name, out effect))
                return false;

            return effect.isPlaying;
        }
        
        public static bool IsAllPlaying(string name)
        {
            Effect effect;
            if (!TryResolve(name, out effect))
                return false;

            return effect.isAllPlaying;
        }

        public static void Play(string name, bool clear = false)
        {
            Play(name, Effect.EffectProperties.Default, clear);
        }
        
        public static void Play(string name, Effect.EffectProperties properties, bool clear = false)
        {
            Effect effect;
            if (!TryResolve(name, out effect))
                return;

            effect.Play(properties, clear);
        }
        
        public static void Stop(string name)
        {
            Effect effect;
            if (!TryResolve(name, out effect))
                return;

            effect.Stop();
        }
        
        private static bool TryResolve(string name, out Effect effect)
        {
            if (UnresolvedCache.Contains(name))
            {
                effect = null;
                return false;
            }
            
            if (!PatternManager.Effects.ContainsKey(name))
            {
                UnresolvedCache.Add(name);
                effect = null;
                return false;
            }

            effect = PatternManager.Effects[name];
            return true;
        }
    }
}