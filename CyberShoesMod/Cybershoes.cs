using System;
using System.Reflection;
using Il2CppSystem.Collections.Generic;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;
using UniverseLib;
using UniverseLib.Input;

namespace CyberShoes
{
    public class CybershoesMod : MelonMod
    {
        // public static Type TGamepad => t_Gamepad ??= ReflectionUtility.GetTypeByName("UnityEngine.InputSystem.Gamepad");
        // static Type t_Gamepad;
        
        // static object CurrentGamepad => p_gpCurrent.GetValue(null, null);
        // static PropertyInfo p_gpCurrent;
        
        public override void OnApplicationStart()
        {
            UniverseLib.Config.UniverseLibConfig config = new()
            {
                Disable_EventSystem_Override = false, // or null
                Force_Unlock_Mouse = true, // or null
                Unhollowed_Modules_Folder = System.IO.Path.Combine("MelonLoader", "Managed") // or null
            };
            
            ClassInjector.RegisterTypeInIl2Cpp<CybershoesManager>();
            UniverseLib.Universe.Init(1f, OnInitialized, LogHandler, config);
        }
        
        void OnInitialized() 
        {
            MelonLogger.Msg(InputManager.CurrentType);
            
            MelonLogger.Msg(InputManager.MousePosition.ToString());
            
            foreach (var joystickName in UnityEngine.Input.GetJoystickNames())
            {
                MelonLogger.Msg("joystick " + joystickName);
            }
            
            MelonLogger.Msg("finished joysticks");
        }

        void LogHandler(string message, LogType type) 
        {
            switch (type)
            {
                case LogType.Assert:
                case LogType.Error:
                case LogType.Exception:
                    MelonLogger.Error(message);
                    break;
                case LogType.Warning:
                    MelonLogger.Warning(message);
                    break;
                case LogType.Log:
                    MelonLogger.Msg(message);
                    break;
            }
        }

        public override void OnFixedUpdate()
        {
            // MelonLogger.Msg(InputManager.MousePosition.ToString());
            MelonLogger.Msg(Input.mousePosition.ToString() + " " + Input.GetAxisRaw("Horizontal"));
            // Input.
            Input
        }
        

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            // base.OnSceneWasLoaded(buildIndex, sceneName);
            
            // foreach (var joystickName in Input.GetJoystickNames())
            // {
            //     MelonLogger.Msg(joystickName);
            // }
            
            // Unity.
            // InputManager.CurrentType
            // MelonLogger.Msg(InputManager.CurrentType);
            // p_gpCurrent = TGamepad.GetProperty("current");
            
            // MelonLogger.Msg(CurrentGamepad != null ? "real" : "fake");
            
            // MelonLogger.Msg(InputSystem.TKeyboard);
            //
            // MelonLogger.Msg(InputManager.MousePosition.ToString());
            //
            // try
            // {
            //     Type t_InputSystem = ReflectionUtility.GetTypeByName("UnityEngine.InputSystem.InputSystem");
            //     // InputSystem.settings
            //     object settings = t_InputSystem.GetProperty("settings", BindingFlags.Public | BindingFlags.Static)
            //         .GetValue(null, null);
            //     // typeof(InputSettings)
            //     Type t_Settings = settings.GetActualType();
            //     // InputSettings.supportedDevices
            //     PropertyInfo supportedProp =
            //         t_Settings.GetProperty("supportedDevices", BindingFlags.Public | BindingFlags.Instance);
            //     object supportedDevices = supportedProp.GetValue(settings, null);
            //     
            //     object[] emptyStringArray = new object[] { new UnhollowerBaseLib.Il2CppStringArray(0) };
            //     MethodInfo op_implicit = supportedDevices.GetActualType().GetMethod("op_Implicit", BindingFlags.Static | BindingFlags.Public);
            //     supportedProp.SetValue(settings, op_implicit.Invoke(null, emptyStringArray), null);
            //     
            //     foreach (var o in supportedDevices.TryCast<List<object>>())
            //     {
            //         MelonLogger.Msg("Device " + o.ToString());
            //     }
            //     
            // }
            // catch (Exception e)
            // {
            //     MelonLogger.Error(e);
            // }
        }

        // public override void ()
        // {
        // Input.
        // foreach (var joystickName in Input.GetJoystickNames())
        // {
            // MelonLogger.Msg(joystickName);
        // }
        // }
    }
}