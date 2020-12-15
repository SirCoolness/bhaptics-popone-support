using System;
using System.Linq;
using BhapticsPopOne.Haptics;
using Goyfs.Instance;
using Goyfs.Signal;
using Harmony;
using Il2CppSystem.Collections.Generic;
using Il2CppSystem.Reflection;
using MelonLoader;

namespace BhapticsPopOne.Data
{
    [HarmonyPatch(typeof(PlayerFirearm), "PlayFireFx")]
    public class PlayFireFx
    {
        // right now this prefix gets called twice
        static void Prefix(PlayerFirearm __instance)
        {
            if (__instance.container != Mod.Instance.Data.Players.LocalPlayerContainer)
                return;
            
            MelonLogger.Log(__instance.usableBehaviour.Info.name);
            PatternManager.FirearmFire(__instance.usableBehaviour.Info.Class, __instance.usableBehaviour.Info.name);
        }
    }
}
