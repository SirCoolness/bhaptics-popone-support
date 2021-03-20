using BhapticsPopOne.ConfigManager.ConfigElements;
using BhapticsPopOne.ConfigManager.ConfigElements.EffectToggles;

namespace BhapticsPopOne.ConfigManager
{
    public class DynConfig
    {
        public enum SceneMode
        {
            General,
            Lobby
        }
        
        public static EffectToggles Toggles = EffectToggles.DefaultConfig;
        public static SceneMode Mode { get; private set; } = SceneMode.General;

        public static void UpdateConfig(SceneMode mode)
        {
            Mode = mode;
            
            if (mode == SceneMode.General)
                Toggles = ConfigLoader.Config.EffectToggles.General;
            else if (mode == SceneMode.Lobby)
                Toggles = ConfigLoader.Config.EffectToggles.Lobby;
        }
    }
}