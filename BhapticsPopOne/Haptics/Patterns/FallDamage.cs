using System;
using Bhaptics.Tact;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using MelonLoader;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class FallDamage
    {
        public static void Execute(int damage, float force)
        {
            if (damage >= 25)
            {
                if (ConfigLoader.Config.EffectToggles.Vest.FallDamage)
                    EffectPlayer.Play("Vest/FallDamage_Level2");
                EffectPlayer.Play("Foot/FallDamage");
            }
            else
            {
                if (ConfigLoader.Config.EffectToggles.Vest.FallDamage)
                    EffectPlayer.Play("Vest/FallDamage_Level1");
                EffectPlayer.Play("Foot/FallDamage", new Effect.EffectProperties
                {
                    Strength = 0.6f
                });
            }
        }
    }
}