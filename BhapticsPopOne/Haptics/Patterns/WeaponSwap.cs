using System;
using MelonLoader;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class WeaponSwap
    {
        private static string lastVestEffect = "";
        private static string lastArmEffect = "";
        
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

        }

        public static void PlayDraw()
        {
            CancelLastPlayback();
            
            Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/SelectItem{EffectExtension()}");
            Mod.Instance.Haptics.Player.SubmitRegistered( $"Arm/SelectItem{EffectExtension()}");
        }

        public static void PlayHide()
        {
            lastVestEffect = $"Vest/HideWeapon{EffectExtension()}";
            lastArmEffect = $"Arm/HideWeapon{EffectExtension()}";
            
            Mod.Instance.Haptics.Player.SubmitRegistered(lastVestEffect);
            Mod.Instance.Haptics.Player.SubmitRegistered(lastArmEffect);
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