using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements.EffectToggles
{
    public class Vest
    {
        public bool Recoil { get; set; }
        public bool ActiveDrink { get; set; }
        public bool Climbing { get; set; }
        public bool RubbingDefib { get; set; }
        public bool ChargedDefib { get; set; }
        public bool DestructibleHit { get; set; }
        public bool ConsumeItem { get; set; }
        public bool DropItem { get; set; }
        public bool FallDamage { get; set; }
        public bool FallingWind { get; set; }
        public bool FistBumpComplete { get; set; }
        public bool FlyingWind { get; set; }
        public bool BananaHeal { get; set; }
        public bool Victory { get; set; }
        public bool Heartbeat { get; set; }
        public bool ShieldBreak { get; set; }
        public bool FullShield { get; set; }
        public bool MeleeHit { get; set; }
        public bool PickupItem { get; set; }
        public bool BulletHit { get; set; }
        public bool ExplosionHit { get; set; }
        public bool Revived { get; set; }
        public bool EnterPod { get; set; }
        public bool LaunchPod { get; set; }
        public bool FlyingPod { get; set; }
        public bool FallPod { get; set; }
        public bool LandPod { get; set; }
        public bool Reload { get; set; }
        public bool SelectItem { get; set; }
        public bool HideItem { get; set; }
        public bool ZoneDamage { get; set; }
        public bool InsideZone { get; set; }
        public bool PlayerTouch { get; set; }
        public bool PlayerTouchVelocity { get; set; }

        [YamlIgnore] 
        public static Vest DefaultConfig => new Vest
        {
            Recoil = true,
            ActiveDrink = true,
            Climbing = false,
            RubbingDefib = true,
            ChargedDefib = true,
            DestructibleHit = true,
            ConsumeItem = true,
            DropItem = true,
            FallDamage = true,
            FallingWind = true,
            FistBumpComplete = true,
            FlyingWind = true,
            BananaHeal = true,
            Victory = true,
            Heartbeat = true,
            ShieldBreak = true,
            FullShield = true,
            MeleeHit = true,
            PickupItem = true,
            BulletHit = true,
            ExplosionHit = true,
            Revived = true,
            EnterPod = true,
            LaunchPod = true,
            FlyingPod = true,
            FallPod = true,
            LandPod = true,
            Reload = true,
            SelectItem = true,
            HideItem = true,
            ZoneDamage = true,
            InsideZone = true,
            PlayerTouch = true,
            PlayerTouchVelocity = true
        };
    }
}