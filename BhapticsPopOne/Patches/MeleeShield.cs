using BhapticsPopOne.Haptics.Patterns;
using Harmony;

namespace BhapticsPopOne
{
    [HarmonyPatch(typeof(MeleeShield), "ShieldActive", MethodType.Setter)]
    public class ShieldActiveSetter
    {
        public static void Prefix(MeleeShield __instance, bool value)
        {
            if (!__instance.melee.container.isLocalPlayer)
                return;
            
            KatanaShield.Execute(value);
        }
    }
}