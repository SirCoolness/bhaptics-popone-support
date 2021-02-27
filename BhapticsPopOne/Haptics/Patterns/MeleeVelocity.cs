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
        
        public static void LoadTrackers()
        {
            tracker = Mod.Instance.Data.Players.LocalPlayerContainer.Avatar.Rig.GetComponent<VelocityTracker>();
            trackerFound = tracker != null;
        }
        
        public static void Execute(Handedness hand, Vector3 velocity)
        {
            if (!IsSlicing || !trackerFound)
                return;
            
            var container = Mod.Instance.Data.Players.LocalPlayerContainer;
            if (container.Inventory.Slots[container.Inventory.EquipIndex].Info.ItemClass != InventoryItemClass.Melee)
                return;
            
            if (container.Data.DominantHand != hand && !container.Data.TwoHand)
                return;
            
            var relativeV = tracker.Velocity - velocity;
            
            PatternManager.Effects[$"Arm/MeleeVelocity{HapticUtils.HandExt(hand)}"]?.Play(new Effect.EffectProperties
            {
                Time = Time.fixedDeltaTime,
                Strength = Mathf.Clamp((relativeV.magnitude / 5f), 0, 1f),
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