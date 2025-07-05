using System;

namespace Server
{
    public class Player
    {
        public ushort playerID;
        public ushort otherID;
        public ushort characterID;
        public string nickname;
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

        public ClientSession session;

        public Player(ClientSession session, ushort playerID, ushort characterID, string nickname, float x, float y, float z)
        {
            this.session = session;
            this.playerID = playerID;
            this.characterID = characterID;
            this.nickname = nickname;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Player(ClientSession session, ushort playerID, string nickname, float x, float y, float z)
        {
            this.session = session;
            this.playerID = playerID;
            this.nickname = nickname;
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Player(ClientSession session, ushort playerID, ushort otherID, string nickname, int damged, int hp, float xHit, float yHit, float zHit)
        {
            this.session = session;
            this.playerID = playerID;
            this.otherID = otherID;
            this.nickname = nickname;
            this.damged = damged;
            this.hp = hp;
            this.x = xHit;
            this.y = yHit;
            this.z = zHit;
        }
    }
}