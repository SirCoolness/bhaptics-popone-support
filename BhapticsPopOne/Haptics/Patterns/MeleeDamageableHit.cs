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

        public static void Play(Handedness hand, float strength = 1f, float time = 0.6f)
        {
            if (DynConfig.Toggles.Arms.MeleeSlice)
                EffectPlayer.Play($"Arm/MeleeSlice{HapticUtils.HandExt(hand)}", new Effect.EffectProperties
                {
                    Strength = strength,
                    Time = time
                });
            
            if (DynConfig.Toggles.Hands.MeleeSlice)
                EffectPlayer.Play($"Hand/MeleeSlice{HapticUtils.HandExt(hand)}", new Effect.EffectProperties
                {
                    Strength = strength,
                    Time = time
                });
        }
    }
}