using System;
using Bhaptics.Tact;
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
                EffectPlayer.Play("Vest/FallDamage_Level2");
                EffectPlayer.Play("Foot/FallDamage");
            }
            else
            {
                EffectPlayer.Play("Vest/FallDamage_Level1");
                EffectPlayer.Play("Foot/FallDamage", new Effect.EffectProperties
                {
                    Strength = 0.6f
                });
            }
        }
    }
}