using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements.EffectSpecialization
{
    public class StateModifier
    {
        public float BaselineTime { get; set; }
        public float ProgressMultiplier { get; set; }
        
        [YamlIgnore] 
        public static StateModifier DefaultConfig = new StateModifier
        {
            BaselineTime = 0f,
            ProgressMultiplier = 1f
        };
    }
}