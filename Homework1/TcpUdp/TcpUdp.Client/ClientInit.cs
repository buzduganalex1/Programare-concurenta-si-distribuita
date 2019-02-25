using System;
using System.Linq;
using TcpUdp.Core.Database;
using TcpUdp.Core.Senders;
using TcpUdp.Core.Utilities;

namespace TcpUdp.Client
{
    public class ClientInit
    {
        private const int messageSize = 65535;

        public static void Main()
        {
            var fileMessageSender = new FileMessagesesSender(ConnectionCredentials.ServerName, ConnectionCredentials.UDPServerPort, messageSize);

            var fileMessages = new FileMessageProvider().GetFileMessages().ToList();
            
            fileMessageSender.SendBatched(fileMessages, ProtocolTypeEnum.UDP);
            
            Console.WriteLine($"\nTotal Transfer time (s):{fileMessageSender.TotalTransferTime.TotalSeconds}\n");
            
            Console.Read();
        }
    }
}
