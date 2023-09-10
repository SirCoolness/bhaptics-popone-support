using System;
using System.Collections.Generic;
using BhapticsPopOne.MonoBehaviours;
using MelonLoader;
using UnityEngine;
using Il2Cpp;

namespace BhapticsPopOne.Data
{
    public class HandHelper
    {
        public Handedness LastHand { get; set; }
        
        public Dictionary<uint, List<Handedness>> brokenDestructibles;

        public HandHelper()
        {
            LastHand = Handedness.Unknown;
            brokenDestructibles = new Dictionary<uint, List<Handedness>>();
        }
    }
}