using FishNet.Connection;
using FishNet.Object;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameManager : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameInput;

    private void Awake()
    {
        usernameInput.onSubmit.AddListener(_input_OnSubmit);

    }

    private void _input_OnSubmit(string text)
    {
        PlayerNameTracker.SetName(text);
    }
}
