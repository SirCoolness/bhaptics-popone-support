using System;
using BhapticsPopOne.Haptics;
using BigBoxVR;
using Harmony;
using MelonLoader;

namespace BhapticsPopOne.UnetGameManager2
{
    [HarmonyPatch(typeof(UnetGameManager), "OnGameStateChanged")]
    public class OnGameStateChanged
    {
        static void Postfix(GameState oldValue, GameState newValue)
        {
            if (newValue != GameState.MatchEnded)
                return;


            var playerstate = Mod.Instance.Data.Players.LocalPlayerContainer.Data.PlayerState;
            if (playerstate == PlayerState.Eliminated)
                return;
            
            PatternManager.Victory();
        }
    }
}