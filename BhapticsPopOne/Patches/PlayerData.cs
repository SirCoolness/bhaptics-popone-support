using BhapticsPopOne.Haptics;
using Harmony;
using MelonLoader;

namespace BhapticsPopOne.Patches.PlayerData2
{
    [HarmonyPatch(typeof(PlayerData), "SyncHealth")]
    public class HandlePlayerHit
    {
        static void Prefix(PlayerData __instance, int oldValue, int newValue)
        {
            if (__instance != Mod.Instance.Data.Players.LocalPlayerContainer.playerData)
                return;

            MelonLogger.Log(System.ConsoleColor.Red, "Health: " + newValue);

            if(newValue < 20)
            {
                PatternManager.LowHealthHeartbeat();
            }
        }
    }
}