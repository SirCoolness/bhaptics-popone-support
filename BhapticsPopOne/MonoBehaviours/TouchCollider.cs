using System.Linq;
using MelonLoader;
using UnityEngine;

namespace BhapticsPopOne
{
    public class TouchCollider : MonoBehaviour
    {
        public TouchCollider(System.IntPtr ptr) : base(ptr) {}
     
        public static float width = 0.115f;

        public static int Layer = 7; // punch

        public static string[] filter = new[]
        {
            "male_a_rig:spine_01",
            "male_a_rig:spine_02",
            "male_a_rig:spine_03",
            "male_a_rig:clavicle_l",
            "male_a_rig:clavicle_l",
        };
        
        private void OnTriggerEnter(Collider other)
        {
            if (!filter.Contains(other.name))
                return;
            
            MelonLogger.Log($"{other.name}");
        }
        
        public static void BindToTransform(Transform dest)
        {
            var exists = dest.Find("TouchCollider") != null;
            if (exists)
                return;
            
            var gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gameObject.name = "TouchCollider";
            gameObject.transform.localScale = new Vector3(width, width, width);

            gameObject.layer = Layer;

            gameObject.transform.parent = dest;
            gameObject.transform.localPosition = Vector3.zero;

            DestroyImmediate(gameObject.GetComponent<Rigidbody>());
            
            var collider = gameObject.GetComponent<SphereCollider>();
            collider.radius = width / 2f;
            collider.isTrigger = true;

            gameObject.AddComponent<TouchCollider>();

            var sphereProxy = gameObject.AddComponent<CustomPhysicsObjectProxy>();
            sphereProxy.UseBounce = false;
            sphereProxy.PhysicsObjectType = PhysicsObjectType.Dynamic;
            sphereProxy.ColliderUpdatePolicy = ColliderUpdatePolicy.All;
        }
    }
}