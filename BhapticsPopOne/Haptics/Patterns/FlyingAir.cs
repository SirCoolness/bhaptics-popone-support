using System;
using System.Runtime.InteropServices;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.ConfigManager.ConfigElements;
using BhapticsPopOne.Haptics.EffectHelpers;
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
            Variants = FrontConfig.Variants
        };
        
        private static ProceduralEffect VestBackEffectManager = new ProceduralEffect
        {
            EffectPrefix = "Vest/FlyingAir_Back",
            Variants = BackConfig.Variants
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

        private static float FallingBaselineTime => Mathf.Max(Config.Modifiers.FromFalling.BaselineTime, 0f);
        private static float FallingProgressMultiplier => Mathf.Max(Config.Modifiers.FromFalling.ProgressMultiplier, 0f);
        private static float HighFlightBaselineTime => Mathf.Max(Config.Modifiers.HighFlight.ProgressMultiplier, 0f);
        private static float HighFlightProgressMultiplier => Mathf.Max(Config.Modifiers.HighFlight.ProgressMultiplier, 0f);
        private static float HighFlightDistance => Mathf.Max(Config.Modifiers.HighFlight.MinDistance, 0f);

        public static void Execute(bool wasFalling)
        {
            #if PLATFORM_ANDROID
            return;
            #endif
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
            if (DynConfig.Toggles.Vest.FlyingWind)
            {
                ExecuteFront(duration);
                ExecuteBack(duration);
            }
            
            ExecuteArms(duration);
            ExecuteHands(duration);
            ExecuteFeet(duration);
        }

        private static void ExecuteFront(float duration)
        {
            // calc progress
            var strengthProgress = Math.Min(duration / (FrontConfig.StrengthTarget * TargetMultiplier), 1f);
            var speedProgress = Math.Min(duration / (FrontConfig.SpeedTarget * TargetMultiplier), 1f);
            var countProgress = Math.Min(duration / (FrontConfig.ConcurrentTarget * TargetMultiplier), 1f);
            
            // calculate effect parameters
            float strength = Mathf.Clamp(Mathf.Lerp(FrontConfig.StrengthLerpStart, FrontConfig.StrengthLerpEnd, strengthProgress) * FrontConfig.StrengthMultiplier, 0f, 1f);
            float speed = Mathf.Lerp(FrontConfig.SpeedLerpStart, FrontConfig.SpeedLerpEnd, speedProgress);
            int count = Mathf.FloorToInt(Mathf.Lerp(1, FrontConfig.MaxEffectCount, countProgress));
            
            VestFrontEffectManager.DispatchEffect(count, (name) =>
            {
                EffectPlayer.Play(
                    name, 
                    new Effect.EffectProperties
                    {
                        Strength = strength,
                        Time = speed
                    }
                );
            });
        }

        private static void ExecuteBack(float duration)
        {
            // calc progress
            var strengthProgress = Math.Min(duration / (BackConfig.StrengthTarget * TargetMultiplier), 1f);
            var speedProgress = Math.Min(duration / (BackConfig.SpeedTarget * TargetMultiplier), 1f);
            var countProgress = Math.Min(duration / (BackConfig.ConcurrentTarget * TargetMultiplier), 1f);
            
            // calculate effect parameters
            float strength = Mathf.Clamp(Mathf.Lerp(BackConfig.StrengthLerpStart, BackConfig.StrengthLerpEnd, strengthProgress) * BackConfig.StrengthMultiplier, 0f, 1f);
            float speed = Mathf.Lerp(BackConfig.SpeedLerpStart, BackConfig.SpeedLerpEnd, speedProgress);
            int count = Mathf.FloorToInt(Mathf.Lerp(1, BackConfig.MaxEffectCount, countProgress));
            
            VestBackEffectManager.DispatchEffect(count, (name) =>
            {
                EffectPlayer.Play(
                    name, 
                    new Effect.EffectProperties
                    {
                        Strength = strength,
                        Time = speed
                    }
                );
            });
        }

        private static void ExecuteArms(float duration)
        {
            if (!DynConfig.Toggles.Arms.FlyingWind)
                return;
            
            string[] effectPool = new[] {"Arm/FlyingAir_Level1", "Arm/FlyingAir_Level2"};
            foreach (var s in effectPool)
            {
                if (EffectPlayer.IsPlaying(s))
                    return;
            }
            
            var progress = Math.Min(duration / (ArmConfig.Target * TargetMultiplier), 1f);
            if (progress >= 1f)
            {
                EffectPlayer.Play(effectPool[1], new Effect.EffectProperties
                {
                    Strength = ArmConfig.Strength
                });
                return;
            }
            
            EffectPlayer.Play(effectPool[0], new Effect.EffectProperties
            {
                Strength = ArmConfig.Strength
            });
        }
        
        private static void ExecuteHands(float duration)
        {
            if (!DynConfig.Toggles.Hands.FlyingWind)
                return;

            var progress = Math.Min(duration / (HandConfig.Target * TargetMultiplier), 1f);
            EffectPlayer.Play("Hand/FlyingAir", new Effect.EffectProperties
            {
                Strength = progress * HandConfig.Strength
            });
        }
        
        private static void ExecuteFeet(float duration)
        {
            if (!DynConfig.Toggles.Feet.FlyingWind)
                return;
            
            var progress = Math.Min(duration / (FeetConfig.Target * TargetMultiplier), 1f);
            EffectPlayer.Play("Foot/FlyingAir", new Effect.EffectProperties
            {
                Strength = progress * FeetConfig.Strength
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
            
            EffectPlayer.Stop("Arm/FlyingAir_Level1");
            EffectPlayer.Stop("Arm/FlyingAir_Level2");
            
            EffectPlayer.Stop("Hand/FlyingAir");
            EffectPlayer.Stop("Foot/FlyingAir");
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

        private class FrontConfig
        {
            public static float StrengthTarget => Mathf.Max(Config.Front.Strength.Target, 0f);
            public static float SpeedTarget => Mathf.Max(Config.Front.Speed.Target, 0f);
            public static float ConcurrentTarget => Mathf.Max(Config.Front.EffectPool.Target, 0f);
            public static float StrengthMultiplier => Mathf.Max(Config.Front.Strength.Multiplier, 0f);
            public static int MaxEffectCount => Mathf.Max(Config.Front.EffectPool.MaxConcurrency, 0);
            public static int Variants => Mathf.Max(Config.Front.EffectPool.Variants, 0);
            public static float StrengthLerpStart => Mathf.Clamp(Config.Front.Strength.Goal.Start, 0f, 1f);
            public static float StrengthLerpEnd => Mathf.Clamp(Config.Front.Strength.Goal.End, 0f, 1f);
            public static float SpeedLerpStart => Mathf.Max(Config.Front.Speed.Goal.Start, 0f);
            public static float SpeedLerpEnd => Mathf.Max(Config.Front.Speed.Goal.End, 0f);
        }
        
        private class BackConfig
        {
            public static float StrengthTarget => Mathf.Max(Config.Back.Strength.Target, 0f);
            public static float SpeedTarget => Mathf.Max(Config.Back.Speed.Target, 0f);
            public static float ConcurrentTarget => Mathf.Max(Config.Back.EffectPool.Target, 0f);
            public static float StrengthMultiplier => Mathf.Max(Config.Back.Strength.Multiplier, 0f);
            public static int MaxEffectCount => Mathf.Max(Config.Back.EffectPool.MaxConcurrency, 0);
            public static int Variants => Mathf.Max(Config.Back.EffectPool.Variants, 0);
            public static float StrengthLerpStart => Mathf.Clamp(Config.Back.Strength.Goal.Start, 0f, 1f);
            public static float StrengthLerpEnd => Mathf.Clamp(Config.Back.Strength.Goal.End, 0f, 1f);
            public static float SpeedLerpStart => Mathf.Max(Config.Back.Speed.Goal.Start, 0f);
            public static float SpeedLerpEnd => Mathf.Max(Config.Back.Speed.Goal.End, 0f);
        }

        private class ArmConfig
        {
            public static float Strength => Mathf.Clamp(Config.Arms.Strength, 0f, 1f);
            public static float Target => Mathf.Max(Config.Arms.Target, 0f);
        }
        
        private class HandConfig
        {
            public static float Strength => Mathf.Clamp(Config.Hands.Strength, 0f, 1f);
            public static float Target => Mathf.Max(Config.Hands.Target, 0f);
        }
        
        private class FeetConfig
        {
            public static float Strength => Mathf.Clamp(Config.Feet.Strength, 0f, 1f);
            public static float Target => Mathf.Max(Config.Feet.Target, 0f);
        }
    }
}