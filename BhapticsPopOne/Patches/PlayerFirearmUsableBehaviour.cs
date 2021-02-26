using System.Runtime.InteropServices;
using BhapticsPopOne.Haptics.Patterns;
using Harmony;
using MelonLoader;

namespace BhapticsPopOne.PlayerFirearmUsableBehaviour2
{
    [HarmonyPatch(typeof(PlayerFirearmUsableBehaviour), "SetFirearmState")]
    public class SetFirearmState
    {
        static void Prefix(PlayerFirearmUsableBehaviour __instance, FirearmState state, [Optional] int primeIndex)
        {
            if (!(__instance?.playerContainer?.isLocalPlayer == true))
                return;
            
            ReloadWeapon.Execute(state, primeIndex != null ? primeIndex : 0, __instance.LastReloadIndex, __instance.GetInstanceID());
        }
    }
}