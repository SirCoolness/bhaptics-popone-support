﻿using System;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using MelonLoader;
using Il2Cpp;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class WeaponSwap
    {
        private static string lastVestEffect = "";
        private static string lastArmEffect = "";
        private static string lastHandEffect = "";
        
        public static void Execute(int current, int next)
        {
            if (next == 0)
                PlayHide();
            else
                PlayDraw();
        }

        public static void CancelLastPlayback()
        {
            EffectPlayer.Stop(lastVestEffect);
            EffectPlayer.Stop(lastArmEffect);
            EffectPlayer.Stop(lastHandEffect);
        }

        public static void PlayDraw()
        {
            CancelLastPlayback();
            
            if (DynConfig.Toggles.Vest.SelectItem)
                EffectPlayer.Play($"Vest/SelectItem{EffectExtension()}");
            
            if (DynConfig.Toggles.Arms.SelectItem)
                EffectPlayer.Play( $"Arm/SelectItem{EffectExtension()}");
            
            if (DynConfig.Toggles.Hands.SelectItem)
                EffectPlayer.Play( $"Hand/SelectItem{EffectExtension()}");
        }

        public static void PlayHide()
        {
            lastVestEffect = $"Vest/HideWeapon{EffectExtension()}";
            lastArmEffect = $"Arm/HideWeapon{EffectExtension()}";
            lastHandEffect = $"Hand/HideWeapon{EffectExtension()}";
         
            if (DynConfig.Toggles.Vest.HideItem)
                EffectPlayer.Play(lastVestEffect);
            
            if (DynConfig.Toggles.Arms.HideItem)
                EffectPlayer.Play(lastArmEffect);
            
            if (DynConfig.Toggles.Hands.HideItem)
                EffectPlayer.Play(lastHandEffect);
        }

        private static string EffectExtension()
        {
            var handed = Mod.Instance.Data.Players.LocalPlayerContainer?.Data.DominantHand;

            if (handed == null)
                return "";

            if (handed == Handedness.Left)
            {
                return "_L";

            } 
            
            return "_R";
        }
    }
}