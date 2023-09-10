using System;
using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.Patterns;
using HarmonyLib;
using MelonLoader;
using Il2CppBigBoxVR.BattleRoyale.Models.Shared;
using Il2Cpp;

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
                Health.LowHealthHeartbeat();

            if (newValue >= PlayerData.MaxHealth)
                DrinkSoda.FullHealth();
        }
    }

    [HarmonyPatch(typeof(PlayerData), "MotionState", MethodType.Setter)]
    public class MotionStateSetter
    {
        // haptics while flying & falling
        static void Prefix(PlayerData __instance, MotionState value)
        {
            // var playerData = Mod.Instance.Data.Players.LocalPlayerContainer?.playerData;
            
            if (!__instance.isLocalPlayer)
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
            
            if ((value == MotionState.Bipedal || value == MotionState.Idle) && (previousValue == MotionState.Flying || previousValue == MotionState.Falling))
                GameState.Land();
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
            {
                Health.ShieldFull();
                DrinkSoda.FullShield();
            }
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
            {
                DrinkSoda.Clear();
                KatanaShield.Execute(false);
            }
        }
    }
}
