using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using HelloNetwork;
using UnityEngine;

public class ServerSession : Session
{
    public override void OnConnected(EndPoint endPoint)
    {
        NetworkManager.Instance.IsConnect = true;
    }

    public override void OnDisconnected(EndPoint endPoint)
    {
        NetworkManager.Instance.IsConnect = false;
    }

    public override void OnPacketReceived(ArraySegment<byte> buffer)
    {
        Debug.Log($"{buffer.Count} : 데이터 받음!");
        Packet packet = PacketManager.Instance.CreatePacket(buffer);
        NetworkManager.Instance.PushPacket(packet);
    }

    public override void OnSent(int length)
    {
        Debug.Log($"길이 : {length} 데이터 보냄");
    }
}