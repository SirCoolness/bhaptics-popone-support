using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements
{
    public class EffectsConfig
    {
        public FlyEffects Flying { get; set; }
        
        [YamlIgnore] 
        public static EffectsConfig DefaultConfig = new EffectsConfig
        {
            Flying = FlyEffects.DefaultConfig
        };
    }
}