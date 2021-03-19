﻿using System;
using System.Collections.Generic;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using MelonLoader;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class ReloadWeapon
    {
        public static void Execute(FirearmState state, int currentState, int finalState)
        {
            var ext = HapticUtils.HandExt(
                Mod.Instance.Data.Players.LocalPlayerContainer.Data.DominantHand == Handedness.Right 
                    ? Handedness.Left : Handedness.Right);

            if (state == FirearmState.Prime && currentState == finalState)
            {
                if (ConfigLoader.Config.EffectToggles.Vest.Reload)
                    EffectPlayer.Play($"Vest/ReloadStep2{ext}");
                
                if (ConfigLoader.Config.EffectToggles.Arms.Reload)
                    EffectPlayer.Play($"Arm/ReloadStep2{ext}");
            } else if (state == FirearmState.Prime || state == FirearmState.InsertAmmo)
            {
                if (ConfigLoader.Config.EffectToggles.Vest.Reload)
                    EffectPlayer.Play($"Vest/ReloadStep1{ext}");
                
                if (ConfigLoader.Config.EffectToggles.Arms.Reload)
                    EffectPlayer.Play($"Arm/ReloadStep1{ext}");
                
            }
        }
    }
}