using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements
{
    public class Config
    {
        public float VestRecoil { get; set; }
        public bool VestClimbEffects { get; set; }

        [YamlIgnore] 
        public static Config DefaultConfig = new Config
        {
            VestRecoil = 1f,
            VestClimbEffects = true
        };
    }
}