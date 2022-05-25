using System.IO;
using BigBoxVR;
using HarmonyLib;
using Il2CppSystem;
using MelonLoader;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Internal;
using PlayFab.SharedModels;
using UnhollowerBaseLib;
using UnityEngine;

[assembly: MelonInfo(typeof(AuthenticationHelper.AuthenticationHelper), "Quest Authentication Helper", "0.0.2", "SirCoolness")]
[assembly: MelonGame("BigBoxVR", "Population: ONE")]

namespace AuthenticationHelper
{
    public class AuthenticationHelper : MelonMod
    {
        public static void ManualAuthentication(string oculusId, GetPlayerCombinedInfoRequestParams infoParams,
            Il2CppSystem.Action<LoginResult> resultCallback, Il2CppSystem.Action<PlayFabError> errorCallback)
        {
            var loginRequest = new LoginWithCustomIDRequest
            {
                TitleId = PlayFabConnection.IsProduction ? PlayFabConnection.ProductionTitleId : PlayFabConnection.StagingTitleId, 
                CustomId = oculusId, 
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetUserAccountInfo = true,
                    GetTitleData = true,
                    GetUserData = true,
                    GetUserReadOnlyData = true,
                }
            };
            
            PlayFabClientAPI.LoginWithCustomID(loginRequest, resultCallback, errorCallback);
        }
    }
    
    [HarmonyPatch(typeof(BigBoxVR.PlayFabConnection), nameof(BigBoxVR.PlayFabConnection.DoOculusAuthentication))]
    public class DoOculusAuthentication
    {
        static bool Prefix(ref bool __runOriginal)
        {
            __runOriginal = false;
            return false;
        }
    
        static void Postfix(BigBoxVR.PlayFabConnection __instance, string oculusID, GetPlayerCombinedInfoRequestParams infoParams, Il2CppSystem.Action<LoginResult> resultCallback, Il2CppSystem.Action<PlayFabError> errorCallback)
        {
            MelonLogger.Msg($"Postfix {oculusID}");
            
            AuthenticationHelper.ManualAuthentication(oculusID, infoParams, resultCallback, errorCallback);
        }
    }
    
    [HarmonyPatch(typeof(PlayFabUnityHttp), "OnResponse")]
    public class OnResponse
    {
        static void Prefix(string response, CallRequestContainer reqContainer)
        {
            MelonLogger.Msg($"response {response}");
        }
    }
    
    [HarmonyPatch(typeof(OculusPresenceManager), nameof(OculusPresenceManager.StartPresenceRoutine))]
    public class OculusPresence
    {
        static bool Prefix(ref bool __runOriginal)
        {
            __runOriginal = false;
            return false;
        }
    }
}