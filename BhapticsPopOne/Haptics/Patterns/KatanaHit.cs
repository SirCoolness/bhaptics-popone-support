using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class KatanaHit
    {
        public static void Execute(PlayerContainer player, DamageableHitInfo info)
        {
            if (!DynConfig.Toggles.Vest.MeleeHit)
                return;
            
            var source = PlayerContainer.Find(info.OwningPlayer);
            var sourceDirection = (source.Avatar.rig.position - info.ImpactPosition).normalized;
            
            Vector3 playerForward = player.Avatar.Rig.forward;
            var rotation = BhapticsUtils.Angle(playerForward, sourceDirection);
            
            var angle = (Quaternion.FromToRotation(Vector3.down, info.Forward - playerForward)).eulerAngles;
            var effectIndex =
                Mathf.Clamp(Mathf.FloorToInt(Mathf.Floor(angle.y / 22.5f) + Mathf.Round(((angle.y) % 22.5f) / 22.5f)) %
                 17 , 0, 16);
            
            EffectPlayer.Play($"Vest/SwordSlice_{effectIndex}", new Effect.EffectProperties
            {
                Time = 0.25f,
                XRotation = -rotation
            });
        }
    }
}