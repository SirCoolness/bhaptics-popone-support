using System;
using System.IO;
using System.Net.Mime;
using System.Threading;
using BigBoxVR;
using MelonLoader;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Harmony;
using Object = UnityEngine.Object;

[assembly: MelonInfo(typeof(LoginCapture.LoginCapture), "Login Capture", "0.0.2", "SirCoolness")]
[assembly: MelonGame("BigBoxVR", "Population: ONE")]

namespace LoginCapture
{
    public class LoginCapture : MelonMod
    {
        public override void OnLevelWasLoaded(int level)
        {
            if (level != 0)
                return;
            
            StartAuthentication();
            
            // InstallFiles.IsGameInstalled();
            
            // Application.Quit();
        }

        public static void Finalize(LoginResult result)
        {
            MelonLogger.Msg("Complete");
            
            var outputDir = Path.Combine(MelonHandler.ModsDirectory, nameof(LoginCapture));
            var outputFile = Path.Combine(outputDir, "login.json");
            
            Directory.CreateDirectory(outputDir);
            File.WriteAllText(outputFile, result.ToJson());

            // InstallFiles.IsGameInstalled();

            MelonLogger.Msg($"Saved session to [{outputFile}]");
            
            Application.Quit(0);
            Thread.Sleep(100000000);
        }

        public static void StartAuthentication()
        {
            var playfabConnection = Object.FindObjectOfType<PlayFabConnection>();

            if (playfabConnection != null)
            {
                MelonLogger.Msg("connection found");
                return;
            }
            
            MelonLogger.Msg("creating connection");
            
            var obj = new GameObject();
            obj.name = "AuthHelper";

            playfabConnection = obj.AddComponent<PlayFabConnection>();

            Object.DontDestroyOnLoad(playfabConnection);
            MelonLogger.Msg("added");
        }
    }
    
    [HarmonyLib.HarmonyPatch(typeof(PlayFabConnection), "OnResult")]
    class OnResult
    {
        static bool Prefix(ref bool __runOriginal, LoginResult result)
        {
            LoginCapture.Finalize(result);
            
            __runOriginal = false;
            return false;
        }
    }
    
    // [HarmonyLib.HarmonyPatch(typeof(PlayFabConnection), "CheckEntitlement")]
    class CheckEntitlement
    {
        static bool Prefix(ref bool __runOriginal,    
            string loginResultText,
            Il2CppSystem.Action<LoginResult> resultCallback,
            Il2CppSystem.Action<PlayFabError> errorCallback,
            GetPlayerCombinedInfoRequestParams infoParams)
        {
            MelonLogger.Msg(loginResultText);
            return false;
        }
    }
}