using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements.EffectToggles
{
    public class Face
    {
        [YamlIgnore] 
        public static Face DefaultConfig = new Face
        {

        };
    }
}