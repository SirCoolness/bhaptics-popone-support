using System.Collections.Generic;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class MeleeVelocity
    {
        public static bool IsSlicing = false;
        private static bool trackerFound = false;
        private static VelocityTracker tracker;
        
        public static void LoadTrackers()
        {
            tracker = Mod.Instance.Data.Players.LocalPlayerContainer.Avatar.Rig.GetComponent<VelocityTracker>();
            trackerFound = tracker != null;
            
            if (!trackerFound)
                MelonLogger.LogError("Failed to find tracked in MeleeVelocity");
        }
        
        public static void Execute(Handedness hand, Vector3 velocity)
        {
            var localPlayer = Mod.Instance.Data.Players.LocalPlayerContainer;
            if (!IsSlicing || !trackerFound || localPlayer.Data.PlayerState != PlayerState.Active)
                return;
            
            if (localPlayer.Inventory.Slots[localPlayer.Inventory.EquipIndex].Info.ItemClass != InventoryItemClass.Melee)
                return;
            
            if (localPlayer.Data.DominantHand != hand && !localPlayer.Data.TwoHand)
                return;
            
            var relativeV = tracker.Velocity - velocity;
            
            if (DynConfig.Toggles.Arms.MeleeVelocity)
                EffectPlayer.Play($"Arm/MeleeVelocity{HapticUtils.HandExt(hand)}", new Effect.EffectProperties
                {
                    Time = Time.fixedDeltaTime,
                    Strength = Mathf.Clamp((relativeV.magnitude / 2.5f), 0, 1f),
                });
                
            if (DynConfig.Toggles.Hands.MeleeVelocity)
                EffectPlayer.Play($"Hand/MeleeVelocity{HapticUtils.HandExt(hand)}", new Effect.EffectProperties
                {
                    Time = Time.fixedDeltaTime,
                    Strength = Mathf.Clamp((relativeV.magnitude / 2.5f), 0, 1f),
                });
        }

        public static void Reset()
        {
            IsSlicing = false;
            trackerFound = false;
            tracker = null;
        }
    }
}