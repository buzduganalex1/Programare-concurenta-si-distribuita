using System;
using System.Linq;
using TcpUdp.Core.Database;
using TcpUdp.Core.Senders;
using TcpUdp.Core.Utilities;

namespace TcpUdp.Client
{
    public class ClientInit
    {
        private const string serverName = "localhost";
        private const int tcpServerPort = 9999;
        private const int udpServerPort = 8888;
        private const int messageSize = 65535;

        public static void Main()
        {
            var fileMessageSender = new FileMessagesesSender(serverName, tcpServerPort, messageSize);
            var fileMessages = new FileMessageProvider().GetFileMessages().ToList();
            
            fileMessageSender.SendBatched(fileMessages, ProtocolTypeEnum.TCP);
            
            Console.WriteLine($"\nTotal Transfer time (s):{fileMessageSender.TotalTransferTime.TotalSeconds}\n");
            
            Console.Read();
        }
    }
}
