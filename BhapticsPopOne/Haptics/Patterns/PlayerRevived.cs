using Bhaptics.Tact;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class PlayerRevived
    {
        public static void Execute(Vector3 point, Vector3 direction)
        {
            var vestRef = Mod.Instance.Data.Players.VestReference();
            if (vestRef == null)
            {
                MelonLogger.LogWarning("Cant the reference transform for the vest.");
                return;
            }

            Vector3 forward = Quaternion.Euler(0, -90f, 0) * vestRef.forward;
            var angle = BhapticsUtils.Angle(forward, -direction);

            // float offsetY = Mathf.Clamp((point.y - vestRef.position.y) / PatternManager.VestHeight, -0.5f, 0.5f);
            Mod.Instance.Haptics.Player.SubmitRegisteredVestRotation("Vest/Revived", new RotationOption(angle, 0));
            Mod.Instance.Haptics.Player.SubmitRegistered("Vest/RevivedHeartbeat");

        }
    }
}