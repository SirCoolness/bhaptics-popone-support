using BhapticsPopOne.Haptics.EffectHelpers;
using MelonLoader;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class KatanaShield
    {
        private static bool Active = false;
        private static bool WasPlaying = false;
        
        public static void Execute(bool active)
        {
            Active = active;
            
            if (WasPlaying)
            {
                PatternManager.Effects["Arm/MeleeShield"]?.Stop();
                WasPlaying = false;
            }
        }

        public static void Block()
        {
            PatternManager.Effects["Arm/MeleeBlock"]?.Play(new Effect.EffectProperties
            {
                Strength = 1f,
                Time = 0.08f
            });
        }

        public static void FixedUpdate()
        {
            if (!Active)
                return;
            
            if (WasPlaying && PatternManager.Effects["Arm/MeleeShield"]?.isPlaying == true)
                return;
                
            PatternManager.Effects["Arm/MeleeShield"]?.Play(new Effect.EffectProperties
            {
                Strength = 0.15f
            });
            WasPlaying = true;
        }
    }
}