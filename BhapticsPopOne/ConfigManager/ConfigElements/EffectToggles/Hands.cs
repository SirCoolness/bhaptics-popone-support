using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements.EffectToggles
{
    public class Hands
    {
        [YamlIgnore] 
        public static Hands DefaultConfig = new Hands
        {

        };
    }
}