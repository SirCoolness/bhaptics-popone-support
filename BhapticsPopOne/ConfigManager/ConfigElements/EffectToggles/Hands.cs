using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements.EffectToggles
{
    public class Hands
    {
        public bool Climbing { get; set; }
        public bool PlayerTouch { get; set; }
        public bool PlayerTouchVelocity { get; set; }
        public bool DestructibleHit { get; set; }
        public bool Recoil { get; set; }
        public bool FistBumpFriend { get; set; }
        public bool FistBumpComplete { get; set; }
        public bool FlyingWind { get; set; }
        public bool MeleeShield { get; set; }
        public bool MeleeBlock { get; set; }
        public bool MeleeSlice { get; set; }
        public bool MeleeVelocity { get; set; }
        public bool PickupItem { get; set; }
        public bool SelectItem { get; set; }
        public bool HideItem { get; set; }

        [YamlIgnore] 
        public static Hands DefaultConfig = new Hands
        {
            Climbing = true,
            PlayerTouch = true,
            PlayerTouchVelocity = true,
            DestructibleHit = true,
            Recoil = true,
            FistBumpFriend = true,
            FistBumpComplete = true,
            FlyingWind = true,
            MeleeShield = true,
            MeleeBlock = true,
            MeleeSlice = true,
            MeleeVelocity = true,
            PickupItem = true,
            SelectItem = true,
            HideItem = true
        };
    }
}