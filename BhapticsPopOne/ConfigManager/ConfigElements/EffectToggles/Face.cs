using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements.EffectToggles
{
    public class Face
    {
        public bool BulletHit { get; set; }
        public bool ExplosionHit { get; set; }
        public bool PlayerTouch { get; set; }
        public bool PlayerTouchVelocity { get; set; }

        [YamlIgnore] 
        public static Face DefaultConfig => new Face
        {
            BulletHit = true,
            ExplosionHit = true,
            PlayerTouch = true,
            PlayerTouchVelocity = true
        };
    }
}