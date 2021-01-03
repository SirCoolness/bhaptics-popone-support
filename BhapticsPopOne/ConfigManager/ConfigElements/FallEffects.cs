using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements
{
    public class FallEffects
    {
        public int EffectVariants { get; set; }
        public int MaxConcurrentEffectCount { get; set; }
        public float StrengthTarget { get; set; }
        public float SpeedTarget { get; set; }
        public float ConcurrentTarget { get; set; }
        public float StrengthMultiplier { get; set; }
        
        [YamlIgnore] 
        public static FallEffects DefaultConfig = new FallEffects
        {
            EffectVariants = 10,
            MaxConcurrentEffectCount = 6,
            StrengthTarget = 50f,
            SpeedTarget = 15f,
            ConcurrentTarget = 30f,
            StrengthMultiplier = 1f
        };
    }
}