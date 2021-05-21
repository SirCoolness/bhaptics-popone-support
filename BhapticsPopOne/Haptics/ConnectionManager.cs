using Bhaptics.Tact;
using BhapticsPopOne.ConfigManager;
using MelonLoader;

namespace BhapticsPopOne.Haptics
{
    // TODO: use HapticPlayerManager
    public class ConnectionManager
    {
        private bool UseFake = false;
        private FakeInstance _fakeInstance = new FakeInstance();
        private PlayerProxy _proxy = new PlayerProxy();

        public IHapticPlayer Player
        {
            get
            {
                if (UseFake)
                    return _fakeInstance;
                return _proxy;
            }
        }

        public void Start()
        {
            
        }

        public void Stop()
        {
            if (Mod.Instance.Disabled)
                return;
            
            MelonLogger.Warning("Disabling bHaptics support, please restart the game with bHaptics player running.");
            
            UseFake = true;
            Player.TurnOff();
            
            Mod.Instance.Disable();
        }
    }
}