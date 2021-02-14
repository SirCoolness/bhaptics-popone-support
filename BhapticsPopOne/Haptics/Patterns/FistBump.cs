using Bhaptics.Tact;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class FistBump
    {
        public static Handedness lastPunchhandR;
        public static Handedness lastPunchhandL;
        public static Handedness currentFistbump;
        
        public static void Start(float time)
        {
            if (currentFistbump != Handedness.Unknown)
                return;
            
            Handedness dominant = Mod.Instance.Data.Players.LocalPlayerContainer.Data.DominantHand;

            Handedness preferredHand;
            if (lastPunchhandR == dominant)
                preferredHand = lastPunchhandR;
            else if (lastPunchhandL == dominant)
                preferredHand = lastPunchhandL;
            else if (lastPunchhandR != Handedness.Unknown)
                preferredHand = lastPunchhandR;
            else
                preferredHand = lastPunchhandL;


            currentFistbump = preferredHand;
            
            Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/FistBumpHit{HapticUtils.HandExt(currentFistbump)}");
            Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/FistBumpStarted{HapticUtils.HandExt(currentFistbump)}", new ScaleOption(1f, time));
            
            Mod.Instance.Haptics.Player.SubmitRegistered($"Hand/FistBumpHit{HapticUtils.HandExt(currentFistbump)}");
            Mod.Instance.Haptics.Player.SubmitRegistered($"Hand/FistBumpStarted{HapticUtils.HandExt(currentFistbump)}", new ScaleOption(1f, time));
        }

        public static void Stop(bool finished)
        {
            if (finished)
            {
                Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/FistBump{HapticUtils.HandExt(currentFistbump)}");
                Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/FistBumpComplete{HapticUtils.HandExt(currentFistbump)}");
                Mod.Instance.Haptics.Player.SubmitRegistered($"Hand/FistBumpComplete{HapticUtils.HandExt(currentFistbump)}");
            }
            
            Mod.Instance.Haptics.Player.TurnOff($"Arm/FistBumpStarted{HapticUtils.HandExt(currentFistbump)}");
            Mod.Instance.Haptics.Player.TurnOff($"Hand/FistBumpStarted{HapticUtils.HandExt(currentFistbump)}");

            currentFistbump = Handedness.Unknown;
        }
    }
}