using System;
using MelonLoader;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class FallDamage
    {
        public static void Execute(int damage, float force)
        {
            if (damage >= 25)
                Mod.Instance.Haptics.Player.SubmitRegistered("Vest/FallDamage_Level2");
            else
                Mod.Instance.Haptics.Player.SubmitRegistered("Vest/FallDamage_Level1");
        }
    }
}