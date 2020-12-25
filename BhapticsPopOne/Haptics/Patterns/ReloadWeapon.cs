using MelonLoader;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class ReloadWeapon
    {
        private static FirearmState _previous;
        
        public static void Execute(FirearmState state)
        {
            if (_previous == FirearmState.Fire)
            {
                _previous = state;
                return;
            }

            _previous = state;
            
            var dominant = Mod.Instance.Data.Players.LocalPlayerContainer.Data.DominantHand;
            
            var ext = FistBump.HandExt(dominant == Handedness.Right ? Handedness.Left : Handedness.Right);

            if (state == FirearmState.Prime1)
            {
                Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/ReloadStep1{ext}");
                Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/ReloadStep1{ext}");
            } else if (state == FirearmState.Ready || state == FirearmState.Prime2)
            {
                Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/ReloadStep2{ext}");
                Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/ReloadStep2{ext}");
            }
        }
    }
}