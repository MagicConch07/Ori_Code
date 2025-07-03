using Packets;
using HelloNetwork;

namespace Server
{
    public class PacketHandler
    {
        public static void C_LogInPacket(Session session, Packet packet) // Client로부터 ChatPacket을 받았을 때
        {
            C_LogInPacket loginPacket = packet as C_LogInPacket;
            ClientSession clientSession = session as ClientSession;

            Player player = new Player(clientSession, Program.playerCount, loginPacket.nickname, 0, 0, 0);
            Program.players.Add(player.playerID, player);

            Program.playerCount++;

            S_LogInPacket sendPacket = new S_LogInPacket();
            sendPacket.playerID = player.playerID;

            clientSession.Send(sendPacket.Serialize());
        }

        public static void C_RoomEnterPacket(Session session, Packet packet)
        {
            C_RoomEnterPacket enterPacket = packet as C_RoomEnterPacket;

            GameRoom room = Program.room;
            room.AddJob(() => room.AddPlayer(enterPacket.playerID));

            S_RoomEnterPacket resPacket = new S_RoomEnterPacket();
            resPacket.playerList = new List<PlayerPacket>();
            room.players.ForEach(p =>
            {
                if (p == enterPacket.playerID)
                    return;

                Console.WriteLine($"현재 플레이어 아이디 : {p}");

                Player player = Program.players[p];
                PlayerPacket playerPacket = new PlayerPacket(player.playerID, player.characterID);
                resPacket.playerList.Add(playerPacket);
            });
            session.Send(resPacket.Serialize());

            Player player = Program.players[enterPacket.playerID];
            S_PlayerJoinPacket broadcastPacket = new S_PlayerJoinPacket();
            broadcastPacket.playerData = new PlayerPacket(player.playerID, player.characterID, player.x, player.y, player.z, player.xAngle, player.yAngle, player.zAngle, player.xAnim, player.yAnim, player.zAnim);

            room.AddJob(() => room.Broadcast(broadcastPacket, player.playerID));
        }

        public static void C_SelectPacket(Session session, Packet packet)
        {
            C_SelectPacket selectPacket = packet as C_SelectPacket;
            GameRoom room = Program.room;

            Player player = room.GetPlayer(selectPacket.playerData.playerID);
            if (player == null)
                return;

            player.playerID = selectPacket.playerData.playerID;
            player.characterID = selectPacket.playerData.characterID;

            S_SelectPacket selPacket = new S_SelectPacket();
            selPacket.playerData = new PlayerPacket(player.playerID, player.characterID);
            selPacket.characterID = player.characterID;

            room.Broadcast(selPacket, player.playerID);
        }

        public static void C_MovePacket(Session session, Packet packet)
        {
            C_MovePacket movePacket = packet as C_MovePacket;
            GameRoom room = Program.room;

            Player player = room.GetPlayer(movePacket.playerData.playerID);
            if (player == null)
                return;

            player.x = movePacket.playerData.x;
            player.y = movePacket.playerData.y;
            player.z = movePacket.playerData.z;
            player.xAngle = movePacket.playerData.xAngle;
            player.yAngle = movePacket.playerData.yAngle;
            player.zAngle = movePacket.playerData.zAngle;
            player.xAnim = movePacket.playerData.xAnim;
            player.yAnim = movePacket.playerData.yAnim;
            player.zAnim = movePacket.playerData.zAnim;

            S_MovePacket resPacket = new S_MovePacket();
            resPacket.playerData = new PlayerPacket(player.playerID, player.x, player.y, player.z, player.xAngle, player.yAngle, player.zAngle, player.xAnim, player.yAnim, player.zAnim); ;

            room.Broadcast(resPacket, movePacket.playerData.playerID);
        }

        public static void C_HitPacket(Session session, Packet packet)
        {
            C_HitPacket hitPacket = packet as C_HitPacket;
            GameRoom room = Program.room;

            Player player = room.GetPlayer(hitPacket.playerData.playerID);
            if (player == null) return;

            player.otherID = hitPacket.playerData.ohterID;
            player.damged = hitPacket.playerData.damged;
            player.x = hitPacket.playerData.x;
            player.y = hitPacket.playerData.y;
            player.z = hitPacket.playerData.z;

            S_HitPacket resPacket = new S_HitPacket();
            resPacket.playerData = new PlayerPacket(player.playerID, player.otherID, player.damged, player.x, player.y, player.z);

            room.Broadcast(resPacket, hitPacket.playerData.playerID);
        }

        public static void C_AttackPacket(Session session, Packet packet)
        {
            C_AttackPacket attackPacket = packet as C_AttackPacket;
            GameRoom room = Program.room;

            Player player = room.GetPlayer(attackPacket.playerData.playerID);
            if (player == null) return;

            player.damged = attackPacket.damaged;

            S_AttackPacket resPacket = new S_AttackPacket();
            resPacket.playerData = new PlayerPacket(player.playerID);
            resPacket.damaged = player.damged;

            room.Broadcast(resPacket, attackPacket.playerData.playerID);
        }

    }
}