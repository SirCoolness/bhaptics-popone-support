using System;
using System.Runtime.InteropServices;
using Bhaptics.Tact;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.ConfigManager.ConfigElements;
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
        private static bool HighFlightTriggered = false;

        private static ProceduralEffect VestFrontEffectManager = new ProceduralEffect
        {
            EffectPrefix = "Vest/FlyingAir_Front",
            Variants = Variants
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

        private static FlyEffects Config => ConfigLoader.Config.Effects.Flying;
        private static float StrengthTarget => Mathf.Max(Config.Front.Strength.Target, 0f);
        private static float SpeedTarget => Mathf.Max(Config.Front.Speed.Target, 0f);
        private static float ConcurrentTarget => Mathf.Max(Config.Front.EffectPool.Target, 0f);
        private static float StrengthMultiplier => Mathf.Max(Config.Front.Strength.Multiplier, 0f);
        private static int MaxEffectCount => Mathf.Max(Config.Front.EffectPool.MaxConcurrency, 0);
        private static int Variants => Mathf.Max(Config.Front.EffectPool.Variants, 0);
        private static float FallingBaselineTime => Mathf.Max(Config.Modifiers.FromFalling.BaselineTime, 0f);
        private static float FallingProgressMultiplier => Mathf.Max(Config.Modifiers.FromFalling.ProgressMultiplier, 0f);
        private static float HighFlightBaselineTime => Mathf.Max(Config.Modifiers.HighFlight.ProgressMultiplier, 0f);
        private static float HighFlightProgressMultiplier => Mathf.Max(Config.Modifiers.HighFlight.ProgressMultiplier, 0f);
        private static float HighFlightDistance => Mathf.Max(Config.Modifiers.HighFlight.MinDistance, 0f);
        private static float StrengthLerpStart => Mathf.Clamp(Config.Front.Strength.Goal.Start, 0f, 1f);
        private static float StrengthLerpEnd => Mathf.Clamp(Config.Front.Strength.Goal.End, 0f, 1f);
        private static float SpeedLerpStart => Mathf.Max(Config.Front.Speed.Goal.Start, 0f);
        private static float SpeedLerpEnd => Mathf.Max(Config.Front.Speed.Goal.End, 0f);

        private static bool MathValidation()
        {
            return true;
        }
        
        public static void Execute(bool wasFalling)
        {
            if (!MathValidation())
                return;

            if (ResetFlight)
            {
                ResetFlight = false;
                FlightStart = DateTime.Now;

                if (wasFalling)
                {
                    UpdateBaselines(FallingBaselineTime, FallingProgressMultiplier);
                }
            }

            var internalDuration = FlightDuration + BaselineTime;
            
            if (internalDuration < 0.1f && !HighFlightTriggered && IsHighFlight())
            {
                UpdateBaselines(HighFlightBaselineTime, HighFlightProgressMultiplier);
                HighFlightTriggered = true;
            }
            
            var duration = internalDuration + BaselineTime;
            
            // calc progress
            var strengthProgress = Math.Min(duration / (StrengthTarget * TargetMultiplier), 1f);
            var speedProgress = Math.Min(duration / (SpeedTarget * TargetMultiplier), 1f);
            var countProgress = Math.Min(duration / (ConcurrentTarget * TargetMultiplier), 1f);
            
            // calculate effect parameters
            float strength = Mathf.Clamp(Mathf.Lerp(StrengthLerpStart, StrengthLerpEnd, strengthProgress) * StrengthMultiplier, 0f, 1f);
            float speed = Mathf.Lerp(SpeedLerpStart, SpeedLerpEnd, speedProgress);
            int count = Mathf.FloorToInt(Mathf.Lerp(1, MaxEffectCount, countProgress));
            
            VestFrontEffectManager.DispatchEffect(count, (name) =>
            {
                // MelonLogger.Log(name);
                Mod.Instance.Haptics.Player.SubmitRegistered(
                    name, 
                    new ScaleOption(strength, speed)
                );
            });
        }
        
        public static void Clear()
        {
            ResetFlight = true;
            HighFlightTriggered = false;
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