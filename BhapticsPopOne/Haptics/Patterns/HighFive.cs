using BhapticsPopOne.Haptics.Patterns;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class HighFive
    {
        public static void Execute(Handedness hand)
        {
            Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/HighFive{HapticUtils.HandExt(hand)}");
            Mod.Instance.Haptics.Player.SubmitRegistered($"Hand/HighFive{HapticUtils.HandExt(hand)}");
        }
    }
}