using System;
using System.Runtime.InteropServices;
using Bhaptics.Tact;
using BhapticsPopOne.ConfigManager;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class FirearmFire
    {
        private static LineDrawer line = new LineDrawer();
        private static LineDrawer line2 = new LineDrawer();

        private static Vector3 pos1 = Vector3.zero;
        private static Vector3 pos2 = Vector3.zero;

        private static float _effectStrength => Mathf.Clamp(ConfigLoader.Config.VestRecoil, 0f, 1f);
        private static bool _disableVest => _effectStrength <= 0f;
        
        public static void Execute(FirearmClass type, string name)
        {
            // DebugRay();
            
            string effectExtension = "";
            string otherHandeffectExtension = "";

            var handed = Mod.Instance.Data.Players.LocalPlayerContainer?.Data.DominantHand;
            var twoHanded = Mod.Instance.Data.Players.LocalPlayerContainer?.Data.TwoHand == true;

            float intensity = ConfigLoader.Config.OffhandRecoilStrength;
            float duration = 1f;
            
            if (handed == null)
                return;

            if (handed == Handedness.Left)
            {
                effectExtension = "_L";
                otherHandeffectExtension = "_R";

            } else if (handed == Handedness.Right)
            {
                effectExtension = "_R";
                otherHandeffectExtension = "_L";
            }


            if (name == "SniperAWP" || name == "Pistol357")
            {
                if (!_disableVest)
                    PlayPooledEffect($"Vest/RecoilLevel9001{effectExtension}", 4, new ScaleOption(_effectStrength, duration));
                Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel9001{effectExtension}");
                Mod.Instance.Haptics.Player.SubmitRegistered($"Hand/Recoil{effectExtension}", new ScaleOption(1f, 0.4f));

                if (twoHanded)
                {
                    if (!_disableVest)
                        PlayPooledEffect($"Vest/RecoilLevel9001{otherHandeffectExtension}", 4, new ScaleOption(_effectStrength * intensity, duration));
                    Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel9001{otherHandeffectExtension}",
                        new ScaleOption(intensity, duration));
                    Mod.Instance.Haptics.Player.SubmitRegistered($"Hand/Recoil{effectExtension}", new ScaleOption(1f * intensity, 0.4f));
                }
            }
            else if (type == FirearmClass.SMG)
            {
                if (!_disableVest)
                    PlayPooledEffect($"Vest/RecoilLevel1{effectExtension}", 4, new ScaleOption(_effectStrength, duration));
                Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel1{effectExtension}");
                Mod.Instance.Haptics.Player.SubmitRegistered($"Hand/Recoil{effectExtension}", new ScaleOption(0.9f, 0.125f));
                
                if (twoHanded)
                {
                    if (!_disableVest)
                        PlayPooledEffect($"Vest/RecoilLevel1{otherHandeffectExtension}", 4, new ScaleOption(_effectStrength * intensity, duration));
                    Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel1{otherHandeffectExtension}",
                        new ScaleOption(intensity, duration));
                    Mod.Instance.Haptics.Player.SubmitRegistered($"Hand/Recoil{effectExtension}", new ScaleOption(0.9f * intensity, 0.125f));
                }
            }
            else if (type == FirearmClass.Pistol || type == FirearmClass.AR)
            {
                if (!_disableVest)
                    PlayPooledEffect($"Vest/RecoilLevel2{effectExtension}", 4, new ScaleOption(_effectStrength, duration));
                Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel2{effectExtension}");
                Mod.Instance.Haptics.Player.SubmitRegistered($"Hand/Recoil{effectExtension}", new ScaleOption(1f, 0.2f));

                if (twoHanded)
                {
                    if (!_disableVest)
                        PlayPooledEffect($"Vest/RecoilLevel2{otherHandeffectExtension}", 4, new ScaleOption(_effectStrength * intensity, duration));
                    Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel2{otherHandeffectExtension}",
                        new ScaleOption(intensity, duration));
                    Mod.Instance.Haptics.Player.SubmitRegistered($"Hand/Recoil{effectExtension}", new ScaleOption(1f * intensity, 0.2f));
                }
            }
            else if (type == FirearmClass.Sniper || type == FirearmClass.Shotgun)
            {
                if (!_disableVest)
                    PlayPooledEffect($"Vest/RecoilLevel3{effectExtension}", 4, new ScaleOption(_effectStrength, duration));
                Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel3{effectExtension}");
                Mod.Instance.Haptics.Player.SubmitRegistered($"Hand/Recoil{effectExtension}", new ScaleOption(1f, 0.3f));

                if (twoHanded)
                {
                    if (!_disableVest)
                        PlayPooledEffect($"Vest/RecoilLevel3{otherHandeffectExtension}", 4, new ScaleOption(_effectStrength * intensity, duration));
                    Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel3{otherHandeffectExtension}",
                        new ScaleOption(intensity, duration));
                    Mod.Instance.Haptics.Player.SubmitRegistered($"Hand/Recoil{effectExtension}", new ScaleOption(1f * intensity, 0.3f));
                }
            }
        }
        
        public static void DebugRay()
        {
            // vest height is about 0.7 units
            line.Destroy();
            line = new LineDrawer(0.1f);
            
            line2.Destroy();
            line2 = new LineDrawer(0.1f);

            pos2 = pos1;
            pos1 = Mod.Instance.Data.Players.DebugHandReference().position;

            var vestRef = Mod.Instance.Data.Players.VestReference();

            var bottom = vestRef.position;
            bottom.x -= 1;

            var top = new Vector3(bottom.x + 2, bottom.y, bottom.z);
            
            line.DrawLineInGameView(bottom, top, Color.green);
            line2.DrawLineInGameView(pos1, pos2, Color.blue);
            
            MelonLogger.Log(Logging.StringifyVector3(new Vector3(Mathf.Abs(pos1.x - pos2.x), Mathf.Abs(pos1.y - pos2.y), Mathf.Abs(pos1.z - pos2.z))));
        }
        
        public static void PlayPooledEffect(string effect, int poolSize, [Optional] ScaleOption scale)
        {
            for (int i = 0; i < poolSize; i++)
            {
                if (!Mod.Instance.Haptics.Player.IsPlaying($"{effect}_Pool{i + 1}"))
                {
                    if (scale != null)
                        Mod.Instance.Haptics.Player.SubmitRegistered($"{effect}_Pool{i + 1}", scale);
                    else 
                        Mod.Instance.Haptics.Player.SubmitRegistered($"{effect}_Pool{i + 1}");
                    return;
                }
            }
        }
    }
}
