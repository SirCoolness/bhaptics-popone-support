using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements.EffectToggles
{
    public class Arms
    {
        [YamlIgnore] 
        public static Arms DefaultConfig = new Arms
        {

        };
    }
}