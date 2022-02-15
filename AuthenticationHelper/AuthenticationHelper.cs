using System.IO;
using BigBoxVR;
using HarmonyLib;
using MelonLoader;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Internal;
using PlayFab.SharedModels;
using UnhollowerBaseLib;
using UnityEngine;

[assembly: MelonInfo(typeof(AuthenticationHelper.AuthenticationHelper), "Quest Authentication Helper", "0.0.1", "SirCoolness")]
[assembly: MelonGame("BigBoxVR", "Population: ONE")]

namespace AuthenticationHelper
{
    public class AuthenticationHelper : MelonMod
    {
        private static LoginResult LoginRes;
        
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            
            if (!LoadLogin())
            {
                MelonLogger.Error("Failed to load login data");
                Application.Quit(1);
            }
        }

        private static bool LoadLogin()
        {
            var outputDir = Path.Combine(MelonHandler.ModsDirectory, nameof(AuthenticationHelper));
            Directory.CreateDirectory(outputDir);
            
            var outputFile = Path.Combine(outputDir, "login.json");

            string rawSessionInfo = "";
            try
            {
                rawSessionInfo = System.IO.File.ReadAllText(outputFile);
            }
            catch (FileNotFoundException err)
            {
                return false;
            }
            
            if (rawSessionInfo.Length < 2)
                return false;
            
            LoginRes = BigBoxJsonUtility.ParseAs<LoginResult>(rawSessionInfo);

            return true;
        }
        
        public static void ProxyRequest(GetPlayerCombinedInfoRequestParams infoParams, Il2CppSystem.Action<LoginResult> resultCallback, Il2CppSystem.Action<PlayFabError> errorCallback)
        {
            PlayFabHttp.InitializeHttp();

            var instanceApi = new PlayFabClientInstanceAPI
            {
                apiSettings = PlayFabSettings.staticSettings,
                authenticationContext = PlayFabSettings.staticPlayer
            };
            var reqContainer = new CallRequestContainer
            {
                ApiEndpoint = "/Client/LoginWithCustomID",
                ApiResult = LoginRes,
                context = instanceApi.authenticationContext,
                settings = instanceApi.apiSettings,
                instanceApi = instanceApi.Cast<IPlayFabInstanceApi>(),
            };
            
            PlayFabHttp.instance.OnPlayFabApiResult(reqContainer);
            
            PlayFabDeviceUtil.OnPlayFabLogin(LoginRes, reqContainer.settings, reqContainer.instanceApi);
            
            try
            {
                PlayFabHttp.SendEvent("/Client/LoginWithCustomID", new LoginWithCustomIDRequest(), LoginRes, ApiProcessingEventType.Post);
            }
            catch (Il2CppException e)
            {
                MelonLogger.Msg(e.Message);
            }
            
            PlayFabSettings.staticPlayer.CopyFrom(LoginRes.AuthenticationContext);
            
            resultCallback.Invoke(LoginRes);
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
            
            AuthenticationHelper.ProxyRequest(infoParams, resultCallback, errorCallback);
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