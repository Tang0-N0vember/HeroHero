using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    [SerializeField] GameObject graphic;

    private void Awake()
    {
        graphic.SetActive(false);
    }
}
