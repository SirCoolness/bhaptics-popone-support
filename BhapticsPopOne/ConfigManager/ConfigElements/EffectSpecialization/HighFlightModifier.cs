using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements.EffectSpecialization
{
    public class HighFlightModifier : StateModifier
    {
        public float MinDistance { get; set; }
        
        [YamlIgnore] 
        public new static HighFlightModifier DefaultConfig = new HighFlightModifier
        {
            BaselineTime = 0.3f,
            ProgressMultiplier = 0.5f,
            MinDistance = 20f
        };
    }
}