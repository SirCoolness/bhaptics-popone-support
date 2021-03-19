using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements.EffectToggles
{
    public class Arms
    {
        public bool Recoil { get; set; }
        public bool PickupItem { get; set; }
        public bool PlayerTouch { get; set; }
        public bool PlayerTouchVelocity { get; set; }
        public bool Climbing { get; set; }
        public bool RubbingDefib { get; set; }
        public bool ChargedDefib { get; set; }
        public bool DestructibleHit { get; set; }
        public bool FistBumpFriend { get; set; }
        public bool FistBumpComplete { get; set; }
        public bool FlyingWind { get; set; }
        public bool MeleeBlock { get; set; }
        public bool MeleeShield { get; set; }
        public bool MeleeSlice { get; set; }
        public bool MeleeVelocity { get; set; }
        public bool Reload { get; set; }
        public bool SelectItem { get; set; }
        public bool HideItem { get; set; }

        [YamlIgnore] 
        public static Arms DefaultConfig => new Arms
        {
            Recoil = true,
            PickupItem = true,
            PlayerTouch = true,
            PlayerTouchVelocity = true,
            Climbing = true,
            RubbingDefib = true,
            ChargedDefib = true,
            DestructibleHit = true,
            FistBumpFriend = true,
            FistBumpComplete = true,
            FlyingWind = true,
            MeleeBlock = true,
            MeleeShield= true,
            MeleeSlice = true,
            MeleeVelocity = true,
            Reload = true,
            SelectItem = true,
            HideItem = true
        };
    }
}