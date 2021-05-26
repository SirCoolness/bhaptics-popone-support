using System;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.EffectHelpers;
using BhapticsPopOne.Utils;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne.Haptics.Patterns
{
    public class Shaker
    {
        private enum Direction
        {
            Down = -1,
            Up = 1,
            None = 0
        }
        
        private static Vector3 _lastPos = Vector3.zero;
        private static Direction _direction = Direction.None;

        private static bool ready = false;
        private static LocalProperties _properties = null;

        private static Handedness VelocityHand = Handedness.Unknown;
        private static Vector3 Velocity = Vector3.zero;
        
        public static void Consume(BuffState state)
        {
            if (state != BuffState.Consumed)
                return;
            
            if (DynConfig.Toggles.Vest.ConsumeItem)
                EffectPlayer.Play("Vest/ConsumeItem", new Effect.EffectProperties
                {
                    Strength = ConfigHelpers.EnforceIntensity(ConfigLoader.Config.FoodEatIntensity)
                });
        }

        public static void HandVelocity(Handedness hand, Vector3 velocity)
        {
            if (!ready)
            {
                _properties = GoyfsHelper.Get<LocalProperties>();
                ready = _properties != null;
                
                if (!ready) return;
            }
            
            if (hand != _properties.DominantHand.IVRController.Handedness)
                return;

            VelocityHand = hand;
            Velocity = velocity;
        }
        
        public static void HandleShake(Vector3 lastPos)
        {
            if (!ready)
            {
                _properties = GoyfsHelper.Get<LocalProperties>();
                ready = _properties != null;
                
                if (!ready) return;
            }
            
            MeleeVelocity.Execute(VelocityHand, Velocity, true);

            var diff = lastPos - _lastPos;
            
            if (Mathf.Abs(diff.y) < 0.0125f)
                return;
            
            var direction = (Direction)Mathf.Sign(diff.y);

            if (direction == _direction)
            {
                _lastPos = lastPos;
                return;
            }

            EffectPlayer.Play($"Vest/Shaker_{direction.ToString()}{HapticUtils.HandExt(_properties.DominantHand.IVRController.Handedness)}", new Effect.EffectProperties
            {
                Time = 0.15f,
                Strength = 0.65f
            });
            
            _direction = direction;
            _lastPos = lastPos;
        }

        public static void SceneInit()
        {
            _properties = null;
            ready = false;
        }
    }
}