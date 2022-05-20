using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class ModelDisplay : NetworkBehaviour
{
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            //gameObject.SetActive(false);
        }
        //text.text = _networkObject.Owner.nam;
    }
}
