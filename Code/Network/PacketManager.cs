using System;
using System.Collections;
using System.Collections.Generic;
using HelloNetwork;
using UnityEngine;
using Packets;

public class PacketManager
{
    private static PacketManager instance;
    public static PacketManager Instance
    {
        get
        {
            instance ??= new PacketManager();
            return instance;
        }
    }

    private Dictionary<ushort, Func<ArraySegment<byte>, Packet>> packetFactories =
        new Dictionary<ushort, Func<ArraySegment<byte>, Packet>>();
    private Dictionary<ushort, Action<Session, Packet>> packetHandlers =
        new Dictionary<ushort, Action<Session, Packet>>();

    public PacketManager()
    {
        packetFactories.Clear();
        packetHandlers.Clear();

        RegisterHandler();
    }

    private void RegisterHandler()
    {
        packetFactories.Add((ushort)PacketID.S_LogInPacket, PacketUtility.CreatePacket<S_LogInPacket>);
        packetHandlers.Add((ushort)PacketID.S_LogInPacket, PacketHandler.S_LogInPacket);

        packetFactories.Add((ushort)PacketID.S_MovePacket, PacketUtility.CreatePacket<S_MovePacket>);
        packetHandlers.Add((ushort)PacketID.S_MovePacket, PacketHandler.S_MovePacket);

        packetFactories.Add((ushort)PacketID.S_PlayerJoinPacket, PacketUtility.CreatePacket<S_PlayerJoinPacket>);
        packetHandlers.Add((ushort)PacketID.S_PlayerJoinPacket, PacketHandler.S_PlayerJoinPacket);

        packetFactories.Add((ushort)PacketID.S_RoomEnterPacket, PacketUtility.CreatePacket<S_RoomEnterPacket>);
        packetHandlers.Add((ushort)PacketID.S_RoomEnterPacket, PacketHandler.S_RoomEnterPacket);

        packetFactories.Add((ushort)PacketID.S_AttackPacket, PacketUtility.CreatePacket<S_AttackPacket>);
        packetHandlers.Add((ushort)PacketID.S_AttackPacket, PacketHandler.S_AttackPacket);

        packetFactories.Add((ushort)PacketID.S_HitPacket, PacketUtility.CreatePacket<S_HitPacket>);
        packetHandlers.Add((ushort)PacketID.S_HitPacket, PacketHandler.S_HitPacket);
    }

    public Packet CreatePacket(ArraySegment<byte> buffer)
    {
        ushort packetID = PacketUtility.ReadPacketID(buffer);

        if (packetFactories.ContainsKey(packetID))
            return packetFactories[packetID]?.Invoke(buffer);
        else
            return null;
    }

    public void HandlePacket(Session session, Packet packet)
    {
        if (packet != null)
            if (packetHandlers.ContainsKey(packet.ID))
                packetHandlers[packet.ID]?.Invoke(session, packet);
    }
}