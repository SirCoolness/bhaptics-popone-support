using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.EffectManagers;
using BhapticsPopOne.Haptics.Patterns;
using BhapticsPopOne.Patches;
using MelonLoader;
using UnityEngine;
using String = System.String;

namespace BhapticsPopOne
{
    public class Mod : MelonMod
    {
        private static Mod instance = null;
        private static readonly object padlock = new object();
        
        public static Mod Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Mod();
                    }
                    return instance;
                }
            }
        }

        private LoggingContext _initLoggingContext;
        private LoggingContext _disposeLoggingContext;
        private EffectLoop _effectLoop;

        public Data.Data Data;
        public ConnectionManager Haptics;

        public bool Disabled { get; private set; } = false;

        public Mod()
        {
            _initLoggingContext = new LoggingContext("init");
            _disposeLoggingContext = new LoggingContext("exit");
        }
        
        /**
         * The first call made on application start
         */
        private void RootInit()
        {
            if (instance == null)
            {
                instance = this;
            }
            
            Data = new Data.Data();
            _effectLoop = new EffectLoop();
        }

        
        public override void OnApplicationStart()
        {
            MelonLogger.Log($"{_initLoggingContext.Prefix} Application initializing");

            RootInit();
            
            FileHelpers.EnforceDirectory();
            ConfigLoader.InitConfig();
            
            Patreon.Run(); // (●'◡'●)
            
            MonoBehavioursLoader.Inject();
            
            InitializeManagers();
            
            StartServices();
            
            PatternManager.LoadPatterns();
            EffectEventsDispatcher.Init();
            
            Data.Initialize();
            
            Physics.IgnoreLayerCollision(10, 19, false);
            
            MelonLogger.Log("Successfully started");
        }

        public override void OnApplicationQuit()
        {
            MelonLogger.Log($"{_disposeLoggingContext.Prefix} Application closing");

            base.OnApplicationQuit();
            
            StopServices();
        }

        private void InitializeManagers()
        {
            Haptics = new ConnectionManager();
        }

        private void StartServices()
        {
            MelonLogger.Log($"{_initLoggingContext.Prefix} Starting Services");
            
            Haptics.Start();
        }

        private void StopServices()
        {
            MelonLogger.Log($"{_disposeLoggingContext.Prefix} Stopping Services");

            Haptics.Stop();
        }
        
        public override void OnFixedUpdate()
        {
            if (Disabled)
                return;
            
            _effectLoop.FixedUpdate();
            KatanaShield.FixedUpdate();
            ZoneDamage.OnFixedUpdate();
            // TestOcilate.FixedUpdate();
        }

        public override void OnUpdate()
        {
            EffectLoopRegistry.Update();
        }

        public override void OnLevelWasInitialized(int level)
        {
            EffectLoopRegistry.LevelInit();
            DrinkSoda.Clear();
            KatanaShield.Execute(false);
            ZoneDamage.Clear();
            BhapticsPopOne.Haptics.Patterns.MeleeVelocity.Reset();
            // MelonLogger.Log("init level");
            
            Data.Players.Reset();
        }

        public void Disable()
        {
            if (Disabled)
                return;
            
            harmonyInstance.UnpatchAll();
            
            Disabled = true;
        }
    }
}