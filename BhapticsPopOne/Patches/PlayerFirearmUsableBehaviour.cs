using BhapticsPopOne.Haptics.Patterns;
using Harmony;
using MelonLoader;

namespace BhapticsPopOne.PlayerFirearmUsableBehaviour2
{
    [HarmonyPatch(typeof(PlayerFirearmUsableBehaviour), "State", MethodType.Setter)]
    public class StateSetter
    {
        static void Prefix(PlayerFirearmUsableBehaviour __instance, FirearmState value)
        {
            if (!__instance.playerContainer.isLocalPlayer)
                return;
            
            ReloadWeapon.Execute(value);
        }
    }
}