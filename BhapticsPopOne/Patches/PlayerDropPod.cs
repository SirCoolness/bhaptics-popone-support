using BhapticsPopOne.Haptics;
using Harmony;
using MelonLoader;
using BigBoxVR.BattleRoyale.Models.Shared;

namespace BhapticsPopOne.Patches
{
    [HarmonyPatch(typeof(PlayerDropPod), "OnStateChanged")]
    public class OnStateChanged
    {
        // change pattern with pod state
        static void Prefix(PlayerDropPod __instance, PodState oldState, PodState newState)
        {
            if (__instance.attachedContainer != Mod.Instance.Data.Players.LocalPlayerContainer)
                return;

            MelonLogger.Log(System.ConsoleColor.Blue, "attachedContainer: " + __instance.attachedContainer.playerData.DisplayName);
            MelonLogger.Log(System.ConsoleColor.Blue, "PodStateChanged: " + oldState + " " + newState);
        }
    }

    
}