using System;
using MelonLoader;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class PickupItem
    {
        private static Handedness _lastGripHand;
        
        public static void UpdateLastGrip(Handedness hand)
        {
            _lastGripHand = hand;
            
            MelonLogger.Log(ConsoleColor.Red, _lastGripHand.ToString());
        }

        public static void Execute()
        {
            Execute(_lastGripHand);
        }
        
        public static void Execute(Handedness hand)
        {
            MelonLogger.Log($"Pickup with {hand.ToString()}");
        }
    }
}