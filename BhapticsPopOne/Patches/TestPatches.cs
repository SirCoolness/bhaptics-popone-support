using System.Collections.Generic;
using System.Reflection;

namespace BhapticsPopOne.Patches
{
    public class TestPatches
    {
        private MethodInfo[] _methodsToTest = new[]
        {
            typeof(PlayerContainer).GetMethod("HandlePlayerHit"),
            typeof(PlayerBuff).GetMethod("OnBuffStateChanged"),
            typeof(PlayerFirearm).GetMethod("PlayFireFx"),
        };

        public bool Test()
        {
            return Test(new List<MethodInfo>());
        }

        public bool Test(List<MethodInfo> results)
        {
            bool pass = true;
            
            foreach (var methodInfo in _methodsToTest)
            {
                var patchStatus = Mod.Instance.harmonyInstance.GetPatchInfo(methodInfo);
                if (patchStatus is null)
                {
                    pass = false;
                    results.Add(methodInfo);
                }
            }

            return pass;
        }
    }
}