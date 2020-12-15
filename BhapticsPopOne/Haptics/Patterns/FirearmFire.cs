namespace BhapticsPopOne.Haptics.Patterns
{
    public class FirearmFire
    {
        public static void Execute(FirearmClass type, string name)
        {
            string effectExtension = "";

            var handed = Mod.Instance.Data.Players.LocalPlayerContainer?.Data.DominantHand;
            if (handed == null)
                return;

            if (handed == Handedness.Left)
                effectExtension = "_L";
            else if (handed == Handedness.Right)
                effectExtension = "_R";

            if (name  == "SniperAWP" || name == "Pistol357")
                Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/RecoilLevel9001{effectExtension}");
            else if (type == FirearmClass.SMG)
                Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/RecoilLevel1{effectExtension}");
            else if (type == FirearmClass.Pistol || type == FirearmClass.AR)
                Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/RecoilLevel2{effectExtension}");
            else if (type == FirearmClass.Sniper || type == FirearmClass.Shotgun)
                Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/RecoilLevel3{effectExtension}");
        }
    }
}
