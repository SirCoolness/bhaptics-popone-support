using System;
using BhapticsPopOne.Haptics.Patterns;
using HarmonyLib;
using MelonLoader;

namespace BhapticsPopOne.PlayerInventory2
{
    [HarmonyPatch(typeof(PlayerInventory), "NetworkequipIndex", MethodType.Setter)]
    public class NetworkequipIndexSetter
    {
        static void Prefix(PlayerInventory __instance, int value)
        {
            var containerData = PlayerContainer.Find(__instance.netId)?.Data;
            if (containerData?.isLocalPlayer != true)
                return;
            
            var current = __instance.NetworkequipIndex;
            if (value == current)
                return;
            
            WeaponSwap.Execute(current, value);
        }
    }
}
