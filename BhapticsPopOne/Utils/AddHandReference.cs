using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Haptics.Patterns;
using BhapticsPopOne.MonoBehaviours;
using UnityEngine;

namespace BhapticsPopOne
{
    public class AddHandReference
    {
        public static float width = 0.125f;

        public static void AddHandsToPlayer(PlayerContainer player)
        {
            // is this the way to do things, no.
            Collider[] leftIgnore;
            Collider[] rightIgnore;

            var leftRBRes = AddColliderHand(player.Avatar?.HandLeftAttachPoint?.gameObject, Handedness.Left);
            var rightRBRes = AddColliderHand(player.Avatar?.HandRightAttachPoint?.gameObject, Handedness.Right);

            if (leftRBRes == null)
                leftIgnore = new Collider[]
                {
                    player.Avatar.HandLeft.gameObject.GetComponent<CapsuleCollider>()
                };
            else
                leftIgnore = new Collider[]
                {
                    player.Avatar.HandLeft.gameObject.GetComponent<CapsuleCollider>(),
                    leftRBRes.GetComponent<SphereCollider>()
                };
            
            if (rightRBRes == null)
                rightIgnore = new Collider[]
                {
                    player.Avatar.HandRight.gameObject.GetComponent<CapsuleCollider>()
                };
            else
                rightIgnore = new Collider[]
                {
                    player.Avatar.HandRight.gameObject.GetComponent<CapsuleCollider>(),
                    rightRBRes.GetComponent<SphereCollider>()
                };
            
            ApplyComponents(player.netId, Handedness.Left, AddHand(player.Avatar?.HandLeftAttachPoint?.gameObject, leftIgnore));
            ApplyComponents(player.netId, Handedness.Right, AddHand(player.Avatar?.HandRightAttachPoint?.gameObject, rightIgnore));
        }

        private static GameObject AddColliderHand(GameObject dest, Handedness hand)
        {
            string name = $"{nameof(AddHandReference)}{HapticUtils.HandExt(hand)}";
            
            if (dest?.transform.Find(name) != null)
                return null;
            
            var gameObject = new GameObject(name);
            gameObject.transform.parent = dest.transform;
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.layer = 19;
            gameObject.transform.localScale = new Vector3(width, width, width);
            
            var rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            
            var collider = gameObject.AddComponent<SphereCollider>();
            collider.radius = width / 2f;
            collider.isTrigger = false;
            
            var proxy = gameObject.AddComponent<CustomPhysicsObjectProxy>();
            proxy.useBounce = false;
            proxy.PhysicsObjectType = PhysicsObjectType.Dynamic;
            proxy.ColliderUpdatePolicy = ColliderUpdatePolicy.All;

            return gameObject;
        }
        
        private static GameObject AddHand(GameObject dest, Collider[] ignoreC)
        {
            if (dest?.transform.Find(nameof(AddHandReference)) != null)
                return null;
            
            var gameObject = new GameObject(nameof(AddHandReference));
            gameObject.transform.parent = dest.transform;
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.layer = 19;
            gameObject.transform.localScale = new Vector3(width, width, width);

            var rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            
            var collider = gameObject.AddComponent<SphereCollider>();
            collider.radius = width / 2f;
            collider.isTrigger = true;

            foreach (var collider1 in ignoreC)
            {
                Physics.IgnoreCollision(collider, collider1);
            }

            var proxy = gameObject.AddComponent<CustomPhysicsObjectProxy>();
            proxy.useBounce = false;
            proxy.PhysicsObjectType = PhysicsObjectType.Dynamic;
            proxy.ColliderUpdatePolicy = ColliderUpdatePolicy.All;

            return gameObject;
        }

        private static void ApplyComponents(uint netId, Handedness hand, GameObject dest)
        {
            if (dest == null)
                return;
            
            // HandCollider.BindToTransform(dest.transform, hand, netId);
            // TouchCollider.BindToTransform(dest.transform);
            // GeneralTouchCollider.BindToTransform(dest.transform);

            if (Mod.Instance.Data.Players.LocalPlayerContainer.netId != netId)
            {
                if (ConfigLoader.Config.Toggles.PlayerTouching)
                {
                    TouchCollider.BindToTransform(dest.transform);
                    GeneralTouchCollider.BindToTransform(dest.transform);
                }
            }
            else
            {
                DestructibleCollisionHelp.BindToTransform(dest.transform, hand, netId);
                if (ConfigLoader.Config.Toggles.PlayerTouching)
                    SendTouch.BindToTransform(dest.transform, hand);
            }
        }
    }
}