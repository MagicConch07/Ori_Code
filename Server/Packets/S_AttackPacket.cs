using HelloNetwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packets
{
    public class S_AttackPacket : Packet
    {
        public override ushort ID => (ushort)PacketID.S_AttackPacket;

        public PlayerPacket playerData;
        public int damaged;

        public override void Deserialize(ArraySegment<byte> buffer)
        {
            ushort process = 0;

            process += sizeof(ushort);
            process += sizeof(ushort);

            process += PacketUtility.ReadDataPacket<PlayerPacket>(buffer, process, out playerData);
            process += PacketUtility.ReadIntData(buffer, process, out damaged);
        }

        public override ArraySegment<byte> Serialize()
        {
            ArraySegment<byte> buffer = UniqueBuffer.Open(1024);
            ushort process = 0;

            process += sizeof(ushort);
            process += PacketUtility.AppendUShortData(this.ID, buffer, process);
            process += PacketUtility.AppendDataPacket<PlayerPacket>(this.playerData, buffer, process);
            process += PacketUtility.AppendIntData(this.damaged, buffer, process);
            PacketUtility.AppendUShortData(process, buffer, 0);

            return UniqueBuffer.Close(process);
        }
    }
}