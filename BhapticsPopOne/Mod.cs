using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics;
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
            base.OnApplicationStart();

            RootInit();
            
            ConfigLoader.InitConfig();
            
            Patreon.Run(); // (●'◡'●)
            
            Validation();
            
            MonoBehavioursLoader.Inject();
            
            InitializeManagers();
            
            StartServices();
            
            PatternManager.LoadPatterns();
            
            Data.Initialize();
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
        
        private void Validation()
        {
            var validationLogContext = new LoggingContext("validation", _initLoggingContext);
            
            var patchTester = new TestPatches();
            var failedMethods = new List<MethodInfo>();

            var status = patchTester.Test(failedMethods);
            
            if (status) {
                MelonLogger.Log($"{validationLogContext.Prefix} Methods have been patched successfully");
            }
            else
            {
                string failedMethodsMessage = String.Join(", ", failedMethods.Select(x =>
                {
                    var delaringClass = x.DeclaringType;
                    if (delaringClass == null) return x.Name;
                    return $"{delaringClass.Name}.{x.Name}";
                }));
                
                MelonLogger.Log($"{validationLogContext.Prefix} Some methods failed to be patched. Exiting early. ({failedMethods.Count})[{failedMethodsMessage}]");
                Application.Quit();
            }
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
            base.OnFixedUpdate();
            
            _effectLoop.FixedUpdate();
        }

        public override void OnLevelWasLoaded(int level)
        {
            base.OnLevelWasLoaded(level);
            
            // clear when level reloads to avoid memory overflow
            ReloadWeapon.PreviousStateMap.Clear();
        }
    }
}