using System;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using MelonLoader;
using Il2Cpp;

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
            
            if (DynConfig.Toggles.Vest.DropItem)
                EffectPlayer.Play($"Vest/DropItem{effectExtension}");
        }
    }
}