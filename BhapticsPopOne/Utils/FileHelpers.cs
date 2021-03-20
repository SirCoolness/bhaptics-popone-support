using System.IO;

namespace BhapticsPopOne
{
    public class FileHelpers
    {
        public static string RootDirectory => Directory.GetCurrentDirectory() + @"\Mods\BhapticsPopOne";

        public static void EnforceDirectory()
        {
            Directory.CreateDirectory(RootDirectory);
        }
    }
}