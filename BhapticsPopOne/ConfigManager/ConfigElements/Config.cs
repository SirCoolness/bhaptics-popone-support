using System;
using System.Reflection;
using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements
{
    public class Config
    {
        public string Version { get; set; }
        public EffectToggles.EffectToggles EffectToggles { get; set; }
        public AllEffectToggles Toggles { get; set; }
        public float VestRecoil { get; set; }
        public float OffhandRecoilStrength { get; set; }
        public float FoodEatIntensity { get; set; }
        public float SodaBubbleIntensity { get; set; }
        public FallEffects FallEffect { get; set; }
        public EffectsConfig Effects { get; set; }

        [YamlIgnore] 
        public static Version CurrentVersion => new Version("0.0.6");
        
        [YamlIgnore] 
        public static Config DefaultConfig = new Config
        {
            Version = CurrentVersion.ToString(),
            EffectToggles = ConfigElements.EffectToggles.EffectToggles.DefaultConfig,
            VestRecoil = 1f,
            OffhandRecoilStrength = 0.7f,
            FoodEatIntensity = 1.0f,
            SodaBubbleIntensity = 1.0f,
            FallEffect = FallEffects.DefaultConfig,
            Effects = EffectsConfig.DefaultConfig,
            Toggles = AllEffectToggles.DefaultConfig
        };
    }
}