using System;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Data;
using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.Patterns;
using BhapticsPopOne.MonoBehaviours;
using Il2CppBigBoxVR.BattleRoyale.Models.Shared;
using HarmonyLib;
using MelonLoader;
using Il2CppMirror;
using UnityEngine;
using Il2Cpp;

namespace BhapticsPopOne.Patches.PlayerContainer2
{
    [HarmonyPatch(typeof(PlayerContainer), "CmdStartFistBump")]
    public class CmdStartFistBump
    {
        static void Prefix(PlayerContainer __instance, Handedness handedness)
        {
            if (!__instance.isLocalPlayer)
                return;

            if (handedness == Handedness.Right)
                FistBump.lastPunchhandR = handedness;
            else if (handedness == Handedness.Left)
                FistBump.lastPunchhandL = handedness;
        }
    }
    
    [HarmonyPatch(typeof(PlayerContainer), "CmdStopFistBump")]
    public class CmdStopFistBump
    {
        static void Prefix(PlayerContainer __instance, Handedness handedness)
        {
            if (!__instance.isLocalPlayer)
                return;

            if (handedness == Handedness.Right)
                FistBump.lastPunchhandR = Handedness.Unknown;
            else if (handedness == Handedness.Left)
                FistBump.lastPunchhandL = Handedness.Unknown;
        }
    }
}
