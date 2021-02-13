using Bhaptics.Tact;
using BhapticsPopOne.Haptics.EffectHelpers;
using BhapticsPopOne.Haptics.EffectManagers;

namespace BhapticsPopOne.Haptics.Loaders
{
    public class InitializeByteEffects
    {
        public static void Init()
        {
            byte armTouchPowered = 22;
            
            EffectLoopRegistry.Register("Arm/SendTouch_L", new ByteEffect
            {
                Position = PositionType.ForearmL,
                Steps = new []
                {
                    new ByteEffect.Step
                    {
                        Delay = 10,
                        Length = 30,
                        State = new byte[]
                        {
                            0, 0, armTouchPowered,
                            0, 0, 0
                        }
                    }, 
                    new ByteEffect.Step
                    {
                        Delay = 10,
                        Length = 30,
                        State = new byte[]
                        {
                            0, armTouchPowered, 0,
                            0, 0, 0
                        }
                    }, 
                }
            });
        }
    }
}