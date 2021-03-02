using BhapticsPopOne.ConfigManager.ConfigElements.EffectSpecialization;
using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements
{
    public class FlyEffects
    {
        public EffectStateModifiers Modifiers { get; set; }
        public FrontEffects Front { get; set; }
        public BackEffects Back { get; set; }
        public ArmEffects Arms { get; set; }
        public HandEffects Hands { get; set; }
        public FeetEffects Feet { get; set; }

        [YamlIgnore] 
        public static FlyEffects DefaultConfig = new FlyEffects
        {
            Front = FrontEffects.DefaultConfig,
            Back = BackEffects.DefaultConfig,
            Arms = ArmEffects.DefaultConfig,
            Hands = HandEffects.DefaultConfig,
            Feet = FeetEffects.DefaultConfig,
            Modifiers = EffectStateModifiers.DefaultConfig
        };

        public class EffectStateModifiers
        {
            public StateModifier FromFalling { get; set; }
            public HighFlightModifier HighFlight { get; set; }
            
            [YamlIgnore] 
            public static EffectStateModifiers DefaultConfig = new EffectStateModifiers
            {
                FromFalling = new StateModifier
                {
                    BaselineTime = 0.2f,
                    ProgressMultiplier = 0.7f
                },
                HighFlight = HighFlightModifier.DefaultConfig
            };
        }
        
        public class FrontEffects
        {
            public ProceduralStrength Strength { get; set; }
            public ProceduralEffectTarget Speed { get; set; }
            public ProceduralPool EffectPool { get; set; }
            
            [YamlIgnore] 
            public static FrontEffects DefaultConfig = new FrontEffects
            {
                Strength = new ProceduralStrength
                {
                    Multiplier = 0.4f,
                    Target = 0.75f,
                    Goal = new LerpGoal
                    {
                        Start = 0.3f,
                        End = 1f
                    }
                },
                Speed = new ProceduralEffectTarget
                {
                    Target = 0.75f,
                    Goal = new LerpGoal
                    {
                        Start = 1f,
                        End = 0.5f
                    }
                },
                EffectPool = new ProceduralPool
                {
                    Target = 2f,
                    Variants = 8,
                    MaxConcurrency = 4
                }
            };
        }
        
        public class BackEffects
        {
            public ProceduralStrength Strength { get; set; }
            public ProceduralEffectTarget Speed { get; set; }
            public ProceduralPool EffectPool { get; set; }
            
            [YamlIgnore] 
            public static BackEffects DefaultConfig = new BackEffects
            {
                Strength = new ProceduralStrength
                {
                    Multiplier = 0.4f,
                    Target = 0.75f,
                    Goal = new LerpGoal
                    {
                        Start = 0.3f,
                        End = 1f
                    }
                },
                Speed = new ProceduralEffectTarget
                {
                    Target = 0.75f,
                    Goal = new LerpGoal
                    {
                        Start = 1f,
                        End = 0.5f
                    }
                },
                EffectPool = new ProceduralPool
                {
                    Target = 2f,
                    Variants = 8,
                    MaxConcurrency = 4
                }
            };
        }

        public class ArmEffects
        {
            public float Strength { get; set; }
            public float Target { get; set; }
            
            [YamlIgnore] 
            public static ArmEffects DefaultConfig = new ArmEffects
            {
                Strength = 1f,
                Target = 0.75f
            };
        }
        
        public class HandEffects
        {
            public float Strength { get; set; }
            public float Target { get; set; }
            
            [YamlIgnore] 
            public static HandEffects DefaultConfig = new HandEffects
            {
                Strength = 1f,
                Target = 0.75f
            };
        }
        
        public class FeetEffects
        {
            public float Strength { get; set; }
            public float Target { get; set; }
            
            [YamlIgnore] 
            public static FeetEffects DefaultConfig = new FeetEffects
            {
                Strength = 1f,
                Target = 0.75f
            };
        }
    }
}