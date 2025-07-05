using System.Net.Sockets;
using System.Net;
using HelloNetwork;

namespace Server
{
    internal class Program
    {
        public static GameRoom room = new GameRoom();
        public static Dictionary<ushort, Player> players = new Dictionary<ushort, Player>();
        public static ushort playerCount = 0;

        static void Main(string[] args)
        {
            string host = Dns.GetHostName();
            IPHostEntry iphost = Dns.GetHostEntry(host);
            IPAddress iPAddress = iphost.AddressList[1];
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("116.47.243.28"), 8081);

            Listener listener = new Listener(endPoint);
            if (listener.Listen(10))
                listener.StartAccept(OnAccepted);

            FlushLoop(10);
        }

        private static void FlushLoop(int delay)
        {
            int lastFlushTime = Environment.TickCount;

            while (true)
            {
                int currentTime = Environment.TickCount;
                if (currentTime - lastFlushTime > delay)
                {
                    room.AddJob(() => room.FlushPacketQueue());
                    lastFlushTime = currentTime;
                }
            }
        }

        private static void OnAccepted(Socket socket)
        {
            ClientSession session = new ClientSession();
            session.Open(socket);
            session.OnConnected(socket.RemoteEndPoint);
        }
    }
}