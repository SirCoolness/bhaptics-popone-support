using System;
using System.Threading.Tasks;
using Bhaptics.Tact;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using BigBoxVR.BattleRoyale.Models.Shared;
using Il2CppSystem.Collections.Generic;
using MelonLoader;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class DrinkSoda
    {
        public static bool Active
        {
            get
            {
                foreach (var estimatedSodaEffect in estimatedSodaEffects)
                    foreach (var b in estimatedSodaEffect.value)
                        if (b)
                            return true;

                return false;
            }
        }
        public static Dictionary<InventoryItemType, List<bool>> estimatedSodaEffects = new Dictionary<InventoryItemType, List<bool>>(); 
        
        public static void Execute(InventoryItemType type, float consumptionTime, BuffState state)
        {
            if (state != BuffState.Consumed)
                return;
            
            EffectPlayer.Play("Vest/ConsumeItem", new Effect.EffectProperties
            {
                Strength = ConfigHelpers.EnforceIntensity(ConfigLoader.Config.FoodEatIntensity)
            });
            
            if (!estimatedSodaEffects.ContainsKey(type))
                estimatedSodaEffects[type] = new List<bool>();
            
            // TODO: check against server time instead
            estimatedSodaEffects[type].Add(true);
            
            Task.Delay(TimeSpan.FromSeconds(consumptionTime)).ContinueWith(t =>
            {
                if (estimatedSodaEffects.ContainsKey(type) && estimatedSodaEffects[type].Count > 0)
                    estimatedSodaEffects[type].RemoveAt(0);
            });
        }

        public static void FullHealth()
        {
            if (estimatedSodaEffects.ContainsKey(InventoryItemType.BuffEnergyDrink))
                estimatedSodaEffects[InventoryItemType.BuffEnergyDrink].Clear();
        }
        
        public static void FullShield()
        {
            if (estimatedSodaEffects.ContainsKey(InventoryItemType.BuffShieldDrink))
                estimatedSodaEffects[InventoryItemType.BuffShieldDrink].Clear();
        }

        public static void Clear()
        {
            estimatedSodaEffects.Clear();
        }
    }
}