using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using TMPro;

public class UsernameDisplay : NetworkBehaviour
{
    [SerializeField] TMP_Text text;

    // Start is called before the first frame update
    public  override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            gameObject.SetActive(false);
        }
    }
}
