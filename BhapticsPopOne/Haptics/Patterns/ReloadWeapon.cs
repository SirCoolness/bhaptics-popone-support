using System;
using System.Collections.Generic;
using MelonLoader;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class ReloadWeapon
    {
        public static Dictionary<int, FirearmState> PreviousStateMap = new Dictionary<int, FirearmState>();
        
        public static void Execute(FirearmState state, int instanceId)
        {
            if (!PreviousStateMap.ContainsKey(instanceId))
            {
                PreviousStateMap.Add(instanceId, state);
                // return early because the only reason this would be executed is that if a weapon is spawned with ready
                return;
            }
            
            var previous = PreviousStateMap[instanceId];
            PreviousStateMap[instanceId] = state;
                
            if (previous == FirearmState.Fire || previous == state)
                return;

            var dominant = Mod.Instance.Data.Players.LocalPlayerContainer.Data.DominantHand;
            
            var ext = HapticUtils.HandExt(dominant == Handedness.Right ? Handedness.Left : Handedness.Right);

            // if (state == FirearmState.Prime1)
            // {
            //     Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/ReloadStep1{ext}");
            //     Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/ReloadStep1{ext}");
            // } else if (state == FirearmState.Ready || state == FirearmState.Prime2)
            // {
            //     Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/ReloadStep2{ext}");
            //     Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/ReloadStep2{ext}");
            // }
        }
    }
}