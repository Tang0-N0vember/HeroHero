using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerCameraController : NetworkBehaviour
{
    public override void OnStartServer()
    {
        base.OnStartServer();
        if (base.IsServer)
        {
            Debug.Log("is Server");
            gameObject.SetActive(true);
        }
            
    }
}
