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
            
            Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/FistBumpHit{HandExt(currentFistbump)}");
            Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/FistBumpStarted{HandExt(currentFistbump)}", new ScaleOption(1f, time));
        }

        public static void Stop(bool finished)
        {
            if (finished)
            {
                Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/FistBump{HandExt(currentFistbump)}");
                Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/FistBumpComplete{HandExt(currentFistbump)}");
            }
            
            Mod.Instance.Haptics.Player.TurnOff($"Arm/FistBumpStarted{HandExt(currentFistbump)}");

            currentFistbump = Handedness.Unknown;
        }

        public static string HandExt(Handedness hand)
        {
            if (hand == Handedness.Left)
            {
                return "_L";

            } else if (hand == Handedness.Right)
            {
                return "_R";
            }

            return "";
        }
    }
}