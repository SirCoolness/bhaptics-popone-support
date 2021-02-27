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
            
            ClassInjector.RegisterTypeInIl2Cpp<VelocityTracker>();
            ClassInjector.RegisterTypeInIl2Cpp<TriggerEnterTest>();
            ClassInjector.RegisterTypeInIl2Cpp<DebugMarker>();
            ClassInjector.RegisterTypeInIl2Cpp<DestructibleCollisionHelp>();
            ClassInjector.RegisterTypeInIl2Cpp<HighFiveTarget>();
            ClassInjector.RegisterTypeInIl2Cpp<TouchCollider>();
            ClassInjector.RegisterTypeInIl2Cpp<SendTouch>();
            ClassInjector.RegisterTypeInIl2Cpp<GeneralTouchCollider>();
            ClassInjector.RegisterTypeInIl2Cpp<MeleeVelocity>();
        }
    }
}