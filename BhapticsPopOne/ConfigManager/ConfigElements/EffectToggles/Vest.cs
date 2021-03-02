using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements.EffectToggles
{
    public class Vest
    {
        [YamlIgnore] 
        public static Vest DefaultConfig = new Vest
        {

        };
    }
}