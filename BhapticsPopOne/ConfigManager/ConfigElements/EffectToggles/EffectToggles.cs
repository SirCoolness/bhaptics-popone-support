using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements.EffectToggles
{
    public class EffectToggles
    {
        public Face Face { get; set; }
        public Vest Vest { get; set; }
        public Arms Arms { get; set; }
        public Hands Hands { get; set; }
        public Feet Feet { get; set; }
        
        [YamlIgnore] 
        public static EffectToggles DefaultConfig = new EffectToggles
        {
            Face = Face.DefaultConfig,
            Vest = Vest.DefaultConfig,
            Arms = Arms.DefaultConfig,
            Hands = Hands.DefaultConfig,
            Feet = Feet.DefaultConfig
        };
    }
}