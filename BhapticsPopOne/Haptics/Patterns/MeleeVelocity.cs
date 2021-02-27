using System.Collections.Generic;
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
        private static PlayerContainer localPlayer;
        
        public static void LoadTrackers()
        {
            localPlayer = Mod.Instance.Data.Players.LocalPlayerContainer;
            tracker = localPlayer.Avatar.Rig.GetComponent<VelocityTracker>();
            trackerFound = tracker != null;
            
            if (!trackerFound)
                MelonLogger.LogError("Failed to find tracked in MeleeVelocity");
        }
        
        public static void Execute(Handedness hand, Vector3 velocity)
        {
            if (!IsSlicing || !trackerFound)
                return;
            
            if (localPlayer.Inventory.Slots[localPlayer.Inventory.EquipIndex].Info.ItemClass != InventoryItemClass.Melee)
                return;
            
            if (localPlayer.Data.DominantHand != hand && !localPlayer.Data.TwoHand)
                return;
            
            var relativeV = tracker.Velocity - velocity;
            
            PatternManager.Effects[$"Arm/MeleeVelocity{HapticUtils.HandExt(hand)}"]?.Play(new Effect.EffectProperties
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
            localPlayer = null;
        }
    }
}