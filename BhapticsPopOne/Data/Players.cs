using System.Collections.Generic;
using Goyfs.Signal;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne.Data
{
    public class Players
    {
        private GameObject _gameRoot;

        public BattleContextView _battleContext;
        
        private PlayerContainer _localPlayerContainer;
        public PlayerContainer LocalPlayerContainer
        {
            get
            {
                if (_localPlayerContainer != null && _localPlayerContainer.isLocalPlayer)
                    return _localPlayerContainer;

                foreach (var gameManagerAllContainer in PlayerContainers)
                {
                    if (gameManagerAllContainer.isLocalPlayer)
                    {
                        _localPlayerContainer = gameManagerAllContainer;
                        return _localPlayerContainer;
                    }
                }

                // PlayerBuffConsumedSignal buffConsumed;
                // BattleRoyaleExtensions.TryGet<PlayerBuffConsumedSignal>(Goyfs.Instance.Instance, out buffConsumed);
                return null;
            }
        }

        public List<PlayerContainer> PlayerContainers =>
            new List<PlayerContainer>(GameObject.FindObjectsOfType<PlayerContainer>());
        
        public void Initialize()
        {
            _gameRoot = GameObject.Find("SceneFunction/SceneRoot");
            if (_gameRoot == null)
            {
                MelonLogger.LogError("Failed to find SceneFunction/SceneRoot");
                return;
            }
            
            _battleContext = _gameRoot.GetComponent<BattleContextView>();
            if (_battleContext == null)
            {
                MelonLogger.LogError("Failed to load battle context.");
                return;
            }

            _localPlayerContainer = null;
        }

        public PlayerContainer FindCollisionPlayerContainer(Collider collision)
        {
            var colliderRoot = collision.transform.root.gameObject;
            
            if (colliderRoot == null)
            {
                // MelonLogger.Log("[HIT] Cant find collider root object");
                return null;
            }
            
            var skeleton = colliderRoot.GetComponent<DamageableSkeleton>();
            if (skeleton == null)
            {
                // MelonLogger.Log($"[HIT] NO SKELETON FOUND {colliderRoot.name}");
                return null;
            }
            
            PlayerContainer player = null;
            foreach (var modPlayerContainer in PlayerContainers)
            {
                if (modPlayerContainer.Avatar == null || modPlayerContainer.Avatar.Rig == null)
                    continue;

                if (modPlayerContainer.Avatar.Rig.gameObject == colliderRoot)
                {
                    player = modPlayerContainer;
                    break;
                }
            }

            return player;
        }

        public PlayerContainer FindPlayerByNetworkID(uint networkId)
        {
            foreach (var playerContainer in PlayerContainers)
            {
                if (playerContainer.netIdentity.netId == networkId)
                {
                    return playerContainer;
                }
            }
            
            return null;
        }

        public Transform VestReference()
        {
            var trans = Mod.Instance.Data.Players.LocalPlayerContainer?.Avatar?.Rig?.transform;
            if (trans == null)
            {
                MelonLogger.LogError("Can not find player avatar");
                return null;
            }

            var result = BattleRoyaleExtensions.FindRecursivelyRegex(trans, @".*:spine_02.*",
                new Il2CppSystem.Text.RegularExpressions.RegexOptions());

            return result;
        }
        
        public Transform DebugHandReference()
        {
            var trans = Mod.Instance.Data.Players.LocalPlayerContainer?.Avatar?.Rig?.transform;
            if (trans == null)
            {"Can not find player avatar""cant find transform");
                return null;
            }

            var result = BattleRoyaleExtensions.FindRecursivelyRegex(trans, @".*:hand_r.*",
                new Il2CppSystem.Text.RegularExpressions.RegexOptions());

            return result;
        }
    }
}