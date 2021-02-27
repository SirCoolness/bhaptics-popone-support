using Harmony;
using MelonLoader;

namespace BhapticsPopOne
{
    [HarmonyPatch(typeof(PlayerMelee), "OnWoosh")]
    public class OnWoosh
    {
        public static void Prefix(PlayerMelee __instance)
        {
            if (!__instance.isLocalPlayer)
                return;
            
            Haptics.Patterns.MeleeVelocity.IsSlicing = true;
        }
    }
}