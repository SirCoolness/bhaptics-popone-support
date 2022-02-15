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
                MelonLogger.Error("MonoBehaviours were already injected.");
            }
            
            ClassInjector.RegisterTypeInIl2Cpp<VelocityTracker>(true);
            ClassInjector.RegisterTypeInIl2Cpp<TriggerEnterTest>(true);
            ClassInjector.RegisterTypeInIl2Cpp<DebugMarker>(true);
            ClassInjector.RegisterTypeInIl2Cpp<DestructibleCollisionHelp>(true);
            ClassInjector.RegisterTypeInIl2Cpp<HighFiveTarget>(true);
            ClassInjector.RegisterTypeInIl2Cpp<TouchCollider>(true);
            ClassInjector.RegisterTypeInIl2Cpp<SendTouch>(true);
            ClassInjector.RegisterTypeInIl2Cpp<GeneralTouchCollider>(true);
            ClassInjector.RegisterTypeInIl2Cpp<MeleeVelocity>(true);
        }
    }
}