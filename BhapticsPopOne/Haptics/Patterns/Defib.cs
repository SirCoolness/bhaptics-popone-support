using BhapticsPopOne.Haptics.EffectHelpers;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class Defib
    {
        public static void RubbingDefib()
        {
            EffectPlayer.Play("Arm/RubbingDefib");
            EffectPlayer.Play("Vest/RubbingDefib");
        }

        public static void ChargedDefib()
        {
            EffectPlayer.Play("Arm/ChargedDefib");
            EffectPlayer.Play("Vest/ChargedDefib");
        }
    }
}