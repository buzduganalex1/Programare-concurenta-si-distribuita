using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TcpUdp.Core.Models;
using TcpUdp.Core.Utilities;

namespace TcpUdp.Server
{
    public class UDPServer : BaseServer
    {
        public UDPServer(string serverName, int serverPort) : base(serverName, serverPort)
        {
        }

        public override ProtocolTypeEnum Type => ProtocolTypeEnum.UDP;

        public override void Start()
        {

            Console.WriteLine("UDP Server started\nWaiting for clients...");

            var client = new IPEndPoint(IPAddress.Any, this.ServerPort);

            var newsock = new UdpClient(client);

            while (true)
            {
                var data = new byte[102400];

                var sender = new IPEndPoint(IPAddress.Any, 0);

                data = newsock.Receive(ref sender);

                var test = data.ByteArrayToObject();

                var file = test as FileMessage;

                new FileMessageHandler().Handle(Guid.NewGuid().ToString(), file);

                data = Encoding.ASCII.GetBytes("File received.");

                newsock.Send(data, data.Length, sender);
            }        
        }
    }
}