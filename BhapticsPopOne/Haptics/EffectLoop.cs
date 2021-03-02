﻿using Bhaptics.Tact;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using BhapticsPopOne.Haptics.Patterns;
using MelonLoader;

namespace BhapticsPopOne.Haptics
{
    public class EffectLoop
    {
        private bool WasPlaying = false;
        private void PlayActiveBuff()
        {
            if (WasPlaying && !DrinkSoda.Active)
            {
                EffectPlayer.Stop("Vest/ActiveBuff");
                WasPlaying = false;
                return;
            }

            if (ConfigLoader.Config.EffectToggles.Vest.ActiveDrink)
                EffectPlayer.Play("Vest/ActiveBuff", new Effect.EffectProperties
                {
                    Strength = ConfigHelpers.EnforceIntensity(ConfigLoader.Config.SodaBubbleIntensity)
                });
            WasPlaying = true;
        }

        public void FixedUpdate()
        {
            PlayActiveBuff();
        }
    }
}