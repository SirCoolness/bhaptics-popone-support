using System;
using Bhaptics.Tact;
using MelonLoader;

namespace BhapticsPopOne
{
    public class TestOcilate
    {
        private static int OnTime = 40;
        private static int OffTime = 40;
        
        private static DateTime endTime = DateTime.Now;

        private static bool State = false;
        
        public static void FixedUpdate()
        {
            DateTime current = DateTime.Now;
            
            if (endTime > current)
                return;

            float time = State ? OnTime : OffTime;
            
            Trigger(State);

            endTime = current.AddMilliseconds(time);
            State = !State;
        }

        private static void Trigger(bool state)
        {
            MelonLogger.Log(state);
            byte strength = (byte)(State ? 22 : 0);
            byte strength2 = (byte) (State ? 0 : 22);
            
            Mod.Instance.Haptics.Player.Submit("Bytes", PositionType.ForearmL, new byte[]
            {
                0, strength2, strength, 
                0, 0, 0,
            }, 30);
        }
    }
}