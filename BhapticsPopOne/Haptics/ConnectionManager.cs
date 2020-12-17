using Bhaptics.Tact;
using MelonLoader;

namespace BhapticsPopOne.Haptics
{
    // TODO: use HapticPlayerManager
    public class ConnectionManager
    {
        // public HapticPlayerManager HapticPlayerManager = null;

        private IHapticPlayer _Player;

        public IHapticPlayer Player
        {
            get
            {
                // return HapticPlayerManager.GetHapticPlayer();
                return _Player;
            }
        }

        public void Start()
        {
            // HapticPlayerManager = HapticPlayerManager.Instance();
#pragma warning disable 618
            _Player = new HapticPlayer("Population: One", "Population: One", state =>
#pragma warning restore 618
            {
                MelonLogger.Log($"BHAPTICS PLAYER: CONNECTION STATE {state}");
            });
        }

        public void Stop()
        {
            // HapticPlayerManager.Dispose();
            _Player.Disable();
            _Player.Dispose();
        }
    }
}