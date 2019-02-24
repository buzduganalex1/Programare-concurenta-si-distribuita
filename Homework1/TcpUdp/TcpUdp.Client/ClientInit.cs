using System;
using System.Threading.Tasks;

namespace TcpUdp.Client
{
    public class ClientInit
    {
        private const int serverPort = 9999;

        private const string serverName = "localhost";

        private const int messageSize = 65535;

        public static void Main()
        {
            var numberOfClients = 3;

            while (numberOfClients > 0)
            {
                new Task(() => new FileMessageClient(serverName, serverPort, messageSize).SendData()).Start();

                numberOfClients--;
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}
