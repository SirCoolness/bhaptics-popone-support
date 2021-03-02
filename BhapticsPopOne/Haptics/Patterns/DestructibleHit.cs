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
            
            EffectPlayer.Play($"Vest/DestructibleHit{effectExtension}");
            EffectPlayer.Play($"Arm/DestructibleHit{effectExtension}");
            EffectPlayer.Play($"Hand/DestructibleHit{effectExtension}");
        }
    }
}