using UnityEngine;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class FirearmFire
    {
        private static LineDrawer line = new LineDrawer();
        
        public static void Execute(FirearmClass type, string name)
        {
            // DebugRay();
            
            string effectExtension = "";

            var handed = Mod.Instance.Data.Players.LocalPlayerContainer?.Data.DominantHand;
            if (handed == null)
                return;

            if (handed == Handedness.Left)
                effectExtension = "_L";
            else if (handed == Handedness.Right)
                effectExtension = "_R";

            if (name == "SniperAWP" || name == "Pistol357")
            {
                Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/RecoilLevel9001{effectExtension}");
                Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel9001{effectExtension}");
            }
            else if (type == FirearmClass.SMG)
            {
                Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/RecoilLevel1{effectExtension}");
                Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel1{effectExtension}");
            }
            else if (type == FirearmClass.Pistol || type == FirearmClass.AR)
            {
                Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/RecoilLevel2{effectExtension}");
                Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel2{effectExtension}");
            }
            else if (type == FirearmClass.Sniper || type == FirearmClass.Shotgun)
            {
                Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/RecoilLevel3{effectExtension}");
                Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/RecoilLevel3{effectExtension}");
            }
        }
        
        public static void DebugRay()
        {
            line.Destroy();
            line = new LineDrawer(0.1f);

            var vestRef = Mod.Instance.Data.Players.VestReference();
            var collider = vestRef.gameObject.GetComponent<CapsuleCollider>();

            var bottom = collider.center;
            bottom.y -= collider.height / 2;

            var top = bottom;
            top.y += collider.height;
            
            line.DrawLineInGameView(bottom, top, Color.green);
        }
    }
}
