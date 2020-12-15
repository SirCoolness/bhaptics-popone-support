using BhapticsPopOne.Haptics;
using Harmony;
using MelonLoader;

namespace BhapticsPopOne.Patches.PlayerContainer2
{
    [HarmonyPatch(typeof(PlayerContainer), "HandlePlayerHit")]
    public class HandlePlayerHit
    {
            static void Prefix(PlayerContainer __instance, DamageableHitInfo info)
            {
                if (__instance != Mod.Instance.Data.Players.LocalPlayerContainer)
                    return;
                
                if (info.Source == HitSourceCategory.Bot || info.Source == HitSourceCategory.Player)
                    PatternManager.BulletHit(info);
                else if (info.Source == HitSourceCategory.BattleZone)
                    PatternManager.ZoneHit();
                else if (info.Source == HitSourceCategory.Falling)
                    PatternManager.FallDamage();
                else
                    PatternManager.TestPattern();
            }
    }
}