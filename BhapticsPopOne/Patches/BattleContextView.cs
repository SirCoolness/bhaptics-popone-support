using BhapticsPopOne.Helpers;
using BhapticsPopOne.Utils;
using HarmonyLib;
using MelonLoader;
using Il2Cpp;

namespace BhapticsPopOne
{
    [HarmonyPatch(typeof(BattleContextView), "Awake")]
    public class Awake
    {
        public static void Postfix()
        {
            if (!BindGlobals.BindGoyfs())
                MelonLogger.Error("Failed to bind to Goyfs");
        }
    }
}
