using BhapticsPopOne.Haptics.Patterns;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class HighFive
    {
        public static void Execute(Handedness hand)
        {
            Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/HighFive{FistBump.HandExt(hand)}");
        }
    }
}