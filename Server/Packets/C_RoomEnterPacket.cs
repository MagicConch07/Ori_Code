﻿using System;
using HelloNetwork;

namespace Packets
{
    public class C_RoomEnterPacket : Packet
    {
        public override ushort ID => (ushort)PacketID.C_RoomEnterPacket;

        public ushort playerID;
        public ushort characterID;

        public override void Deserialize(ArraySegment<byte> buffer)
        {
            ushort process = 0;

            process += sizeof(ushort);
            process += sizeof(ushort);
            process += PacketUtility.ReadUShortData(buffer, process, out playerID);
            process += PacketUtility.ReadUShortData(buffer, process, out characterID);
        }

        public override ArraySegment<byte> Serialize()
        {
            ArraySegment<byte> buffer = UniqueBuffer.Open(1024);
            ushort process = 0;

            process += sizeof(ushort);
            process += PacketUtility.AppendUShortData(this.ID, buffer, process);
            process += PacketUtility.AppendUShortData(this.playerID, buffer, process);
            process += PacketUtility.AppendUShortData(this.characterID, buffer, process);
            PacketUtility.AppendUShortData(process, buffer, 0);

            return UniqueBuffer.Close(process);
        }
    }
}