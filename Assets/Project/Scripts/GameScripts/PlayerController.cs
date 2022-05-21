using FishNet;
using FishNet.Object;
using FishNet.Object.Prediction;
using FishNet.Object.Synchronizing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour, IDamageable
{
    [SyncVar] public PlayerManager playerManager;

    [SerializeField] GameObject cameraHolder;

    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;

    [SerializeField] Item[] items;
    int itemIndex;
    int previousItemIndex = -1;


    private float ySpeed;
    private float originalStepOffset;
    float verticalLookRotation;
    bool rbOwner;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;
    CharacterController _characterController;


    const float maxHealth = 100f;

    [SyncVar] public float currentHealth = maxHealth;


    void Awake()
    {
        _characterController=GetComponent<CharacterController>();
        originalStepOffset = _characterController.stepOffset;

    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            EquipItem(0);
        }
        else
        {
            //Destroy(hud);
        }
    }

    private void Update()
    {
        if (!base.IsOwner)
            return;


        Move();
        SwitchItems();
        Shoot();
        if (transform.position.y < -10f)
        {
            Die();
        }
    }
    void Move()
    {
        transform.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X") * mouseSensitivity));

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;


        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 offset = new Vector3(horizontal, Physics.gravity.y, vertical) * walkSpeed;
        offset = transform.TransformDirection(offset);

        ySpeed += Physics.gravity.y * Time.deltaTime;


        if (_characterController.isGrounded)
        {
            _characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ySpeed = jumpForce;
                Debug.Log("Jumping");
            }
        }
        else
        {
            _characterController.stepOffset = 0;
        }

        offset.y = ySpeed;

        _characterController.Move(offset * Time.deltaTime);
    }
    void EquipItem(int _index)
    {
        if (_index == previousItemIndex)
            return;

        itemIndex = _index;
        items[itemIndex].itemGameObject.SetActive(true);

        if (previousItemIndex != -1)
        {
            items[previousItemIndex].itemGameObject.SetActive(false);
        }
        previousItemIndex = itemIndex;

        if (base.IsOwner)
        {
            PlayerManager.Instance.itemIndex = itemIndex;
        }
        
    }
    

    void SwitchItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            if (itemIndex >= items.Length - 1)
            {
                EquipItem(0);
            }
            else
            {
                EquipItem(itemIndex + 1);
            }


        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            if (itemIndex <= 0)
            {
                EquipItem(items.Length - 1);
            }
            else
            {
                EquipItem(itemIndex - 1);
            }

        }
        
    }
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            items[itemIndex].Use();
        }
        
    }


    public void TakeDamage(float damage,int playerId)
    {
        Debug.Log("took damge: " + damage+" from playerID: "+playerId);
        //RPC_TakeDamge(damage);
        //Debug.Log("took damage: " + damage);

        if ((currentHealth - damage) <= 0)
        {
            if (playerId >=0)
            {
                //Debug.Log("Send message to: " + playerId);
                GameManager.Instance.ChangeKillScoreForPlayer(playerId);
                //Debug.Log(InstanceFinder.ClientManager.Clients[playerId].ClientId);
                //InstanceFinder.ClientManager.Clients[playerId]
            }
        }

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    /*
    [ServerRpc(RequireOwnership = false)]
    private void RPC_TakeDamge(float damage)
    {
        //Debug.Log("took damage: " + damage);

        if((currentHealth -= damage) <= 0)
        {
            
        }

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }*/
    //[ServerRpc]
    void Die()
    {
        //Debug.Log("Dead");
        playerManager.TargetPlayerKilled(Owner);
        
        Despawn();
        //Destroy(gameObject);
    }
}
