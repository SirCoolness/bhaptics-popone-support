using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class PodLifecycle
    {
        public static void EnteringPod()
        {
            if (DynConfig.Toggles.Vest.EnterPod)
                EffectPlayer.Play("Vest/EnteringPod");
        }
        public static void LaunchingPod()
        {
            if (DynConfig.Toggles.Vest.LaunchPod)
                EffectPlayer.Play("Vest/LaunchingPod");
            
            if (DynConfig.Toggles.Feet.LaunchPod)
                EffectPlayer.Play("Foot/LaunchingPod");
        }

        public static void DuringPod()
        {
            if (DynConfig.Toggles.Vest.FlyingPod)
                EffectPlayer.Play("Vest/DuringPod");
            
            if (DynConfig.Toggles.Feet.FlyingPod)
                EffectPlayer.Play("Foot/DuringPod");
        }
    }
}