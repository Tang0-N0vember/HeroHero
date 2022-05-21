using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using FishNet;
using FishNet.Object.Synchronizing;
using FishNet.Connection;

public class PlayerManager : NetworkBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SyncVar] public string username;

    [SyncVar] public int killScore;

    [SyncVar] public int deathScore;

    [SyncVar] public int itemIndex;


    [SyncVar] public PlayerController playerController;

    [SerializeField] GameObject playerPrefab;

    public override void OnStartServer()
    {
        base.OnStartServer();

        GameManager.Instance.playerManagers.Add(this);

    }
    public override void OnStopServer()
    {
        base.OnStopServer();

        GameManager.Instance.playerManagers.Remove(this);
    }


    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
            return;

        Instance = this;
        ServerSpawnPawn();
    }

    void CreatePawn()
    {
        
    }
    
    [ServerRpc]
    public void ReSpawn(NetworkConnection networkConnection)
    {

        //Destroy(_playerControllerPrefab);
        //InstanceFinder.ServerManager.Despawn(_playerControllerPrefab);
        //InstanceFinder.ServerManager.Despawn(playerController);
        //GameObject playerInstance = playerController.GetComponent<GameObject>();
        //InstanceFinder.ServerManager.Despawn(playerInstance);

    }
    [ServerRpc]
    public void ConfirmKill()
    {
        //killScore++;
    }


    [TargetRpc]
    public void TargetPlayerKilled(NetworkConnection networkConnection)
    {
        
        
        PlayerController playerInstance = playerController;
        
        //InstanceFinder.ServerManager.Despawn(playerInstance.gameObject);
        GameManager.Instance.ChangeDeathScoreForPlayer(OwnerId);
        
        ServerSpawnPawn();
    }

    [ServerRpc]
    private void ServerSpawnPawn()
    {
        GameObject playerInstance = Instantiate(playerPrefab);
        Spawn(playerInstance, Owner);
        playerController = playerInstance.GetComponent<PlayerController>();
        playerController.playerManager = this;
    }
}
