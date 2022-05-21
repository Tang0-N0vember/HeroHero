using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDDisplayer : NetworkBehaviour
{
    [SerializeField] Image healthbarImage;

    [SerializeField] GameObject playerHUD;
    [SerializeField] GameObject ui;

    [SerializeField] TMP_Text kdText;

    //bool isActive=false;
    // Start is called before the first frame update
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
            return;


    }
    private void Update()
    {
        PlayerManager playerManager = PlayerManager.Instance;
        if (playerManager == null || playerManager.playerController == null)
        {
            playerHUD.SetActive(false);
            return;
        }
        else
        {
            if(playerManager.playerController == null)
            {
                return;
            }
            playerHUD.SetActive(true);

            healthbarImage.fillAmount = PlayerManager.Instance.playerController.currentHealth / 100;//maxHealth;
            kdText.text = $"{PlayerManager.Instance.killScore}/{PlayerManager.Instance.deathScore}";
        }
        
    }
}
