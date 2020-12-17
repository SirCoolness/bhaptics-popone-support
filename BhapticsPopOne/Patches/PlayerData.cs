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
            if (__instance != Mod.Instance.Data.Players.LocalPlayerContainer.playerData)
                return;

            MelonLogger.Log(System.ConsoleColor.DarkBlue, "[SyncHealth] Name: " + __instance.DisplayName);
            MelonLogger.Log(System.ConsoleColor.DarkBlue, "oldValue: " + oldValue + " newValue: " + newValue);

            if (newValue < 25 && oldValue >= 25)
            {
                PatternManager.LowHealthHeartbeat();
            }
        }
    }

    [HarmonyPatch(typeof(PlayerData), "SyncMotionState")]
    public class SyncMotionState
    {
        // haptics while flying
        static void Prefix(PlayerData __instance, MotionState oldValue, MotionState newValue)
        {
            if (__instance != Mod.Instance.Data.Players.LocalPlayerContainer.playerData)
                return;

            MelonLogger.Log(System.ConsoleColor.Red, "[SyncMotionState] Name: " + __instance.DisplayName);
            MelonLogger.Log(System.ConsoleColor.Red, "oldValue: " + oldValue + " newValue: " + newValue);

            if (oldValue != MotionState.Flying && newValue == MotionState.Flying)
            {
                PatternManager.FlyingAir();
            }
        }
    }
}