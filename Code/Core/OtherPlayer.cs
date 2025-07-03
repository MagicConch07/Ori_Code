using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Packets;
using SingularityGroup.HotReload;
using TMPro;
using UnityEngine.UI;

public class OtherPlayer : MonoBehaviour
{
    private Animator _animator;
    private AgentHp _hp;
    private readonly int _animationNumHash = Animator.StringToHash("animation");
    private ushort otherId = 9999;
    public ushort characterID;

    public ushort OtherID
    {
        get => otherId;
        set
        {
            if(otherId != 9999) return;
            otherId = value;
        }
    }
    
    private TextMeshProUGUI _text;
    
    private void Awake()
    {
        _animator = transform.Find("Visual").GetComponent<Animator>();
        _hp = GetComponent<AgentHp>();
        _text = FindObjectOfType<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(otherId);
            _text.text = Convert.ToString(otherId);
        }
    }

    public void WalkAnimation_Net(PlayerPacket playerData)
    {
        Vector3 dir = Vector3.zero;
        dir.x = playerData.xAnim;
        dir.y = playerData.yAnim;
        dir.z = playerData.zAnim;
        
        var value = _animator.GetInteger(_animationNumHash);
        if (value is 3 or 6 or 7) return;
        _animator.SetInteger(_animationNumHash, Equals(dir, Vector3.zero) ? 0 : 2);
    }
    
    public void SetPosition(PlayerPacket playerData)
    {
        Vector3 pos = transform.position;
        pos.x = playerData.x;
        pos.y = playerData.y;
        pos.z = playerData.z;

        Vector3 anglePos = transform.rotation.eulerAngles;
        anglePos.x = playerData.xAngle;
        anglePos.y = playerData.yAngle;
        anglePos.z = playerData.zAngle;

        transform.position = pos;
        transform.rotation = Quaternion.Euler(anglePos.x, anglePos.y, anglePos.z);
    }

    public void Hit(PlayerPacket playerData)
    {
        _hp.ReceivedDamage = playerData.damged;
    }
    
}