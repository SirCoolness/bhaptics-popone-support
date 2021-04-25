#if PORT_DISABLE
using System;
using System.Linq;
using BhapticsPopOne.Haptics;
using BhapticsPopOne.Haptics.Patterns;
using Goyfs.Instance;
using Goyfs.Signal;
using Harmony;
using Il2CppSystem.Collections.Generic;
using Il2CppSystem.Reflection;
using MelonLoader;

namespace BhapticsPopOne.Data
{
    // [HarmonyPatch(typeof(PlayerFirearm), "PlayFireFx")]
    // public class PlayFireFx
    // {
    //     // right now this prefix gets called twice
    //     static void Prefix(PlayerFirearm __instance)
    //     {
    //         if (__instance.container != Mod.Instance.Data.Players.LocalPlayerContainer)
    //             return;
    //         
    //         FirearmFire.Execute(__instance.usableBehaviour.Info.Class, __instance.usableBehaviour.Info.Type);
    //     }
    // }
}
#endif