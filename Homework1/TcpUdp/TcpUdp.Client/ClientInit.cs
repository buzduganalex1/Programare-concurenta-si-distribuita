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
        private const int serverPort = 9999;
        private const int messageSize = 65535;

        public static void Main()
        {
            var fileMessageSender = new FileMessagesesSender(serverName, serverPort, messageSize);
            var fileMessages = new MockMessageProvider().GetFileMessages().ToList();

            if (fileMessages.Count == 1)
            {
                fileMessageSender.Send(fileMessages.First(), ProtocolTypeEnum.UDP);

                Console.WriteLine($"Total Transfer time (s):{fileMessageSender.TotalTransferTime.TotalSeconds}");
            }

            Console.WriteLine($"Total Transfer time (s):{fileMessageSender.TotalTransferTime.TotalSeconds}");
            
            Console.Read();
        }
    }
}
