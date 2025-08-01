﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Packets
{
    public enum PacketID
    {
        // System
        C_LogInPacket, // 클라이언트가 로그인 요청을 보낼 때 전송하는 패킷
        S_LogInPacket, // 서버가 클라이언트에게 로그인 성공했음을 알리는 패킷
        C_RoomEnterPacket, // 클라이언트가 게임 참가 요청을 보낼 때 전송하는 패킷
        S_RoomEnterPacket, // 서버가 게임 참가에 성공했음을 알리는 패킷
        S_PlayerJoinPacket, // 서버가 클라이언트에게 새로운 클라이언트가 접속했음을 알리는 패킷
        S_SelectPacket, // 캐릭터 선택 (서버)
        C_SelectPacket, // 캐릭터 선택 (클라)

        // Play
        S_MovePacket, // 서버가 클라이어트에게 특정 플레이어가 움직였음을 알리는 패킷
        C_MovePacket, // 클라이언트가 서버에게 본인이 움직였음을 알리는 패킷
        S_AttackPacket, // 서버가 클라이어트에게 특정 플레이어가 아이템을 획득했음을 알리는 패킷
        C_AttackPacket, // 클라이언트가 서버에게 본인이 아이템을 획득했음을 알리는 패킷
        S_HitPacket, // 서버가 클라이어트에게 특정 플레이어가 피격당하였음을 알리는 패킷
        C_HitPacket, // 클라이언트가 서버에게 본인이 피격당하였음을 알리는 패킷
        S_DiePacket, // 서버가 클라이어트에게 특정 플레이어가 죽었음을 알리는 패킷
        C_DiePacket, // 클라이언트가 서버에게 본인이 죽었음을 알리는 패킷
        S_CreateItemPacket, // 서버가 클라이언트에게 아이템이 생성됐음을 알리는 패킷
        S_ItemPacket, // 서버가 클라이어트에게 특정 플레이어가 아이템을 획득했음을 알리는 패킷
        C_ItemPacket // 클라이언트가 서버에게 본인이 아이템을 획득했음을 알리는 패킷

    }
}