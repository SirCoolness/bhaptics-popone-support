using System;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using MelonLoader;

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
            
            if (ConfigLoader.Config.EffectToggles.Vest.SelectItem)
                EffectPlayer.Play($"Vest/SelectItem{EffectExtension()}");
            EffectPlayer.Play( $"Arm/SelectItem{EffectExtension()}");
            EffectPlayer.Play( $"Hand/SelectItem{EffectExtension()}");
        }

        public static void PlayHide()
        {
            lastVestEffect = $"Vest/HideWeapon{EffectExtension()}";
            lastArmEffect = $"Arm/HideWeapon{EffectExtension()}";
            lastHandEffect = $"Hand/HideWeapon{EffectExtension()}";
         
            if (ConfigLoader.Config.EffectToggles.Vest.HideItem)
                EffectPlayer.Play(lastVestEffect);
            EffectPlayer.Play(lastArmEffect);
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