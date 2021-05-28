using System;
using BhapticsPopOne.Haptics.Patterns;
using Harmony;
using MelonLoader;

namespace BhapticsPopOne.FriendProgressPopup2
{
    [HarmonyPatch(typeof(FriendProgressPopup), "Init")]
    public class Init
    {
        static void Prefix(FriendProgressPopup __instance, float length, string name)
        {
            FistBump.Start(length);
        }
    }
    
    [HarmonyPatch(typeof(FriendProgressPopup), "Finish")]
    public class Finish
    {
        static void Prefix(FriendProgressPopup __instance, bool wasCompleted)
        {
            FistBump.Stop(wasCompleted);
        }
    }
}
