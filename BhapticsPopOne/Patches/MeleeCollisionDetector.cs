﻿using Harmony;
using MelonLoader;

namespace BhapticsPopOne
{
    [HarmonyPatch(typeof(MeleeCollisionDetector), "ClearSlash")]
    public class ClearSlash
    {
        public static void Prefix(MeleeCollisionDetector __instance)
        {
            if (!__instance.UsableBehaviour.PlayerUsable.isLocalPlayer)
                return;
            
            Haptics.Patterns.MeleeVelocity.IsSlicing = false;
        }
    }
}
