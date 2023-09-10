using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class PlayerRevived
    {
        public static void Execute(Vector3 point, Vector3 direction)
        {
            if (!DynConfig.Toggles.Vest.Revived)
                return;
            
            var vestRef = Mod.Instance.Data.Players.VestReference();
            if (vestRef == null)
            {
                MelonLogger.Warning("Cant the reference transform for the vest.");
                return;
            }

            Vector3 forward = Quaternion.Euler(0, -90f, 0) * vestRef.forward;
            var angle = BhapticsUtils.Angle(forward, -direction);

            EffectHelpers.EffectPlayer.Play("Vest/Revived", new Effect.EffectProperties
            {
                XRotation = angle
            });
            EffectHelpers.EffectPlayer.Play("Vest/RevivedHeartbeat");
        }
    }
}