using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements
{
    public class ProceduralEffectTarget : ProceduralEffectBase
    {
        public LerpGoal Goal { get; set; }
    }
}