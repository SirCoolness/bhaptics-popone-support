using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements.EffectToggles
{
    public class Feet
    {
        [YamlIgnore] 
        public static Feet DefaultConfig = new Feet
        {
            
        };
    }
}