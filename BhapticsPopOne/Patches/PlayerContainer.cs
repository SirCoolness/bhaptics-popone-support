using System;
using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.Patterns;
using BigBoxVR.BattleRoyale.Models.Shared;
using Harmony;
using MelonLoader;
using UnityEngine;

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
                {
                    PlayerHit.Execute(info);
                }
                else if (info.Source == HitSourceCategory.BattleZone)
                    PatternManager.ZoneHit();
                else if (info.Source == HitSourceCategory.Falling)
                    FallDamage.Execute(-info.Damage, info.Power);
                else
                    PatternManager.TestPattern();

                if (info.ArmorBroke)
                {
                    PatternManager.ShieldBreak();
                }
            }
    }
}