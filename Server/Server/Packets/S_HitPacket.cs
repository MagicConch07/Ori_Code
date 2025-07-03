using HelloNetwork;
using System;

namespace Packets
{
    public class S_HitPacket : Packet
    {
        public override ushort ID => (ushort)PacketID.S_HitPacket;

        public PlayerPacket playerData;
        public int hp;

        public override void Deserialize(ArraySegment<byte> buffer)
        {
            ushort process = 0;

            process += sizeof(ushort);
            process += sizeof(ushort);
            process += PacketUtility.ReadDataPacket<PlayerPacket>(buffer, process, out playerData);
            process += PacketUtility.ReadIntData(buffer, process, out hp);
        }

        public override ArraySegment<byte> Serialize()
        {
            ArraySegment<byte> buffer = UniqueBuffer.Open(1024);
            ushort process = 0;

            process += sizeof(ushort);
            process += PacketUtility.AppendUShortData(this.ID, buffer, process);
            process += PacketUtility.AppendDataPacket<PlayerPacket>(this.playerData, buffer, process);
            process += PacketUtility.AppendIntData(this.hp, buffer, process);
            PacketUtility.AppendUShortData(process, buffer, 0);

            return UniqueBuffer.Close(process);
        }
    }
}