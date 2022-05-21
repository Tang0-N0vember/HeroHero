using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }

    [SyncObject] public readonly SyncList<PlayerManager> playerManagers = new SyncList<PlayerManager>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //foreach(PlayerManager playerManager in playerManagers)
        {
            //Debug.Log("player: "+ playerManager.OwnerId +" K/D: "+ playerManager.killScore);
            //Debug.Log($"player: {playerManager.OwnerId} K/D {playerManager.killScore}/{playerManager.deathScore}");
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void ChangeKillScoreForPlayer(int playerID)
    {
        PlayerManager playerManager = playerManagers.Find(x => x.OwnerId == playerID);
        playerManager.killScore++;
    }
    [ServerRpc(RequireOwnership = false)]
    public void ChangeDeathScoreForPlayer(int playerID)
    {
        PlayerManager playerManager = playerManagers.Find(x => x.OwnerId == playerID);
        playerManager.deathScore++;
    }

}
