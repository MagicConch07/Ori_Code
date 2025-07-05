using HelloNetwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packets
{
    public class C_SelectPacket : Packet
    {
        public override ushort ID => (ushort)PacketID.C_SelectPacket;

        public ushort characterID;
        public PlayerPacket playerData;

        public override void Deserialize(ArraySegment<byte> buffer)
        {
            ushort process = 0;

            process += sizeof(ushort);
            process += sizeof(ushort);
            process += PacketUtility.ReadDataPacket(buffer, process, out playerData);
            process += PacketUtility.ReadUShortData(buffer, process, out characterID);
        }

        public override ArraySegment<byte> Serialize()
        {
            ArraySegment<byte> buffer = UniqueBuffer.Open(1024);

            ushort process = 0;

            process += sizeof(ushort);
            process += PacketUtility.AppendUShortData(this.ID, buffer, process);
            process += PacketUtility.AppendDataPacket(this.playerData, buffer, process);
            process += PacketUtility.AppendUShortData(this.characterID, buffer, process);
            PacketUtility.AppendUShortData(process, buffer, 0);

            return UniqueBuffer.Close(process);
        }
    }
}