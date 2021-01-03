using Bhaptics.Tact;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.Patterns;
using MelonLoader;

namespace BhapticsPopOne.Haptics
{
    public class EffectLoop
    {
        private bool ActiveBuffPlaying = false;
        
        private void PlayActiveBuff()
        {
            var active = DrinkSoda.estimatedSodaEffects;

            if (active == null)
                return;
            
            if (active.Count < 1)
            {
                Mod.Instance.Haptics.Player.TurnOff("Vest/ActiveBuff");
                return;
            }
            
            if (ActiveBuffPlaying)
                return;

            Mod.Instance.Haptics.Player.SubmitRegistered("Vest/ActiveBuff", new ScaleOption(ConfigHelpers.EnforceIntensity(ConfigLoader.Config.SodaBubbleIntensity), 1f));
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
            ActiveBuffPlaying = Mod.Instance.Haptics.Player.IsPlaying("Vest/ActiveBuff");
        }
        
        public void FixedUpdate()
        {
            UpdateStatus();
            PlayActiveBuff();
        }
    }
}