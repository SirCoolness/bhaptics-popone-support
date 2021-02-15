using System;
using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.Patterns;
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

            if (newValue >= PlayerData.MaxHealth)
            {
                DrinkSoda.FullHealth();
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

            MotionState previousValue = __instance.MotionState;
            
            if (value != previousValue)
            {
                if (previousValue == MotionState.Flying)
                    FlyingAir.Clear();
                else if (previousValue == MotionState.Falling)
                    FallingAir.Clear();
            }
            
            if (value == MotionState.Flying)
                FlyingAir.Execute(previousValue == MotionState.Falling);
            else if (value == MotionState.Falling)
                FallingAir.Execute(previousValue == MotionState.Flying);
        }
    }

    [HarmonyPatch(typeof(PlayerData), "Networkarmor", MethodType.Setter)]
    public class NetworkarmorSetter
    {
        static void Prefix(PlayerData __instance, int value)
        {
            var playerData = Mod.Instance.Data.Players.LocalPlayerContainer?.playerData;
            
            if (playerData == null || __instance != playerData)
                return;
            
            if (value >= PlayerData.MaxArmor)
                PatternManager.ShieldFull();
        }
    }
    
    [HarmonyPatch(typeof(PlayerData), "NetworkplayerState", MethodType.Setter)]
    public class NetworkplayerStateSetter
    {
        static void Postfix(PlayerData __instance, PlayerState value)
        {
            if (!__instance.isLocalPlayer)
                return;
            
            if (value != PlayerState.Active)
                DrinkSoda.Clear();
        }
    }
}