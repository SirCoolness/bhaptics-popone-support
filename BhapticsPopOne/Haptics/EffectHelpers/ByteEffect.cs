using MelonLoader;

namespace BhapticsPopOne.Haptics.EffectHelpers
{
    public class ByteEffect
    {
        public Step[] Steps { get; internal set; }
        public bHaptics.PositionType Position { get; internal set; }

        public struct Step
        {
            public uint Delay;
            public uint Length;
            public byte[] State;
        }
    }
}