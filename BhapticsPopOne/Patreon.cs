using System;
using System.Collections.Generic;
using System.Linq;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne
{
    /**
     * This class is a thank you to all the Patreon members
     */
    public class Patreon
    {
        public static string[] BurgerKingContributors = new string[]
        {
            "AceJas"
        };

        /**
         * Thank you to the Burger King tier contributors.
         * Much Burger King will be consumed in your name.
         *
         * Burger King:
         * - AceJas
         */
        public static void ThankBurgerKing()
        {
            if (BurgerKingContributors.Length <= 0)
                return;

            int rowSize = 3;
            int offsetLen = Mathf.CeilToInt(BurgerKingContributors.Length / (float) rowSize);

            MelonLogger.Log(ConsoleColor.Yellow, "Thank you to the Burger King contributors:");
            for (int i = 0; i < offsetLen; i++)
            {
                MelonLogger.Log(ConsoleColor.Yellow, PatreonHelper(BurgerKingContributors, i, rowSize));
            }
        }
        
        public static string[] CoffeeContributors = new string[]
        { };

        /**
         * I don't like coffee but I love the people who give me coffee.
         * I may try to get over the fear of the beans some day.
         *
         * Coffee:
         */
        public static void ThankCoffee()
        {
            if (CoffeeContributors.Length <= 0)
                return;
            
            int rowSize = 5;
            int offsetLen = Mathf.CeilToInt(BurgerKingContributors.Length / (float) rowSize);

            MelonLogger.Log(ConsoleColor.Yellow, "Thank you to the Coffee contributors:");
            for (int i = 0; i < offsetLen; i++)
            {
                MelonLogger.Log(ConsoleColor.Yellow, PatreonHelper(BurgerKingContributors, i, rowSize));
            }
        }

        private static string PatreonHelper(string[] patreons, int index, int rowSize)
        {
            List<string> names = new List<string>();
            
            for (int i = 0; i < rowSize; i++)
            {
                var offset = (index * rowSize) + i;
                if (patreons.Length <= offset)
                    break;
                
                names.Add(patreons[offset]);
            }

            return String.Join(", ", names);
        }
    }
}