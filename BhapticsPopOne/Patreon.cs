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
        public static ConsoleColor color = ConsoleColor.Yellow;
        public static string[] ExistContributors = new string[]
        {
            "Jennifer Lee @ bHaptics"
        };

        /**
         * People exist. Some people are just extremely generous ♥.
         *
         * Exist:
         * - Jennifer Lee @ bHaptics
         */
        public static void ThankExist()
        {
            if (ExistContributors.Length <= 0)
                return;
            
            int rowSize = 1;
            int offsetLen = Mathf.CeilToInt(ExistContributors.Length / (float) rowSize);

            MelonLogger.Log(color, "Thank you to the Exist contributors:");
            for (int i = 0; i < offsetLen; i++)
            {
                MelonLogger.Log(color, PatreonHelper(ExistContributors, i, rowSize));
            }
        }
        
        public static string[] BurgerKingContributors = new string[]
        {
            "AceJas",
            "ZstormGames",
            "NerdNational"
        };

        /**
         * Thank you to the Burger King tier contributors.
         * Much Burger King will be consumed in your name.
         *
         * Burger King:
         * - AceJas
         * - ZstormGames
         * - NerdNational
         */
        public static void ThankBurgerKing()
        {
            if (BurgerKingContributors.Length <= 0)
                return;

            int rowSize = 3;
            int offsetLen = Mathf.CeilToInt(BurgerKingContributors.Length / (float) rowSize);

            MelonLogger.Log(color, "Thank you to the Burger King contributors:");
            for (int i = 0; i < offsetLen; i++)
            {
                MelonLogger.Log(color, PatreonHelper(BurgerKingContributors, i, rowSize));
            }
        }
        
        public static string[] CoffeeContributors = new string[]
        {
            "Alex VR"
        };

        /**
         * I don't like coffee but I love the people who give me coffee.
         * I may try to get over the fear of the beans some day.
         *
         * Coffee:
         * - Alex VR
         */
        public static void ThankCoffee()
        {
            if (CoffeeContributors.Length <= 0)
                return;
            
            int rowSize = 5;
            int offsetLen = Mathf.CeilToInt(CoffeeContributors.Length / (float) rowSize);

            MelonLogger.Log(color, "Thank you to the Coffee contributors:");
            for (int i = 0; i < offsetLen; i++)
            {
                MelonLogger.Log(color, PatreonHelper(CoffeeContributors, i, rowSize));
            }
        }
        
        /**
         * (☞ﾟヮﾟ)☞☜(ﾟヮﾟ☜)
         */
        public static void Promote()
        {
            MelonLogger.Log(color, "If you would like to support future/ongoing projects and get your name added here.");
            MelonLogger.Log(color, "Check out: https://www.patreon.com/SirCoolness");
        }

        /**
         * Thank you
         */
        public static void Run()
        {
            ThankExist();
            if (ExistContributors.Length > 0)
                MelonLogger.Log(color, "");
            ThankBurgerKing();
            if (BurgerKingContributors.Length > 0)
                MelonLogger.Log(color, "");
            ThankCoffee();
            if (CoffeeContributors.Length > 0)
                MelonLogger.Log(color, "");
            Promote();
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