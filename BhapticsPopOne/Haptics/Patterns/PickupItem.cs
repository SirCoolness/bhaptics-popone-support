using System;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using MelonLoader;
using Il2Cpp;

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
            
            if (DynConfig.Toggles.Vest.PickupItem)
                EffectPlayer.Play($"Vest/ItemPickup{effectExtension}");
            
            if (DynConfig.Toggles.Arms.PickupItem)
                EffectPlayer.Play($"Arm/ItemPickup{effectExtension}");
            
            if (DynConfig.Toggles.Hands.PickupItem)
                EffectPlayer.Play($"Hand/ItemPickup{effectExtension}");
        }
    }
}