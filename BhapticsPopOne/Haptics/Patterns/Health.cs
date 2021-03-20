using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class Health
    {
        public static void LowHealthHeartbeat()
        {
            if (DynConfig.Toggles.Vest.Heartbeat)
                EffectPlayer.Play("Vest/HeartbeatMultiple");
        }

        public static void ShieldBreak()
        {
            if (DynConfig.Toggles.Vest.ShieldBreak)
                EffectPlayer.Play("Vest/ShieldBreak");
        }
        
        public static void ShieldFull()
        {
            if (DynConfig.Toggles.Vest.FullShield)
                EffectPlayer.Play("Vest/FullShield");
        }
    }
}