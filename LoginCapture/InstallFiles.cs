// using System;
// using MelonLoader;
// using SharpAdbClient;
// using SharpAdbClient.DeviceCommands;
//
// namespace LoginCapture
// {
//     public static class InstallFiles
//     {
//         static string[] check_paths = new[]
//         {
//             $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\SideQuest\platform-tools\adb.exe"
//         };
//
//         private static string adb_path;
//
//         private static readonly string game = "com.SirCoolness.SampleGame";
//
//         static InstallFiles()
//         {
//             FindAdbPath();
//         }
//
//         private static void FindAdbPath()
//         {
//             foreach (var checkPath in check_paths)
//             {
//                 if (!System.IO.File.Exists(checkPath)) 
//                     continue;
//
//                 adb_path = checkPath;
//                 break;
//             }
//             
//             if (adb_path == null)
//                 MelonLogger.Msg("Cannot find ADB. Try installing SideQuest.");
//         }
//         
//         public static bool IsGameInstalled()
//         {
//             AdbServer server = new AdbServer();
//             var result = server.StartServer(adb_path, true);
//             
//             var client = new AdbClient();
//             client.GetDevices();
//
//             return false;
//         }
//     }
// }