using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : NetworkBehaviour
{
    //[SerializeField] LayerMask firstPersonCullingMask;
    //[SerializeField] LayerMask thirdPersonCullingMask;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            gameObject.SetActive(true);
            //gameObject.GetComponent<Camera>().cullingMask = firstPersonCullingMask;
        }
        else if (!base.IsOwner)
        {
            //gameObject.GetComponent<Camera>().cullingMask = thirdPersonCullingMask;
        }
        


    }
}
