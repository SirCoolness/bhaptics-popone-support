using System;
using BhapticsPopOne;
using BhapticsPopOne.Data;
using BhapticsPopOne.MonoBehaviours;
using MelonLoader;
using UnityEngine;

public static class ModX
{
    public static void InitColliders()
    {
        var obj1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj1.name = "obj1";
        obj1.AddComponent<TriggerEnterTest>();
        obj1.AddComponent<DebugMarker>();
        
        var sphereProxy = obj1.AddComponent<CustomPhysicsObjectProxy>();
        sphereProxy.UseBounce = false;
        sphereProxy.PhysicsObjectType = PhysicsObjectType.Dynamic;
        sphereProxy.ColliderUpdatePolicy = ColliderUpdatePolicy.All;

        var collider = obj1.GetComponent<SphereCollider>();
        collider.radius = 2f;
        collider.isTrigger = true;
    }

    public static void InitHandColliders(float size)
    {
        var avatar = Mod.Instance.Data.Players.LocalPlayerContainer.Avatar;
        HandCollider.BindToTransform(Mod.Instance.Data.Players.LocalPlayerContainer.Avatar.HandLeftAttachPoint);
        HandCollider.BindToTransform(Mod.Instance.Data.Players.LocalPlayerContainer.Avatar.HandRightAttachPoint);
    }

    public static GameObject AddColliderToPlayer(Transform position, float size)
    {
        var gameObject = new GameObject($"{position.gameObject.name}_Collider");
        
        gameObject.AddComponent<TriggerEnterTest>();
        var marker = gameObject.AddComponent<DebugMarker>();
        marker._size = size;
        marker.SetSize();
         
        var sphereProxy = gameObject.AddComponent<CustomPhysicsObjectProxy>();
        sphereProxy.UseBounce = false;
        sphereProxy.PhysicsObjectType = PhysicsObjectType.Dynamic;
        sphereProxy.ColliderUpdatePolicy = ColliderUpdatePolicy.All;

        var collider = gameObject.AddComponent<CapsuleCollider>();
        collider.height = size * 2.5f;
        collider.radius = size;
        collider.isTrigger = true;

        gameObject.transform.parent = position;
        gameObject.transform.localPosition = Vector3.zero;

        return gameObject;
    }
    
    public static void MoveColliderAway()
    {
        var obj1 = GameObject.Find("obj1");
        obj1.transform.position = obj1.transform.position + new Vector3(10f, 0, 0);
    }
    
    public static void MoveColliderToPlayer()
    {
        var obj1 = GameObject.Find("obj1");
        obj1.transform.position = Mod.Instance.Data.Players.LocalPlayerContainer.Avatar.Rig.transform.position;
    }

    public static void LookupCollision(int extra)
    {
        for (int i = 0; i < 32; i++)
        {
            if (!Physics.GetIgnoreLayerCollision(i, i) && !Physics.GetIgnoreLayerCollision(i, extra))
                MelonLogger.Log(ConsoleColor.Green, $"Self Collision on {i}");
        }
    }
    
    public static void MaterialLookup()
    {
        var materials = Resources.FindObjectsOfTypeAll<Material>();
        Resources.Load<Material>("MainMenu--Batched");
        foreach (var material in materials)
        {
            MelonLogger.Log(material.name);
        }
    }
}