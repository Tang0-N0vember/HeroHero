using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class ItemDisplay : NetworkBehaviour
{
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            //gameObject.SetActive(true);
        }
        else if (!base.IsOwner)
        {
            //gameObject.SetActive(false);
        }

    }
}
