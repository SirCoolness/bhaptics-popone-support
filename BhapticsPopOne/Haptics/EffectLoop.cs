using Bhaptics.Tact;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using BhapticsPopOne.Haptics.Patterns;
using MelonLoader;

namespace BhapticsPopOne.Haptics
{
    public class EffectLoop
    {
        private bool ActiveBuffPlaying = false;
        
        private void PlayActiveBuff()
        {
            if (!DrinkSoda.Active)
            {
                PatternManager.Effects["Vest/ActiveBuff"]?.Stop();
                return;
            }
            
            if (ActiveBuffPlaying)
                return;

            PatternManager.Effects["Vest/ActiveBuff"]?.Play(new Effect.EffectProperties
            {
                Strength = ConfigHelpers.EnforceIntensity(ConfigLoader.Config.SodaBubbleIntensity)
            });
        }

        private int statusSlowdown = 0;
        private void UpdateStatus()
        {
            statusSlowdown++;
            
            if (statusSlowdown < 10)
            {
                return;
            }
            
            statusSlowdown = 0;
            ActiveBuffPlaying = PatternManager.Effects["Vest/ActiveBuff"]?.isPlaying ?? false;
        }
        
        public void FixedUpdate()
        {
            UpdateStatus();
            PlayActiveBuff();
        }
    }
}