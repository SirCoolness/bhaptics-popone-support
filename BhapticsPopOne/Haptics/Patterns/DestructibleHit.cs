using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class DestructibleHit
    {
        public static void Execute(Handedness hand)
        {
            string effectExtension = "";
            
            if (hand == Handedness.Left)
            {
                effectExtension = "_L";

            } else if (hand == Handedness.Right)
            {
                effectExtension = "_R";
            }
            
            if (ConfigLoader.Config.EffectToggles.Vest.DestructibleHit)
                EffectPlayer.Play($"Vest/DestructibleHit{effectExtension}");
            
            if (ConfigLoader.Config.EffectToggles.Arms.DestructibleHit)
                EffectPlayer.Play($"Arm/DestructibleHit{effectExtension}");
            
            if (ConfigLoader.Config.EffectToggles.Hands.DestructibleHit)
                EffectPlayer.Play($"Hand/DestructibleHit{effectExtension}");
        }
    }
}