#if PORT_DISABLE
using BhapticsPopOne.ConfigManager;
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

            if(
                DynConfig.Toggles.Vest.FallPod && 
                (value == PodState.GlidingOpened || value == PodState.Gliding || value == PodState.Falling) &&
               (__instance.attachedContainer.playerData.MotionState == MotionState.Idle ||
                __instance.attachedContainer.playerData.MotionState == MotionState.Bipedal))
            {
                FallingAir.Execute(20f, false);
                return;
            }

            if (__instance.attachedContainer.playerData.MotionState != MotionState.Falling)
            {
                FallingAir.Clear();
            }
            
            if (value == PodState.WaitingToLaunch)
                PodLifecycle.EnteringPod();
            else if (value == PodState.Launching)
                PodLifecycle.LaunchingPod();
            else if(value == PodState.WaitingToDrop)
                PodLifecycle.DuringPod();
            else if (value == PodState.Impacted)
                if (DynConfig.Toggles.Vest.LandPod && (__instance.attachedContainer.playerData.MotionState == MotionState.Idle ||
                                                       __instance.attachedContainer.playerData.MotionState == MotionState.Bipedal))
                {
                    FallDamage.Execute(75, 500f);
                }
        }
    }
}
#endif