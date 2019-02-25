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
        private static int numberOfClients = 1;

        public static void Main()
        {
            var fileMessageSender = new FileMessagesesSender(serverName, serverPort, messageSize);
            var fileMessages = new FileMessageProvider().GetFileMessages().ToList();

            while (numberOfClients > 0)
            {
                if (fileMessages.Count > 1)
                {
                    fileMessageSender.SendBatched(fileMessages, ProtocolTypeEnum.TCP);

                    Console.WriteLine($"Total Transfer time (s):{fileMessageSender.TotalTransferTime.TotalSeconds}");
                }
                else
                {
                    foreach (var fileMessage in fileMessages)
                    {
                        fileMessageSender.Send(fileMessage, ProtocolTypeEnum.TCP);

                        Console.WriteLine(
                            $"Transfer time for message (s):{fileMessageSender.TransferTimeForMessage.TotalSeconds}");
                    }
                }

                Console.WriteLine($"Total Transfer time (s):{fileMessageSender.TotalTransferTime.TotalSeconds}");

                numberOfClients--;
            }

            Console.Read();
        }
    }
}
