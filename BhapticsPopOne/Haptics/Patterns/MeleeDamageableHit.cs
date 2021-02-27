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
            PatternManager.Effects[$"Arm/MeleeSlice{HapticUtils.HandExt(hand)}"]?.Play(new Effect.EffectProperties
            {
                Strength = 0.8f,
                Time = 0.8f
            });
        }
    }
}