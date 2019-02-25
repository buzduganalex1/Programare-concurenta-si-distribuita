using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TcpUdp.Core.Models;
using TcpUdp.Core.Utilities;

namespace TcpUdp.Core.Senders
{
    public class UDPFileMessageSender : BaseFileMessageSender
    {
        public UDPFileMessageSender(string serverName, int serverPort, int maxMessageSize) : base(serverName, serverPort, maxMessageSize)
        {
        }

        public override ProtocolTypeEnum Type => ProtocolTypeEnum.UDP;

        public override void Send(FileMessage fileMessage)
        {
            var udpClient = new UdpClient();

            udpClient.Connect(this.ServerName, this.ServerPort);

            try
            {
                var message = fileMessage.ToByteArray();

                udpClient.Send(message, message.Length);

                this.Results.BytesSent = message.Length;
                this.Results.NumberOfMessages = 1;

                var RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, ServerPort);
                var receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                var returnData = Encoding.ASCII.GetString(receiveBytes);
                                
                Console.WriteLine(this.GetResultsMessage);
                Console.WriteLine($"Message from server: {returnData}");

                udpClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public override void SendBatched(IEnumerable<FileMessage> fileMessages)
        {
            var udpClient = new UdpClient();

            udpClient.Connect(this.ServerName, this.ServerPort);

            foreach (var fileMessage in fileMessages)
            {
                try
                {
                    var message = fileMessage.ToByteArray();

                    udpClient.Send(message, message.Length);

                    this.Results.BytesSent = message.Length;
                    this.Results.NumberOfMessages = 1;

                    var RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, ServerPort);
                    var receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                    var returnData = Encoding.ASCII.GetString(receiveBytes);

                    Console.WriteLine(this.GetResultsMessage);
                    Console.WriteLine($"Message from server: {returnData}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            
            udpClient.Close();
        }
    }
}