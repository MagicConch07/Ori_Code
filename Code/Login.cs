using System;
using System.Collections;
using System.Collections.Generic;
using Packets;
using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
    private TMP_InputField idField;

    private void Awake()
    {
        idField = GetComponent<TMP_InputField>();
    }

    public void LogIn()
    {
        C_LogInPacket packet = new C_LogInPacket();
        packet.nickname = idField.text;
        NetworkManager.Instance.Send(packet);
    }
}