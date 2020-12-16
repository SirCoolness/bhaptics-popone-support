using BhapticsPopOne.Haptics;
using Harmony;
using MelonLoader;

namespace BhapticsPopOne.Patches.PlayerData2
{
    [HarmonyPatch(typeof(PlayerData), "SyncHealth")]
    public class HandlePlayerHit
    {
        // heartbeat when HP under 25
        static void Prefix(PlayerData __instance, int oldValue, int newValue)
        {
            if (__instance != Mod.Instance.Data.Players.LocalPlayerContainer.playerData)
                return;

            if(newValue < 25 && oldValue >= 25)
            {
                PatternManager.LowHealthHeartbeat();
            }
        }
    }
}