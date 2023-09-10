using System;
using System.Collections.Generic;
using System.Linq;
using MelonLoader;
using UnityEngine;

namespace SirCoolness
{
    /**
     * This class is a thank you to all the Patreon members
     */
    public class Patreon
    {
        public static ConsoleColor color = ConsoleColor.Yellow;
        public static string[] ExistContributors = new string[]
        {
            "Jennifer Lee @ bHaptics",
            "RamBot Tech"
        };
        
        /**
         * People exist. Some people are just extremely generous ♥.
         */
        public static void ThankExist()
        {
            MelonLogger.Msg(color, "People exist. Some people are just extremely generous <3.");
            MelonLogger.Msg(color, "");
            MelonLogger.Msg(color, $"Thank you to the {RainbowifyConsoleColor("Exist")} contributors:");

            PatreonHelper(ExistContributors.Select((name) => $"{RainbowifyConsoleColor(name)} \x1b[8m[{name}]\x1b[28m").ToArray(), 1);
        }
        
        public static string[] BurgerKingContributors = new string[]
        {
            "AceJas",
            "ZstormGames",
            "NerdNational",
            "Anonymous Supporter",
            "TheMysticle",
            "Rasta",
            "bsharp",
            "Sky \x1b[2mC\x1b[22mandy", // Sky Candy
            "Wesley5n1p35",
            "Brandon Hughey",
            "MV7945",
            "halfwaymexican",
            "Jesse Forbs",
            "Chris Taylor"
        };

        /**
         * Thank you to the Burger King tier contributors.
         * Much Burger King will be consumed in your name.
         */
        public static void ThankBurgerKing()
        {
            MelonLogger.Msg(color, "Thank you to the Burger King tier contributors.");
            MelonLogger.Msg(color, "Much Burger King will be consumed in your name.");
            MelonLogger.Msg(color, "");
            MelonLogger.Msg(color, "Burger King Tier:");

            PatreonHelper(BurgerKingContributors, 3);
        }
        
        public static string[] CoffeeContributors = new string[]
        {
            "Alex VR",
            "Ham VR",
            "Delistd",
            "Mike.",
            "Maker124",
            "Pally's Crew",
            "AprilPvd",
            "Stan Beener",
            "Gordon",
            "Whodat",
            "Paul Eckhoff",
            "G-Tricks",
            "Dallon Duke",
            "Florian Farhrenberger",
            "Curtis Pedersen",
            "Jo Swannymcswanerson",
            "Niclas Klinterhall", // Niclas Klinterhäll
            "Ludovit Kopcsanyi",
            "evilgeniusmojo",
            "Hunter Caserta",
            "Phenoram",
            "Polydor-FR",
            "VRican VRothers",
            "Benjamin Dowswell",
            "Alan Azzopardi",
            "Othello",
            "ThatFamilyGuy",
            "Allusive Experience"
        };

        /**
         * I don't like coffee but I love the people who give me coffee.
         * I may try to get over the fear of the beans some day.
         */
        public static void ThankCoffee()
        {
            MelonLogger.Msg(color, "I don't like coffee but I love the people who give me coffee.");
            MelonLogger.Msg(color, "I may try to get over the fear of the beans some day.");
            MelonLogger.Msg(color, "");
            MelonLogger.Msg(color, "Thank you to the Coffee contributors:");

            PatreonHelper(CoffeeContributors, 5);
        }
        
        /**
         * (☞ﾟヮﾟ)☞☜(ﾟヮﾟ☜)
         */
        public static void Promote()
        {
            MelonLogger.Msg(color, "Last Updated: 9/9/2023 - If you don't see your name here, you will be added in the next update to the mod.");
            MelonLogger.Msg(color, "If I forgot to add you or you would like your name changed. Please message me.");
            MelonLogger.Msg(color, "");
            MelonLogger.Msg(color, "If you would like to support future/ongoing projects and get your name added here.");
            MelonLogger.Msg(color, "Check out: https://www.patreon.com/SirCoolness");
        }

        /**
         * Thank you
         */
        public static void Run()
        {
            var LogItems = new List<Action>();
            
            if (ExistContributors.Length > 0)
                LogItems.Add(ThankExist);

            if (BurgerKingContributors.Length > 0)
                LogItems.Add(ThankBurgerKing);

            if (CoffeeContributors.Length > 0)
                LogItems.Add(ThankCoffee);
            
            LogItems.Add(Promote);

            var line = "\x1b[38;5;8m----------------------------------------------\x1b[0m";
            
            foreach (var logItem in LogItems)
            {
                MelonLogger.Msg(color, line);
                MelonLogger.Msg(color, "");
                
                logItem.Invoke();
                
                MelonLogger.Msg(color, "");
            }
            
            MelonLogger.Msg(color, line);
        }

        private static void PatreonHelper(string[] patreons, int rowSize)
        {
            var rowCount = Mathf.CeilToInt(patreons.Length / (float) rowSize);

            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                List<string> names = new List<string>();
            
                for (int i = 0; i < rowSize; i++)
                {
                    var offset = (rowIndex * rowSize) + i;
                    if (patreons.Length <= offset)
                        break;
                
                    names.Add(patreons[offset]);
                }
                
                var output = String.Join("\x1b[38;5;11m,\x1b[39m ", names);
                
                MelonLogger.Msg(ConsoleColor.White, output);
            }
            
            
        }

        private static string RainbowifyConsoleColor(string message)
        {
            var rainbowSequence = new int[] { 196, 203, 215, 184, 46, 37, 39, 147, 219, 203 };

            var outMessage = "";

            var index = 0;
            foreach (var c in message)
            {
                outMessage += $"\x1b[1m\x1b[5m\x1b[38;5;{rainbowSequence[index % rainbowSequence.Length]}m{c}";
                index++;
            }

            outMessage += "\x1b[0m";

            return outMessage;
        }
    }
}