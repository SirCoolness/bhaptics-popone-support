using System;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using MelonLoader;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class PickupItem
    {
        public static void Execute(Handedness handed)
        {
            string effectExtension = "";
            if (handed == Handedness.Left)
            {
                effectExtension = "_L";

            } else if (handed == Handedness.Right)
            {
                effectExtension = "_R";
            }
            
            if (ConfigLoader.Config.EffectToggles.Vest.PickupItem)
                EffectPlayer.Play($"Vest/ItemPickup{effectExtension}");
            
            if (ConfigLoader.Config.EffectToggles.Arms.PickupItem)
                EffectPlayer.Play($"Arm/ItemPickup{effectExtension}");
            
            if (ConfigLoader.Config.EffectToggles.Hands.PickupItem)
                EffectPlayer.Play($"Hand/ItemPickup{effectExtension}");
        }
    }
}