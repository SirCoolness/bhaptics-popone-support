using BhapticsPopOne.Haptics.EffectHelpers;
using BhapticsPopOne.Haptics.EffectManagers;
using MelonLoader;

namespace BhapticsPopOne.Haptics.Loaders
{
    public class InitializeByteEffects
    {
        public static void Init()
        {
            byte armTouchPowered = 30;
            byte armTouchTime = 40;
            byte armTouchDelay = 40;
            
            EffectLoopRegistry.Register("Arm/SendTouch_L", new ByteEffect
            {
                Position = bHaptics.PositionType.ForearmL,
                Steps = new []
                {
                    new ByteEffect.Step
                    {
                        Delay = armTouchDelay,
                        Length = armTouchTime,
                        State = new byte[]
                        {
                            0, 0, armTouchPowered,
                            0, 0, 0
                        }
                    }, 
                    new ByteEffect.Step
                    {
                        Delay = armTouchDelay,
                        Length = armTouchTime,
                        State = new byte[]
                        {
                            0, armTouchPowered, 0,
                            0, 0, 0
                        }
                    }
                }
            });
            
            EffectLoopRegistry.Register("Arm/SendTouch_R", new ByteEffect
            {
                Position = bHaptics.PositionType.ForearmR,
                Steps = new []
                {
                    new ByteEffect.Step
                    {
                        Delay = armTouchDelay,
                        Length = armTouchTime,
                        State = new byte[]
                        {
                            armTouchPowered, 0, 0,
                            0, 0, 0
                        }
                    }, 
                    new ByteEffect.Step
                    {
                        Delay = armTouchDelay,
                        Length = armTouchTime,
                        State = new byte[]
                        {
                            0, armTouchPowered, 0,
                            0, 0, 0
                        }
                    }
                }
            });
            
            EffectLoopRegistry.Register("Hand/SendTouch_L", new ByteEffect
            {
                Position = bHaptics.PositionType.HandL,
                Steps = new []
                {
                    new ByteEffect.Step
                    {
                        Delay = armTouchDelay,
                        Length = armTouchTime,
                        State = new byte[]
                        {
                            armTouchPowered, 0, 0
                        }
                    }, 
                    new ByteEffect.Step
                    {
                        Delay = armTouchDelay,
                        Length = armTouchTime,
                        State = new byte[]
                        {
                            0, armTouchPowered, 0
                        }
                    }
                }
            });
            
            EffectLoopRegistry.Register("Hand/SendTouch_R", new ByteEffect
            {
                Position = bHaptics.PositionType.HandR,
                Steps = new []
                {
                    new ByteEffect.Step
                    {
                        Delay = armTouchDelay,
                        Length = armTouchTime,
                        State = new byte[]
                        {
                            armTouchPowered, 0, 0
                        }
                    }, 
                    new ByteEffect.Step
                    {
                        Delay = armTouchDelay,
                        Length = armTouchTime,
                        State = new byte[]
                        {
                            0, armTouchPowered, 0
                        }
                    }
                }
            });
        }
    }
}