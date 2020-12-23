using Bhaptics.Tact;
using MelonLoader;

namespace BhapticsPopOne.Haptics
{
    // TODO: use HapticPlayerManager
    public class ConnectionManager
    {
        public HapticPlayerManager HapticPlayerManager = null;

        public PlayerResponse Status = null;

        public IHapticPlayer Player
        {
            get
            {
                return HapticPlayerManager.GetHapticPlayer();
            }
        }

        public void Start()
        {
            HapticPlayerManager = HapticPlayerManager.Instance();
            Player.StatusReceived += PlayerStatus;
        }

        public void Stop()
        {
            HapticPlayerManager.Dispose();
        }

        public void PlayerStatus(PlayerResponse res)
        {
            Status = res;
        }
    }
}