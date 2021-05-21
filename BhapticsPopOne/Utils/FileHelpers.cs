using System.IO;
using MelonLoader;

namespace BhapticsPopOne
{
    public class FileHelpers
    {
        public static string RootDirectory => Path.Combine(MelonHandler.ModsDirectory, "BhapticsPopOne");
        public static void EnforceDirectory()
        {
            Directory.CreateDirectory(RootDirectory);
        }
    }
}