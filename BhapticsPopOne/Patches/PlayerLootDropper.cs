using System;
using System.Runtime.InteropServices;
using BhapticsPopOne.Haptics.Patterns;
using Harmony;
using Il2CppSystem.Collections.Generic;
using MelonLoader;

namespace BhapticsPopOne.PlayerLootDropper2
{
    [HarmonyPatch(typeof(PlayerLootDropper), "AddLootItem")]
    public class AddLootItem
    {
        static void Prefix(PlayerLootDropper __instance, AbstractInfo itemInfo,
            [Optional] int lootItemQuantity,
            [Optional] int lootItemRarity,
            [Optional] HashSet<uint> pickupHistory)
        {
            // MelonLogger.Log(ConsoleColor.Yellow, itemInfo.DisplayName);
        }
    }
}

