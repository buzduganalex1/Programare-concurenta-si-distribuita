﻿using System.Collections.Generic;
using System.Net;
using TcpUdp.Core.Utilities;
using TcpUdp.Server.Interfaces;

namespace TcpUdp.Server
{
    public class ServerInitiator : IServerFactory
    {
        private readonly IEnumerable<BaseServer> servers;

        public ServerInitiator()
        {
            this.servers = new List<BaseServer>
            {
                new TCPServer(IPAddress.Any.ToString(), ConnectionCredentials.TCPServerPort),
                new UDPServer(IPAddress.Any.ToString(), ConnectionCredentials.UDPServerPort)
            };
        }

        public void Start(ProtocolTypeEnum type)
        {
            foreach (var server in servers)
            {
                if (server.Type == type)
                {
                    server.Start();
                }
            }
        }
    }
}