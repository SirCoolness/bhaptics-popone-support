using System;
using System.Data;
using BhapticsPopOne;
using BhapticsPopOne.ConfigManager;
using BhapticsPopOne.Data;
using BhapticsPopOne.MonoBehaviours;
using Goyfs.Command;
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
        var player = Mod.Instance.Data.Players.LocalPlayerContainer;
        var avatar = player.Avatar;
        HandCollider.BindToTransform(Mod.Instance.Data.Players.LocalPlayerContainer.Avatar.HandLeftAttachPoint, Handedness.Left, player.netId);
        HandCollider.BindToTransform(Mod.Instance.Data.Players.LocalPlayerContainer.Avatar.HandRightAttachPoint, Handedness.Right, player.netId);
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
            if (!Physics.GetIgnoreLayerCollision(i, extra))
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
    
    public static void DrawBounds()
    {
        var hands = GameObject.FindObjectsOfType<HandCollider>();
        foreach (var handCollider in hands)
        {
            BattleRoyaleExtensions.DrawBounds(handCollider.GetComponent<Collider>().bounds, handCollider.transform.position, Color.red);
        }
    }

    public static void ForceBind()
    {
        var players = Mod.Instance.Data.Players.PlayerContainers;

        foreach (var playerContainer in players)
        {
            if (playerContainer.transform.root != playerContainer.transform || playerContainer.Avatar?.Rig == null || !playerContainer.Data.IsReady)
                return;
            
            // MelonLogger.Log($"ForceBind {Logging.StringifyVector3(playerContainer.Avatar.HandLeftAttachPoint.transform.position)} {playerContainer.Data.DisplayName} {playerContainer.Avatar.IsAvatarReady}");
            HandCollider.BindToTransform(playerContainer.Avatar.HandLeftAttachPoint, Handedness.Left, playerContainer.netId);
            HandCollider.BindToTransform(playerContainer.Avatar.HandRightAttachPoint, Handedness.Right, playerContainer.netId);
            
            DestructibleCollisionHelp.BindToTransform(playerContainer.Avatar.HandLeft, Handedness.Left, playerContainer.netId);
            DestructibleCollisionHelp.BindToTransform(playerContainer.Avatar.HandRight, Handedness.Right, playerContainer.netId);
        }
    }
    
    public static void ForceReloadConfig()
    {
        ConfigLoader.InitConfig();
    }

    public static void CheckIgnore()
    {
        var avatar =  Mod.Instance.Data.Players.LocalPlayerContainer?.Avatar;
        
        if (avatar == null)
            return;

        var leftColl = avatar.HandLeft.GetComponent<CapsuleCollider>();
        var rightColl = avatar.HandRight.GetComponent<CapsuleCollider>(); 
        
        Physics.IgnoreCollision(leftColl, rightColl, false);
    }

    public static void Simulate(int amount)
    {
        Physics.Simulate(amount);
        MelonLogger.Log(ConsoleColor.Red, Physics.autoSimulation);
    }

    public static void Start()
    {
        Physics.autoSimulation = true;
        ModX.CheckIgnore();
    }
}