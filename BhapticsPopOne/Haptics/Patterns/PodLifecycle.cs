using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class PodLifecycle
    {
        public static void EnteringPod()
        {
            EffectPlayer.Play("Vest/EnteringPod");
            
        }
        public static void LaunchingPod()
        {
            EffectPlayer.Play("Vest/LaunchingPod");
            EffectPlayer.Play("Foot/LaunchingPod");
        }

        public static void DuringPod()
        {
            EffectPlayer.Play("Vest/DuringPod");
            EffectPlayer.Play("Foot/DuringPod");
        }
    }
}