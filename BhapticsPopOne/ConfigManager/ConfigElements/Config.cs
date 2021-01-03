﻿using System;
using System.Reflection;
using YamlDotNet.Serialization;

namespace BhapticsPopOne.ConfigManager.ConfigElements
{
    public class Config
    {
        public string Version { get; set; }
        public float VestRecoil { get; set; }
        public float OffhandRecoilStrength { get; set; }
        public bool VestClimbEffects { get; set; }
        public bool Clapping { get; set; }
        public bool HighFive { get; set; }
        public bool ShowHighFiveRegion
        {
            get;
            set;
        }
        public bool ShowLoadedEffects { get; set; }

        [YamlIgnore] 
        public static Version CurrentVersion => new Version("0.0.2");
        
        [YamlIgnore] 
        public static Config DefaultConfig = new Config
        {
            Version = CurrentVersion.ToString(),
            VestRecoil = 1f,
            OffhandRecoilStrength = 0.7f,
            VestClimbEffects = true,
            Clapping = false,
            HighFive = true,
            ShowHighFiveRegion = false,
            ShowLoadedEffects = false
        };
    }
}