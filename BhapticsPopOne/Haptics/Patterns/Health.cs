using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class Health
    {
        public static void LowHealthHeartbeat()
        {
            if (ConfigLoader.Config.EffectToggles.Vest.Heartbeat)
                EffectPlayer.Play("Vest/HeartbeatMultiple");
        }

        public static void ShieldBreak()
        {
            if (ConfigLoader.Config.EffectToggles.Vest.ShieldBreak)
                EffectPlayer.Play("Vest/ShieldBreak");
        }
        
        public static void ShieldFull()
        {
            if (ConfigLoader.Config.EffectToggles.Vest.FullShield)
                EffectPlayer.Play("Vest/FullShield");
        }
    }
}