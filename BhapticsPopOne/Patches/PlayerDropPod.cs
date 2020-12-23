using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.Patterns;
using Harmony;
using MelonLoader;
using BigBoxVR.BattleRoyale.Models.Shared;

namespace BhapticsPopOne.Patches
{
    [HarmonyPatch(typeof(PlayerDropPod), "Update")]
    public class OnStateChanged
    {
        // change pattern with pod state
        static void Prefix(PlayerDropPod __instance)
        {
            if (__instance.attachedContainer != Mod.Instance.Data.Players.LocalPlayerContainer)
                return;

            PodState value = __instance.State;

            if (value == PodState.WaitingToLaunch)
            {
                PatternManager.EnteringPod();
            }
            else if (value == PodState.Launching)
            {
                PatternManager.LaunchingPod();
            }
            else if(value == PodState.WaitingToDrop)
            {
                PatternManager.DuringPod();
            }
            else if(value == PodState.GlidingOpened || value == PodState.Gliding || value == PodState.Falling)
            {
                if (__instance.attachedContainer.playerData.MotionState == MotionState.Idle || __instance.attachedContainer.playerData.MotionState == MotionState.Bipedal)
                {
                    PatternManager.FallingPod();
                }
            }
            else if (value == PodState.Impacted)
            {
                if (__instance.attachedContainer.playerData.MotionState == MotionState.Idle || __instance.attachedContainer.playerData.MotionState == MotionState.Bipedal)
                {
                    FallDamage.Execute(75, 500f);
                }
            }
            else
            {
                return;
            }

        }
    }

    
}