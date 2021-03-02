using System;
using System.Collections.Generic;
using BhapticsPopOne.Utils;
using Goyfs.Signal;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne.Data
{
    public class Players
    {
        private bool _foundLocalProperties = false;
        private LocalProperties _localProperties;
        public PlayerContainer LocalPlayerContainer
        {
            get
            {
                if (!_foundLocalProperties)
                {
                    _foundLocalProperties = GoyfsHelper.TryGet(out _localProperties);
                    
                    if (!_foundLocalProperties)
                        return null;
                }

                return _localProperties.Container;
            }
        }

        public HandHelper LocalHandHelper;

        public Il2CppSystem.Collections.Generic.List<PlayerContainer> PlayerContainers => GoyfsHelper.Get<Il2CppSystem.Collections.Generic.List<PlayerContainer>>();
        
        public void Initialize()
        {
            LocalHandHelper = new HandHelper();
        }

        public void Reset()
        {
            _foundLocalProperties = false;
        }
        
        public Transform VestReference()
        {
            var trans = LocalPlayerContainer?.Avatar?.Rig?.transform;
            if (trans == null)
            {
                MelonLogger.LogError("Can not find player avatar");
                return null;
            }

            var result = BattleRoyaleExtensions.FindRecursivelyRegex(trans, @".*:spine_02.*",
                new Il2CppSystem.Text.RegularExpressions.RegexOptions());

            return result;
        }
    }
}