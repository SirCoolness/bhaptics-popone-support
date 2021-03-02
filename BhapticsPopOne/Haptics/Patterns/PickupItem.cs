﻿using System;
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
            
            EffectPlayer.Play($"Vest/ItemPickup{effectExtension}");
            EffectPlayer.Play($"Arm/ItemPickup{effectExtension}");
            EffectPlayer.Play($"Hand/ItemPickup{effectExtension}");
        }
    }
}