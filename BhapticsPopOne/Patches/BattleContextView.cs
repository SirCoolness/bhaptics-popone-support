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
            if (!BindGlobals.BindGoyfs())
                MelonLogger.Error("Failed to bind to Goyfs");
        }
    }
}
