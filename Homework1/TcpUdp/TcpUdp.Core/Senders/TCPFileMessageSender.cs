using System;
using System.Collections.Generic;
using System.Net.Sockets;
using TcpUdp.Core.Models;
using TcpUdp.Core.Utilities;

namespace TcpUdp.Core.Senders
{
    public class TCPFileMessageSender : BaseFileMessageSender
    {

        public TCPFileMessageSender(string serverName, int serverPort, int maxMessageSize) : base(serverName,
            serverPort, maxMessageSize){}

        public override ProtocolTypeEnum Type => ProtocolTypeEnum.TCP;

        public override void Send(FileMessage fileMessage)
        {
            var tcpClient = new TcpClient(this.ServerName, this.ServerPort);
            var stream = tcpClient.GetStream();
            var fileMessageByteArray = fileMessage.ToByteArray();
            var packages = fileMessageByteArray.Split(this.MaxMessageSize);
            var messageSize = BitConverter.GetBytes(fileMessageByteArray.Length);

            stream.Write(messageSize, 0, messageSize.Length);

            foreach (var package in packages)
            {
                stream.Write(package, 0, package.Length);

                this.Results.NumberOfMessages++;

                this.Results.BytesSent += package.Length;
            }
            
            stream.Close();
            tcpClient.Close();
            
            Console.WriteLine(this.GetResultsMessage);
        }

        public override void SendBatched(IEnumerable<FileMessage> fileMessages)
        {
            var tcpClient = new TcpClient(this.ServerName, this.ServerPort);
            var stream = tcpClient.GetStream();

            foreach (var fileMessage in fileMessages)
            {
                var fileMessageByteArray = fileMessage.ToByteArray();
                var packages = fileMessageByteArray.Split(this.MaxMessageSize);
                var messageSize = BitConverter.GetBytes(fileMessageByteArray.Length);

                stream.Write(messageSize, 0, messageSize.Length);

                foreach (var package in packages)
                {
                    stream.Write(package, 0, package.Length);

                    this.Results.NumberOfMessages++;

                    this.Results.BytesSent += package.Length;
                }

                System.Threading.Thread.Sleep(50);
            }

            stream.Close();
            tcpClient.Close();

            Console.WriteLine(this.GetResultsMessage);
        }
    }
}
