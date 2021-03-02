using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class Climbing
    {
        public static void Execute(Handedness value)
        {
            if (ConfigLoader.Config.EffectToggles.Arms.Climbing)
                EffectPlayer.Play($"Arm/Climbing{HapticUtils.HandExt(value)}");
            
            EffectPlayer.Play($"Hand/Climbing{HapticUtils.HandExt(value)}");
            
            if (ConfigLoader.Config.EffectToggles.Vest.Climbing)
                EffectPlayer.Play($"Vest/Climbing{HapticUtils.HandExt(value)}");
        }
    }
}