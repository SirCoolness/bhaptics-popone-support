using System;
using System.Runtime.InteropServices;
using Bhaptics.Tact;
using BhapticsPopOne.Haptics.EffectManagers;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class FlyingAir
    {
        private static DateTime FlightStart;
        private static float BaselineTime = 0f;
        private static float TargetMultiplier = 1f;
        private static bool ResetFlight = true;

        private static ProceduralEffect VestFrontEffectManager = new ProceduralEffect
        {
            EffectPrefix = "Vest/FlyingAir_Front",
            Variants = 8
        };
        
        private static ProceduralEffect VestBackEffectManager = new ProceduralEffect
        {
            EffectPrefix = "Vest/FallingAir",
            Variants = 10
        };
        
        private static float FlightDuration
        {
            get
            {
                TimeSpan ts = DateTime.Now - FlightStart;
                return (float)ts.TotalSeconds;
            }
        }

        private static float StrengthTarget = 0.75f;
        private static float SpeedTarget = 0.75f;
        private static float ConcurrentTarget = 2f;
        private static float StrengthMultiplier = 0.4f;
        private static float MaxEffectCount = 4;
        private static float FallingBaselineTime = 0.2f;
        private static float FallingProgressMultiplier = 0.7f;
        private static float HighFlightBaselineTime = 0.3f;
        private static float HighFlightProgressMultiplier = 0.5f;
        private static float HighFlightDistance = 25f;
        
        public static void Execute(bool wasFalling)
        {
            if (ResetFlight)
            {
                ResetFlight = false;
                FlightStart = DateTime.Now;

                if (IsHighFlight())
                {
                    MelonLogger.Log(ConsoleColor.Magenta, "High Flight");
                    UpdateBaselines(HighFlightBaselineTime, HighFlightProgressMultiplier);
                }
                
                if (wasFalling)
                {
                    MelonLogger.Log(ConsoleColor.Magenta, "Was Falling");
                    UpdateBaselines(FallingBaselineTime, FallingProgressMultiplier);
                }
            }

            var duration = FlightDuration + BaselineTime;
            
            // calc progress
            var strengthProgress = Math.Min(duration / (StrengthTarget * TargetMultiplier), 1f);
            var speedProgress = Math.Min(duration / (SpeedTarget * TargetMultiplier), 1f);
            var countProgress = Math.Min(duration / (ConcurrentTarget * TargetMultiplier), 1f);
            
            // calculate effect parameters
            float strength = Mathf.Clamp(Mathf.Lerp(0.3f, 1f, strengthProgress) * StrengthMultiplier, 0f, 1f);
            float speed = Mathf.Lerp(1f, 0.3f, speedProgress);
            int count = Mathf.FloorToInt(Mathf.Lerp(1, MaxEffectCount, countProgress));
            
            VestFrontEffectManager.DispatchEffect(count, (name) =>
            {
                // MelonLogger.Log(name);
                Mod.Instance.Haptics.Player.SubmitRegistered(
                    name, 
                    new ScaleOption(strength, speed)
                );
            });
            // var hit = GetFlyingHeight();
            //
            // string extension = "";
            //
            // if (hit.HasValue && hit.Value.distance > 10f)
            //     extension = "_Level2";
            // else
            //     extension = "_Level1";
            //
            // if (!Mod.Instance.Haptics.Player.IsPlaying($"Vest/FlyingAir{extension}"))
            // {
            //     Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/FlyingAir{extension}");
            // }
            //
            // if (!Mod.Instance.Haptics.Player.IsPlaying($"Arm/FlyingAir{extension}"))
            // {
            //     Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/FlyingAir{extension}");
            // }
        }
        
        public static void Clear()
        {
            ResetFlight = true;
            BaselineTime = 0f;
            TargetMultiplier = 1f;
            
            VestFrontEffectManager.Clear();
            VestBackEffectManager.Clear();
            
            // Mod.Instance.Haptics.Player.TurnOff("Arm/FlyingAir_Level1");
            // Mod.Instance.Haptics.Player.TurnOff("Arm/FlyingAir_Level2");
        }

        public static bool IsHighFlight()
        {
            var hit = GetFlyingHeight();

            return hit.HasValue && hit.Value.distance >= HighFlightDistance;
        }

        public static RaycastHit? GetFlyingHeight()
        {
            RaycastHit hit;

            var hitItem = Physics.Raycast(Mod.Instance.Data.Players.VestReference().position, -Vector3.up, out hit);

            if (!hitItem)
                return null;
            
            return hit;
        }

        private static void UpdateBaselines(float baselineTime, float targetMultiplier)
        {
            BaselineTime = Mathf.Max(BaselineTime, baselineTime);
            TargetMultiplier = Mathf.Min(TargetMultiplier, targetMultiplier);
        }
    }
}