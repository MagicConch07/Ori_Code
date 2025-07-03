using HelloNetwork;
using System;

namespace Packets
{
    public class PlayerPacket : DataPacket
    {
        // 이 패킷은 단지 데이터를 담고있는 패킷
        // 이걸 직접적으로 전송하긴 않을 거임
        // 따라서 얘를 핸들링 할 필요는 없음

        public ushort playerID;
        public ushort characterID;
        public ushort ohterID;
        public float x;
        public float y;
        public float z;

        public float xAngle;
        public float yAngle;
        public float zAngle;

        public float xAnim;
        public float yAnim;
        public float zAnim;

        public int damged;
        public int hp;

        public PlayerPacket()
        {

        }

        public PlayerPacket(ushort playerID)
        {
            this.playerID = playerID;
        }

        public PlayerPacket(ushort playerID, ushort characterID, float x, float y, float z, float xAngle, float yAngle, float zAngle, float xAnim, float yAnim, float zAnim)
        {
            this.playerID = playerID;
            this.characterID = characterID;
            this.x = x;
            this.y = y;
            this.z = z;

            this.xAngle = xAngle;
            this.yAngle = yAngle;
            this.zAngle = zAngle;

            this.xAnim = xAnim;
            this.yAnim = yAnim;
            this.zAnim = zAnim;
        }

        public PlayerPacket(ushort playerID, ushort characterID)
        {
            this.playerID = playerID;
            this.characterID = characterID;
        }

        public PlayerPacket(ushort playerID, float x, float y, float z, float xAngle, float yAngle, float zAngle, float xAnim, float yAnim, float zAnim)
        {
            this.playerID = playerID;
            this.x = x;
            this.y = y;
            this.z = z;

            this.xAngle = xAngle;
            this.yAngle = yAngle;
            this.zAngle = zAngle;

            this.xAnim = xAnim;
            this.yAnim = yAnim;
            this.zAnim = zAnim;
        }

        public PlayerPacket(ushort playerID, ushort ohterID, int damged, float xHit, float yHit, float zHit)
        {
            this.playerID = playerID;
            this.ohterID = ohterID;
            this.damged = damged;

            this.x = xHit;
            this.y = yHit;
            this.z = zHit;
        }

        public override ushort Deserialize(ArraySegment<byte> buffer, int offset)
        {
            ushort process = 0;
            process += PacketUtility.ReadUShortData(buffer, offset + process, out this.playerID);
            process += PacketUtility.ReadUShortData(buffer, offset + process, out this.ohterID);
            process += PacketUtility.ReadFloatData(buffer, offset + process, out this.x);
            process += PacketUtility.ReadFloatData(buffer, offset + process, out this.y);
            process += PacketUtility.ReadFloatData(buffer, offset + process, out this.z);
            process += PacketUtility.ReadFloatData(buffer, offset + process, out this.xAngle);
            process += PacketUtility.ReadFloatData(buffer, offset + process, out this.yAngle);
            process += PacketUtility.ReadFloatData(buffer, offset + process, out this.zAngle);
            process += PacketUtility.ReadFloatData(buffer, offset + process, out this.xAnim);
            process += PacketUtility.ReadFloatData(buffer, offset + process, out this.yAnim);
            process += PacketUtility.ReadFloatData(buffer, offset + process, out this.zAnim);
            process += PacketUtility.ReadIntData(buffer, offset + process, out this.damged);
            process += PacketUtility.ReadIntData(buffer, offset + process, out this.hp);

            return process;
        }

        public override ushort Serialize(ArraySegment<byte> buffer, int offset)
        {
            ushort process = 0;
            process += PacketUtility.AppendUShortData(this.playerID, buffer, offset + process);
            process += PacketUtility.AppendUShortData(this.ohterID, buffer, offset + process);
            process += PacketUtility.AppendFloatData(this.x, buffer, offset + process);
            process += PacketUtility.AppendFloatData(this.y, buffer, offset + process);
            process += PacketUtility.AppendFloatData(this.z, buffer, offset + process);
            process += PacketUtility.AppendFloatData(this.xAngle, buffer, offset + process);
            process += PacketUtility.AppendFloatData(this.yAngle, buffer, offset + process);
            process += PacketUtility.AppendFloatData(this.zAngle, buffer, offset + process);
            process += PacketUtility.AppendFloatData(this.xAnim, buffer, offset + process);
            process += PacketUtility.AppendFloatData(this.yAnim, buffer, offset + process);
            process += PacketUtility.AppendFloatData(this.zAnim, buffer, offset + process);
            process += PacketUtility.AppendIntData(this.damged, buffer, offset + process);
            process += PacketUtility.AppendIntData(this.hp, buffer, offset + process);

            return process;
        }
    }
}