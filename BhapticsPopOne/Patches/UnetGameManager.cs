using System;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.ConfigManager.ConfigElements;
using BhapticsPopOne.Haptics;
using BigBoxVR;
using HarmonyLib;
using MelonLoader;

namespace BhapticsPopOne.UnetGameManager2
{
    [HarmonyPatch(typeof(UnetGameManager), "OnGameStateChanged")]
    public class OnGameStateChanged
    {
        // TODO: switch to goyfs
        static void Postfix(GameState oldValue, GameState newValue)
        {
            DynConfig.UpdateConfig(newValue == GameState.Default ? DynConfig.SceneMode.Lobby : DynConfig.SceneMode.General);
            if (newValue != GameState.MatchEnded)
                return;


            var playerstate = Mod.Instance.Data.Players.LocalPlayerContainer.Data.NetworkplayerState;
            if (playerstate == PlayerState.Eliminated)
                return;
            
            Haptics.Patterns.GameState.Victory();
        }
    }
}
