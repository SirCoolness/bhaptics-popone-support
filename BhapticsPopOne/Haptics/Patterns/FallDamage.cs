using System;
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
                if (DynConfig.Toggles.Vest.FallDamage)
                    EffectPlayer.Play("Vest/FallDamage_Level2");
                
                if (DynConfig.Toggles.Feet.FallDamage)
                    EffectPlayer.Play("Foot/FallDamage");
            }
            else
            {
                if (DynConfig.Toggles.Vest.FallDamage)
                    EffectPlayer.Play("Vest/FallDamage_Level1");
                
                if (DynConfig.Toggles.Feet.FallDamage)
                    EffectPlayer.Play("Foot/FallDamage", new Effect.EffectProperties
                    {
                        Strength = 0.6f
                    });
            }
        }
    }
}