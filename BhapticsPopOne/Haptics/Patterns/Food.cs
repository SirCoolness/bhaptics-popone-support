using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class Food
    {
        public static void EatBanana(BuffState state)
        { 
            if (state != BuffState.Consumed)
                return;
            
            if (ConfigLoader.Config.EffectToggles.Vest.ConsumeItem)
                EffectPlayer.Play("Vest/ConsumeItem");
            
            if (ConfigLoader.Config.EffectToggles.Vest.BananaHeal)
                EffectPlayer.Play("Vest/BananaHeal");
        }
    }
}