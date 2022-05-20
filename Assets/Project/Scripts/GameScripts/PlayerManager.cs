using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using FishNet;

public class PlayerManager : NetworkBehaviour
{

    [SerializeField] GameObject _playerControllerPrefab;


    public override void OnStartClient()
    {
        base.OnStartClient();
        //if (base.IsOwner)
        {
            //CreateController();
        }
    }

    void CreateController()
    {
        //_playerControllerPrefab = GameObject.Instantiate(_playerControllerPrefab,gameObject.transform.position, gameObject.transform.rotation);
        //_playerControllerPrefab.transform.parent = gameObject.transform;
        //GameObject player = Instantiate(_playerControllerPrefab, gameObject.transform.position, gameObject.transform.rotation);
        //InstanceFinder.ServerManager.Spawn(_playerControllerPrefab, base.Owner);
        //base.Spawn(_playerControllerPrefab, base.Owner);
        //player.SetActive(true);
        //InstanceFinder.ServerManager.Spawn(player,ownerConnection);
        //controller = NetworkObject.Instantiate(_playerControllerPrefab, Vector3.zero, Quaternion.identity);
        //InstanceFinder.ServerManager.Spawn(controller);
    }
    public void Die()
    {
        //Destroy(_playerControllerPrefab);
        //InstanceFinder.ServerManager.Despawn(_playerControllerPrefab);
        CreateController();
    }
}
