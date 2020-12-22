using System;
using Bhaptics.Tact;
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
        
        public static void Execute(FirearmClass type, string name)
        {
            // DebugRay();
            
            string effectExtension = "";
            string otherHandeffectExtension = "";

            var handed = Mod.Instance.Data.Players.LocalPlayerContainer?.Data.DominantHand;
            var twoHanded = Mod.Instance.Data.Players.LocalPlayerContainer?.Data.TwoHand == true;

            float intensity = 0.7f;
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
                Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/RecoilLevel9001{effectExtension}");
                Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel9001{effectExtension}");

                if (twoHanded)
                {
                    Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/RecoilLevel9001{otherHandeffectExtension}", 
                        new ScaleOption(intensity, duration));
                    Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel9001{otherHandeffectExtension}",
                        new ScaleOption(intensity, duration));
                }
            }
            else if (type == FirearmClass.SMG)
            {
                Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/RecoilLevel1{effectExtension}");
                Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel1{effectExtension}");

                if (twoHanded)
                {
                    Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/RecoilLevel1{otherHandeffectExtension}",
                        new ScaleOption(intensity, duration));
                    Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel1{otherHandeffectExtension}",
                        new ScaleOption(intensity, duration));
                }
            }
            else if (type == FirearmClass.Pistol || type == FirearmClass.AR)
            {
                Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/RecoilLevel2{effectExtension}");
                Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel2{effectExtension}");

                if (twoHanded)
                {
                    Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/RecoilLevel2{otherHandeffectExtension}",
                        new ScaleOption(intensity, duration));
                    Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel2{otherHandeffectExtension}",
                        new ScaleOption(intensity, duration));
                }
            }
            else if (type == FirearmClass.Sniper || type == FirearmClass.Shotgun)
            {
                Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/RecoilLevel3{effectExtension}");
                Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel3{effectExtension}");

                if (twoHanded)
                {
                    Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/RecoilLevel3{otherHandeffectExtension}",
                        new ScaleOption(intensity, duration));
                    Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel3{otherHandeffectExtension}",
                        new ScaleOption(intensity, duration));
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
    }
}
