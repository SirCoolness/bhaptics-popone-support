using BhapticsPopOne.Haptics.EffectHelpers;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class Health
    {
        public static void LowHealthHeartbeat()
        {
            EffectPlayer.Play("Vest/HeartbeatMultiple");
        }

        public static void ShieldBreak()
        {
            EffectPlayer.Play("Vest/ShieldBreak");
        }
        
        public static void ShieldFull()
        {
            EffectPlayer.Play("Vest/FullShield");
        }
    }
}