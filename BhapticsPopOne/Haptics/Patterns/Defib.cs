using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class Defib
    {
        public static void RubbingDefib()
        {
            EffectPlayer.Play("Arm/RubbingDefib");
            if (ConfigLoader.Config.EffectToggles.Vest.RubbingDefib)
                EffectPlayer.Play("Vest/RubbingDefib");
        }

        public static void ChargedDefib()
        {
            EffectPlayer.Play("Arm/ChargedDefib");
            if (ConfigLoader.Config.EffectToggles.Vest.ChargedDefib)
                EffectPlayer.Play("Vest/ChargedDefib");
        }
    }
}