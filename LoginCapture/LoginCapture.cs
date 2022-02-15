using System;
using System.IO;
using BigBoxVR;
using HarmonyLib;
using MelonLoader;
using PlayFab.ClientModels;
using UnityEngine;

[assembly: MelonInfo(typeof(LoginCapture.LoginCapture), "Login Capture", "0.0.1", "SirCoolness")]
[assembly: MelonGame("BigBoxVR", "Population: ONE")]

namespace LoginCapture
{
    public class LoginCapture : MelonMod
    {
        public override void OnLevelWasLoaded(int level)
        {
            if (level == 0)
            {
                GameObject.Find("/SplashManager").GetComponent<SplashManager>().OnAnimationFinished();
            }
        }

        public static void Finalize(LoginResult result)
        {
            var outputDir = Path.Combine(MelonHandler.ModsDirectory, nameof(LoginCapture));
            var outputFile = Path.Combine(outputDir, "login.json");
            
            Directory.CreateDirectory(outputDir);
            File.WriteAllText(outputFile, result.ToJson());

            MelonLogger.Msg($"Saved session to [{outputFile}]");
            
            Application.Quit(0);
        }
    }
    
    [HarmonyPatch(typeof(PlayFabConnection), "OnResult")]
    class OnResult
    {
        static bool Prefix(ref bool __runOriginal, LoginResult result)
        {
            LoginCapture.Finalize(result);
            
            __runOriginal = false;
            return false;
        }
    }
}