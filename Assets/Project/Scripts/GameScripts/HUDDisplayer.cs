using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDDisplayer : NetworkBehaviour
{
    // Start is called before the first frame update
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
            Destroy(gameObject);

    }
}
