using Bhaptics.Tact;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;

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


            if (ConfigLoader.Config.EffectToggles.Arms.FistBumpFriend)
            {
                EffectPlayer.Play($"Arm/FistBumpHit{HapticUtils.HandExt(currentFistbump)}");
                EffectPlayer.Play($"Arm/FistBumpStarted{HapticUtils.HandExt(currentFistbump)}",
                    new Effect.EffectProperties
                    {
                        Time = time
                    });
            }

            if (ConfigLoader.Config.EffectToggles.Hands.FistBumpFriend)
            {
                EffectPlayer.Play($"Hand/FistBumpHit{HapticUtils.HandExt(currentFistbump)}");
                EffectPlayer.Play($"Hand/FistBumpStarted{HapticUtils.HandExt(currentFistbump)}",
                    new Effect.EffectProperties
                    {
                        Time = time
                    });
            }
        }

        public static void Stop(bool finished)
        {
            if (finished)
            {
                if (ConfigLoader.Config.EffectToggles.Vest.FistBumpComplete)
                    EffectPlayer.Play($"Vest/FistBump{HapticUtils.HandExt(currentFistbump)}");
                
                if (ConfigLoader.Config.EffectToggles.Arms.FistBumpComplete)
                    EffectPlayer.Play($"Arm/FistBumpComplete{HapticUtils.HandExt(currentFistbump)}");
                
                if (ConfigLoader.Config.EffectToggles.Hands.FistBumpComplete)
                    EffectPlayer.Play($"Hand/FistBumpComplete{HapticUtils.HandExt(currentFistbump)}");
            }
            
            EffectPlayer.Stop($"Arm/FistBumpStarted{HapticUtils.HandExt(currentFistbump)}");
            EffectPlayer.Stop($"Hand/FistBumpStarted{HapticUtils.HandExt(currentFistbump)}");

            currentFistbump = Handedness.Unknown;
        }
    }
}