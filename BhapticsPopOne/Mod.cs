using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.EffectManagers;
using BhapticsPopOne.Haptics.Patterns;
using BhapticsPopOne.Helpers;
using MelonLoader;
using UnityEngine;
using SceneConfig = BhapticsPopOne.ConfigManager.ConfigElements.SceneConfig;
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
            MelonLogger.Msg($"{_initLoggingContext.Prefix} Application initializing");

            RootInit();
            
            FileHelpers.EnforceDirectory();
            ConfigLoader.InitConfig();

            DynConfig.UpdateConfig(DynConfig.SceneMode.General);
            
            SirCoolness.Patreon.Run(); // (●'◡'●)
            
            MonoBehavioursLoader.Inject();

            InitializeManagers();
            
            StartServices();
            
            PatternManager.LoadPatterns();

            EffectEventsDispatcher.Init();

            Data.Initialize();

            Physics.IgnoreLayerCollision(10, 19, false);
            
            MelonLogger.Msg("Successfully started");
        }

        public override void OnApplicationQuit()
        {
            MelonLogger.Msg($"{_disposeLoggingContext.Prefix} Application closing");

            base.OnApplicationQuit();
            
            StopServices();
        }

        private void InitializeManagers()
        {
            Haptics = new ConnectionManager();
        }

        private void StartServices()
        {
            MelonLogger.Msg($"{_initLoggingContext.Prefix} Starting Services");
            
            Haptics.Start();
        }

        private void StopServices()
        {
            MelonLogger.Msg($"{_disposeLoggingContext.Prefix} Stopping Services");

            Haptics.Stop();
        }

        public override void OnFixedUpdate()
        {
            if (Disabled)
                return;
            
            EffectEventsDispatcher.FixedUpdate();
            _effectLoop.FixedUpdate();
            KatanaShield.FixedUpdate();
            ZoneDamage.OnFixedUpdate();
            // TestOcilate.FixedUpdate();
        }

        public override void OnUpdate()
        {
            EffectLoopRegistry.Update();
        }
        
        public override void OnSceneWasInitialized(int level, string name)
        {
            EffectLoopRegistry.LevelInit();
            DrinkSoda.Clear();
            KatanaShield.Execute(false);
            ZoneDamage.Clear();
            BhapticsPopOne.Haptics.Patterns.MeleeVelocity.Reset();
            Shaker.SceneInit();
            Harmonica.OnLevelInit();
            // MelonLogger.Log("init level");
            
            Data.Players.Reset();
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (buildIndex == 0)
                return;
        }
        
        public void Disable()
        {
            if (Disabled)
                return;
            
            HarmonyInstance.UnpatchSelf();
            
            Disabled = true;
        }
    }
}