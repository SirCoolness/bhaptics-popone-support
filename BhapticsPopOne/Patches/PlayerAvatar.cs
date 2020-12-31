using Harmony;
using MelonLoader;

namespace BhapticsPopOne.PlayerAvatar2
{
    [HarmonyPatch(typeof(PlayerAvatar), "OnPlayerAvatarChanged")]
    public class OnPlayerAvatarChanged
    {
        static void Postfix(PlayerAvatar __instance, uint netId, string name)
        {
            if (__instance.transform.root != __instance.transform)
                return;
            
            MelonLogger.Log($"OnPlayerAvatarChanged {Logging.StringifyVector3(__instance.HandLeftAttachPoint.transform.position)} {__instance.Container.Data.DisplayName}");
        }
    }
}