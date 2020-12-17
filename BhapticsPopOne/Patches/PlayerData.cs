using BhapticsPopOne.Haptics;
using Harmony;
using MelonLoader;
using BigBoxVR.BattleRoyale.Models.Shared;

namespace BhapticsPopOne.Patches.PlayerData2
{
    [HarmonyPatch(typeof(PlayerData), "SyncHealth")]
    public class SyncHealth
    {
        // heartbeat when HP under 25
        static void Prefix(PlayerData __instance, int oldValue, int newValue)
        {
            var playerData = Mod.Instance.Data.Players.LocalPlayerContainer?.playerData;
            
            if (playerData == null || __instance != playerData)
                return;
    
    
            if (newValue < 25 && oldValue >= 25)
            {
                PatternManager.LowHealthHeartbeat();
            }
        }
    }

    [HarmonyPatch(typeof(PlayerData), "MotionState", MethodType.Setter)]
    public class MotionStateSetter
    {
        // haptics while flying & falling
        static void Prefix(PlayerData __instance, MotionState value)
        {
            var playerData = Mod.Instance.Data.Players.LocalPlayerContainer?.playerData;
            
            if (playerData == null || __instance != playerData)
                return;

            if (value == MotionState.Flying)
            {
                PatternManager.FlyingAir();
            }

            if (value == MotionState.Falling)
            {
                PatternManager.FallingAir();
            }

        }
    }
}