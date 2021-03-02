using Bhaptics.Tact;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using BhapticsPopOne.Haptics.Patterns;
using MelonLoader;

namespace BhapticsPopOne.Haptics
{
    public class EffectLoop
    {
        private void PlayActiveBuff()
        {
            if (!DrinkSoda.Active)
            {
                EffectPlayer.Stop("Vest/ActiveBuff");
                return;
            }

            EffectPlayer.Play("Vest/ActiveBuff", new Effect.EffectProperties
            {
                Strength = ConfigHelpers.EnforceIntensity(ConfigLoader.Config.SodaBubbleIntensity)
            });
        }

        public void FixedUpdate()
        {
            PlayActiveBuff();
        }
    }
}