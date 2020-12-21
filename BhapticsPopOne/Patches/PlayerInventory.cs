using System;
using Harmony;
using MelonLoader;

namespace BhapticsPopOne.PlayerInventory2
{
    [HarmonyPatch(typeof(PlayerInventory), "NetworkequipIndex", MethodType.Setter)]
    public class NetworkequipIndexSetter
    {
        static void Prefix(PlayerInventory __instance, int value)
        {
            MelonLogger.Log(ConsoleColor.Gray, value);
            if (!__instance.isLocalPlayer)
                return;
        }
    }
}