using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class Defib
    {
        public static void RubbingDefib()
        {
            if (DynConfig.Toggles.Arms.RubbingDefib)
                EffectPlayer.Play("Arm/RubbingDefib");
            
            if (DynConfig.Toggles.Vest.RubbingDefib)
                EffectPlayer.Play("Vest/RubbingDefib");
        }

        public static void ChargedDefib()
        {
            if (DynConfig.Toggles.Arms.ChargedDefib)
                EffectPlayer.Play("Arm/ChargedDefib");
            
            if (DynConfig.Toggles.Vest.ChargedDefib)
                EffectPlayer.Play("Vest/ChargedDefib");
        }
    }
}