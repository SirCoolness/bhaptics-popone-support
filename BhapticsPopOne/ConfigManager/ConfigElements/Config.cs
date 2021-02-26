using System;
using System.Reflection;
using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements
{
    public class Config
    {
        public string Version { get; set; }
        public float VestRecoil { get; set; }
        public float OffhandRecoilStrength { get; set; }
        public bool VestClimbEffects { get; set; }
        public bool Clapping { get; set; }
        public bool HighFive { get; set; }
        public bool ShowHighFiveRegion
        {
            get;
            set;
        }
        public bool ShowLoadedEffects { get; set; }
        public float FoodEatIntensity { get; set; }
        public float SodaBubbleIntensity { get; set; }
        public FallEffects FallEffect { get; set; }

        public EffectsConfig Effects { get; set; }
        
        public AllEffectToggles Toggles { get; set; }
        
        [YamlIgnore] 
        public static Version CurrentVersion => new Version("0.0.5");
        
        [YamlIgnore] 
        public static Config DefaultConfig = new Config
        {
            Version = CurrentVersion.ToString(),
            VestRecoil = 1f,
            OffhandRecoilStrength = 0.7f,
            VestClimbEffects = false,
            Clapping = false,
            HighFive = true,
            ShowHighFiveRegion = false,
            ShowLoadedEffects = false,
            FoodEatIntensity = 1.0f,
            SodaBubbleIntensity = 1.0f,
            FallEffect = FallEffects.DefaultConfig,
            Effects = EffectsConfig.DefaultConfig,
            Toggles = AllEffectToggles.DefaultConfig
        };
    }
}