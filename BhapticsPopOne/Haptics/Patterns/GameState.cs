using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class GameState
    {
        public static void Victory()
        {
            if (ConfigLoader.Config.EffectToggles.Vest.Victory)
                EffectPlayer.Play("Vest/Win");
        }
        
        public static void Land()
        {
            EffectPlayer.Play("Foot/LandOnGround", new Effect.EffectProperties
            {
                Strength = 0.4f,
                Time = 0.12f
            });
        }
    }
}