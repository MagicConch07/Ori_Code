using System.Collections;
using System.Collections.Generic;
using Packets;
using UnityEngine;

public class EnterRoomBtn : MonoBehaviour
{
    public void EnterRoom()
    {
        if (GameManager.Instance.PlayerID < 0)
            return;

        C_RoomEnterPacket packet = new C_RoomEnterPacket();
        packet.playerID = (ushort)GameManager.Instance.PlayerID;

        NetworkManager.Instance.Send(packet);
    }
}