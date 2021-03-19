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
            
            if (DynConfig.Toggles.Vest.DestructibleHit)
                EffectPlayer.Play($"Vest/DestructibleHit{effectExtension}");
            
            if (DynConfig.Toggles.Arms.DestructibleHit)
                EffectPlayer.Play($"Arm/DestructibleHit{effectExtension}");
            
            if (DynConfig.Toggles.Hands.DestructibleHit)
                EffectPlayer.Play($"Hand/DestructibleHit{effectExtension}");
        }
    }
}