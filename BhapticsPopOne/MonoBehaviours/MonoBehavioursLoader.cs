using BhapticsPopOne.MonoBehaviours;
using MelonLoader;
using UnhollowerRuntimeLib;

namespace BhapticsPopOne
{
    public class MonoBehavioursLoader
    {
        private static bool injectionDone = false;
        public static void Inject()
        {
            if (injectionDone)
            {
                MelonLogger.LogError("MonoBehaviours were already injected.");
            }
            
            ClassInjector.RegisterTypeInIl2Cpp<TriggerEnterTest>();
            ClassInjector.RegisterTypeInIl2Cpp<DebugMarker>();
            ClassInjector.RegisterTypeInIl2Cpp<HandCollider>();
            ClassInjector.RegisterTypeInIl2Cpp<DestructibleCollisionHelp>();
        }
    }
}