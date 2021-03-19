using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements.EffectToggles
{
    public class Feet
    {
        public bool LandOnGround { get; set; }
        public bool FallDamage { get; set; }
        public bool FlyingWind { get; set; }
        public bool LaunchPod { get; set; }
        public bool FlyingPod { get; set; }
        public bool PlayerTouch { get; set; }
        public bool PlayerTouchVelocity { get; set; }

        [YamlIgnore] 
        public static Feet DefaultConfig => new Feet
        {
            LandOnGround = true,
            FallDamage = true,
            FlyingWind = true,
            LaunchPod = true,
            FlyingPod = true,
            PlayerTouch = true,
            PlayerTouchVelocity = true
        };
    }
}