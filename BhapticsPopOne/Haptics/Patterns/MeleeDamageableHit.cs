using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using UnityEngine;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class MeleeDamageableHit
    {
        public static void Execute()
        {
            var container = Mod.Instance.Data.Players.LocalPlayerContainer;
            Play(container.Data.DominantHand);
            if (container.Data.TwoHand)
                Play(container.Data.DominantHand == Handedness.Left ? Handedness.Right : Handedness.Left);
        }

        private static void Play(Handedness hand)
        {
            if (DynConfig.Toggles.Arms.MeleeSlice)
                EffectPlayer.Play($"Arm/MeleeSlice{HapticUtils.HandExt(hand)}", new Effect.EffectProperties
                {
                    Strength = 0.8f,
                    Time = 0.8f
                });
            
            if (DynConfig.Toggles.Hands.MeleeSlice)
                EffectPlayer.Play($"Hand/MeleeSlice{HapticUtils.HandExt(hand)}", new Effect.EffectProperties
                {
                    Strength = 0.8f,
                    Time = 0.8f
                });
        }
    }
}