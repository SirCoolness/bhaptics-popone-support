using BhapticsPopOne.Haptics.EffectHelpers;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class Food
    {
        public static void EatBanana(BuffState state)
        { 
            if (state != BuffState.Consumed)
                return;
            
            EffectPlayer.Play("Vest/ConsumeItem");
            EffectPlayer.Play("Vest/BananaHeal");
        }
    }
}