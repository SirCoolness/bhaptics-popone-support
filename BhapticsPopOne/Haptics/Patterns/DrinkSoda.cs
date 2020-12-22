using System;
using System.Threading.Tasks;
using Il2CppSystem.Collections.Generic;
using MelonLoader;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class DrinkSoda
    {
        public static List<bool> estimatedSodaEffects = new List<bool>(); 
        
        public static void Execute(float consumptionTime, BuffState state)
        {
            if (state != BuffState.Consumed)
                return;
            
            Mod.Instance.Haptics.Player.SubmitRegistered("Vest/ConsumeItem");
            
            // TODO: check against server time instead
            estimatedSodaEffects.Add(true);
            Task.Delay(TimeSpan.FromSeconds(consumptionTime)).ContinueWith(t =>
            {
                if (estimatedSodaEffects.Count > 0)
                    estimatedSodaEffects.RemoveAt(0);
            });
        }

        public static void FullHealth()
        {
            estimatedSodaEffects.Clear();
        }
    }
}