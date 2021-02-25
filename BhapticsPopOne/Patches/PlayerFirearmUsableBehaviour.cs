using BhapticsPopOne.Haptics.Patterns;
using Harmony;
using MelonLoader;

// namespace BhapticsPopOne.PlayerFirearmUsableBehaviour2
// {
//     [HarmonyPatch(typeof(PlayerFirearmUsableBehaviour), "State", MethodType.Setter)]
//     public class StateSetter
//     {
//         static void Prefix(PlayerFirearmUsableBehaviour __instance, FirearmState value)
//         {
//             var netId = __instance.playerContainer?.netId;
//             
//             if (netId == null)
//                 return;
//             
//             if (!PlayerContainer.Find(netId.Value).isLocalPlayer)
//                 return;
//             
//             ReloadWeapon.Execute(value, __instance.GetInstanceID());
//         }
//     }
// }