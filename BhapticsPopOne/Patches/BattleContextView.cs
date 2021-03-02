using BhapticsPopOne.Helpers;
using BhapticsPopOne.Utils;
using Goyfs.Instance;
using Harmony;
using MelonLoader;

namespace BhapticsPopOne
{
    [HarmonyPatch(typeof(BattleContextView), "Awake")]
    public class Awake
    {
        public static void Postfix()
        {
            MelonLogger.Log($"Postfix: {GoyfsHelper.DefaultContext.InstanceBinder.Cast<InstanceBinder>().bindings.Count}");
            if (!BindGlobals.BindGoyfs())
                MelonLogger.LogError("Failed to bind to Goyfs");
        }
    }
}