using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne.MonoBehaviours
{
    public class TriggerEnterTest : MonoBehaviour
    {
        public TriggerEnterTest(System.IntPtr ptr) : base(ptr) {}

        public void OnTriggerEnter(Collider other)
        {
            MelonLogger.Msg("OnTriggerEnter! Other: " + other.gameObject.name);
        }
    }
}