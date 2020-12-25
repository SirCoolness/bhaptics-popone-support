using System;
using MelonLoader;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class DropItem
    {
        public static void Execute()
        {
            var handed = Mod.Instance.Data.Players.LocalPlayerContainer?.Data.DominantHand;
            
            string effectExtension = "";
            if (handed == Handedness.Left)
            {
                effectExtension = "_L";

            } else if (handed == Handedness.Right)
            {
                effectExtension = "_R";
            }
            
            Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/DropItem{effectExtension}");
        }
    }
}