using System;
using Bhaptics.Tact;
using MelonLoader;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class FallDamage
    {
        public static void Execute(int damage, float force)
        {
            if (damage >= 25)
            {
                Mod.Instance.Haptics.Player.SubmitRegistered("Vest/FallDamage_Level2");
                Mod.Instance.Haptics.Player.SubmitRegistered("Foot/FallDamage", new ScaleOption(1f, 1f));
            }
            else
            {
                Mod.Instance.Haptics.Player.SubmitRegistered("Vest/FallDamage_Level1");
                Mod.Instance.Haptics.Player.SubmitRegistered("Foot/FallDamage", new ScaleOption(0.6f, 1f));
            }
        }
    }
}