using System;
using System.Collections;
using System.Collections.Generic;
using Packets;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour
{
    [SerializeField] private float syncDelay = 0.0001f;
    [SerializeField] private float syncDistanceErr = 0.1f;
    private float lastSyncTime = 0f;
    private Vector3 lastSyncPosition = Vector3.zero;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void LateUpdate()
    {
        if (lastSyncTime + syncDelay > Time.time)
            return;

        if ((lastSyncPosition - transform.position).sqrMagnitude < syncDistanceErr * syncDistanceErr)
            return;

        PlayerPacket playerData = new PlayerPacket();
        playerData.playerID = (ushort)GameManager.Instance.PlayerID;
        playerData.x = transform.position.x;
        playerData.y = transform.position.y;
        playerData.z = transform.position.z;

        playerData.xAngle = transform.rotation.eulerAngles.x;
        playerData.yAngle = transform.rotation.eulerAngles.y;
        playerData.zAngle = transform.rotation.eulerAngles.z;

        playerData.xAnim = _playerInput.dir.x;
        playerData.yAnim = _playerInput.dir.y;
        playerData.zAnim = _playerInput.dir.z;

        C_MovePacket packet = new C_MovePacket();
        packet.playerData = playerData;

        NetworkManager.Instance.Send(packet);

        lastSyncPosition = transform.position;
        lastSyncTime = Time.time;
    }
}