using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements
{
    public class AllEffectToggles
    {
        public bool LowPerformanceMode { get; set; }
        public bool PlayerTouching { get; set; }
        public bool ShowLoadedEffects { get; set; }
        public bool ZoneBackMassageMode { get; set; }

        [YamlIgnore] 
        public static AllEffectToggles DefaultConfig = new AllEffectToggles
        {
            LowPerformanceMode = false,
            PlayerTouching = true,
            ShowLoadedEffects = false,
            ZoneBackMassageMode = false
        };
    }
}