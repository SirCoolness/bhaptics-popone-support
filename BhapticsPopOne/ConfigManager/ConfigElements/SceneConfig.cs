using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements
{
    public class SceneConfig
    {
        public EffectToggles.EffectToggles General { get; set; }
        public EffectToggles.EffectToggles Lobby { get; set; }
        
        [YamlIgnore]
        public static SceneConfig DefaultConfig = new SceneConfig
        {
            General = EffectToggles.EffectToggles.DefaultConfig,
            Lobby = EffectToggles.EffectToggles.DefaultConfig
        };
    }
}