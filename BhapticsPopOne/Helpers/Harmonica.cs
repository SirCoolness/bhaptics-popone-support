using System.Reflection;
using BhapticsPopOne.Haptics.Patterns;
using BhapticsPopOne.Utils;
using BigBoxVR;
using BigBoxVR.BattleRoyale.Models.Shared;
using Il2CppSystem;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace BhapticsPopOne.Helpers
{
    public class Harmonica
    {
        private static bool ready = false;
        private static LocalProperties _properties = null;
        private static SquadManager _squadManager = null;
        private static bool IsPlaying = false;
        private static DateTime HealTick = DateTime.Now;
        
        public static void OnPlayerWasHit(uint netId, DamageableHitInfo info)
        {
            if (!PlayerContainer.TryFind(netId, out PlayerContainer container))
                return;
            
            if (!container.isLocalPlayer)
                return;
            
            
        }

        public static void OnBuffStateChangedSignal(InventorySlot.BuffRecord record, BuffState state)
        {
            var slot = Mod.Instance.Data.Players.LocalPlayerContainer.Inventory.EquippedSlot;
            if (slot.EquippedType != InventorySlotType.Buff)
                return;

            var info = slot.GetInfo<BuffInfo>();
            
            if (info.Type != InventoryItemType.BuffHarmonica)
                return;

            switch (state)
            {
                case BuffState.State1:
                    StartPlaying();
                    break;
                case BuffState.Initial:
                    StopPlaying();
                    break;
            }
        }

        public static void OnPlayerBuffConsumed(uint netId, int increment, BuffInfo info)
        {
            var player = Mod.Instance.Data.Players.LocalPlayerContainer;
            if (player.netId == netId)
            {
                
            }
            else
            {
                if (info.Type != InventoryItemType.BuffHarmonica)
                    return;

                var now = DateTime.Now;
            
                if (now < HealTick)
                    return;

                if (!player.Buff.playersHealing.Exists(ConvertPredicate<PlayerContainer>(container => container.netId == netId)))
                    return;
            
                MeleeDamageableHit.Play(player.Data.DominantHand, 1f, 0.3f);

                HealTick = now.AddSeconds(0.8f);
            }
        }

        private static Predicate<T> ConvertPredicate<T>(System.Predicate<T> predicate)
        {
            return DelegateSupport.ConvertDelegate<Il2CppSystem.Predicate<T>>(predicate);
        }
        
        static void StartPlaying()
        {
            if (IsPlaying)
                return;

            IsPlaying = true;
        }

        static void StopPlaying()
        {
            if (!IsPlaying)
                return;

            IsPlaying = false;
        }

        public static void OnLevelInit()
        {
            StopPlaying();

            ready = false;
            _properties = null;
        }
        
        public static void HandVelocity(Handedness hand, Vector3 velocity)
        {
            if (!ready)
            {
                _properties = GoyfsHelper.Get<LocalProperties>();
                _squadManager = GoyfsHelper.Get<SquadManager>();
                ready = _properties != null;
                
                if (!ready) return;
            }
            
            if (!IsPlaying)
                return;
            
            if (hand != _properties.DominantHand.IVRController.Handedness)
                return;
            
            Haptics.Patterns.MeleeVelocity.Execute(hand, velocity, true);
        }
    }
}