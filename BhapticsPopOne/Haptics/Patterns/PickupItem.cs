using System;
using MelonLoader;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class PickupItem
    {
        public static void Execute(Handedness handed)
        {
            string effectExtension = "";
            if (handed == Handedness.Left)
            {
                effectExtension = "_L";

            } else if (handed == Handedness.Right)
            {
                effectExtension = "_R";
            }
            
            Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/ItemPickup{effectExtension}");
            Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/ItemPickup{effectExtension}");
        }
    }
}