using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using TcpUdp.Core.Models;
using TcpUdp.Core.Utilities;

namespace TcpUdp.Server
{
    public class TCPServer : BaseServer
    {
        public TcpListener server { get; private set; }

        public TCPServer(string serverName, int serverPort) : base(serverName, serverPort)
        {
            this.server = new TcpListener(IPAddress.Parse(serverName), serverPort);
        }

        public override ProtocolTypeEnum Type => ProtocolTypeEnum.TCP;

        public override void Start()
        {
            server.Start();

            Console.WriteLine("TCP Server started\nWaiting for clients...");

            while (true)
            {
                try
                {
                    var client = server.AcceptTcpClientAsync().Result;

                    client.NoDelay = true;
                    
                    var stream = client.GetStream();

                    while (client.Connected)
                    {

                        if (stream.CanRead && stream.DataAvailable)
                        {
                            Console.WriteLine("Receiving messages...");

                            while (stream.DataAvailable)
                            {
                                try
                                {
                                    var message = new List<byte>();

                                    var readBuffer = new byte[65535];

                                    var messageNumberOfBytes = new byte[4];

                                    var messageNumber = 0;

                                    var size = stream.ReadAsync(messageNumberOfBytes, 0, 4).Result;

                                    var messageNumberOfBytesInt = BitConverter.ToInt32(messageNumberOfBytes, 0);
                                    
                                    while (message.Count < messageNumberOfBytesInt)
                                    {
                                        var messageResult = stream.Read(readBuffer, 0, readBuffer.Length);

                                        message.AddRange(readBuffer);

                                        messageNumber++;
                                    }

                                    var result = message.ToArray().ByteArrayToObject();

                                    if (result != null && result is FileMessage fileMessage)
                                    {
                                        new Task(() =>
                                        {
                                            new FileMessageHandler().Handle(Guid.NewGuid().ToString(),
                                                fileMessage);
                                        }).Start();
                                    }

                                    Console.WriteLine($"Number of packages received: {messageNumber}");
                                    Console.WriteLine($"Number of bytes read: {message.Count}");
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}