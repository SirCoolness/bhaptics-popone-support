using BhapticsPopOne.Haptics;
using BigBoxVR;
using Harmony;
using MelonLoader;

namespace BhapticsPopOne
{
    [HarmonyPatch(typeof(PlayerBuff), "OnBuffStateChanged")]
    public class OnBuffStateChanged
    {
        static void Prefix(PlayerBuff __instance, InventorySlot.BuffRecord record, BuffState state)
        {
            var local = Mod.Instance.Data.Players.LocalPlayerContainer;
            if (__instance.container == null || local == null)
            {
                MelonLogger.LogWarning("container is null");
                return;
            }
            
            if (__instance.container != local)
                return;

            var name = __instance.model?.Info?.name;
            if (name == null)
            {
                MelonLogger.LogWarning("cant find buff info");
                return;
            }

            if (name == "EnergyDrink")
            {
                PatternManager.DrinkSoda(state);
            } else if (name == "Banana")
            {
                PatternManager.EatBanana(state);
            }
        }
    }
}
