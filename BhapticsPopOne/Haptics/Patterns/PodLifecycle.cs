using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class PodLifecycle
    {
        public static void EnteringPod()
        {
            if (ConfigLoader.Config.EffectToggles.Vest.EnterPod)
                EffectPlayer.Play("Vest/EnteringPod");
        }
        public static void LaunchingPod()
        {
            if (ConfigLoader.Config.EffectToggles.Vest.LaunchPod)
                EffectPlayer.Play("Vest/LaunchingPod");
            
            if (ConfigLoader.Config.EffectToggles.Feet.LaunchPod)
                EffectPlayer.Play("Foot/LaunchingPod");
        }

        public static void DuringPod()
        {
            if (ConfigLoader.Config.EffectToggles.Vest.FlyingPod)
                EffectPlayer.Play("Vest/DuringPod");
            
            if (ConfigLoader.Config.EffectToggles.Feet.FlyingPod)
                EffectPlayer.Play("Foot/DuringPod");
        }
    }
}