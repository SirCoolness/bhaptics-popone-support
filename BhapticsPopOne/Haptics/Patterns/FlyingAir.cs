using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class FlyingAir
    {
        public static void Execute()
        {
            var hit = GetFlyingHeight();

            string extension = "";

            if (hit.HasValue || hit.Value.distance > 10f)
                extension = "_Level2";
            else
                extension = "_Level1";
            
            if (!Mod.Instance.Haptics.Player.IsPlaying($"Vest/FlyingAir{extension}"))
            {
                Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/FlyingAir{extension}");
            }
            
            if (!Mod.Instance.Haptics.Player.IsPlaying($"Arm/FlyingAir{extension}"))
            {
                Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/FlyingAir{extension}");
            }
        }

        public static void Clear()
        {
            Mod.Instance.Haptics.Player.TurnOff("Vest/FlyingAir_Level1");
            Mod.Instance.Haptics.Player.TurnOff("Vest/FlyingAir_Level2");
            
            Mod.Instance.Haptics.Player.TurnOff("Arm/FlyingAir_Level1");
            Mod.Instance.Haptics.Player.TurnOff("Arm/FlyingAir_Level2");
        }

        public static RaycastHit? GetFlyingHeight()
        {
            RaycastHit hit;

            var hitItem = Physics.Raycast(Mod.Instance.Data.Players.VestReference().position, -Vector3.up, out hit);

            if (!hitItem)
                return null;
            
            return hit;
        }
    }
}