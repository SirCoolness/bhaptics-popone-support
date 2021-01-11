using System;
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
            Mod.Instance.Haptics.Player.TurnOff(lastVestEffect);
            Mod.Instance.Haptics.Player.TurnOff(lastArmEffect);
            Mod.Instance.Haptics.Player.TurnOff(lastHandEffect);
        }

        public static void PlayDraw()
        {
            CancelLastPlayback();
            
            Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/SelectItem{EffectExtension()}");
            Mod.Instance.Haptics.Player.SubmitRegistered( $"Arm/SelectItem{EffectExtension()}");
            Mod.Instance.Haptics.Player.SubmitRegistered( $"Hand/SelectItem{EffectExtension()}");
        }

        public static void PlayHide()
        {
            lastVestEffect = $"Vest/HideWeapon{EffectExtension()}";
            lastArmEffect = $"Arm/HideWeapon{EffectExtension()}";
            lastHandEffect = $"Hand/HideWeapon{EffectExtension()}";
            
            Mod.Instance.Haptics.Player.SubmitRegistered(lastVestEffect);
            Mod.Instance.Haptics.Player.SubmitRegistered(lastArmEffect);
            Mod.Instance.Haptics.Player.SubmitRegistered(lastHandEffect);
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