using System;

namespace TcpUdp.Client
{
    public class ClientInit
    {
        private const int serverPort = 9999;

        private const string serverName = "localhost";

        private const int messageSize = 65535;

        public static void Main()
        {
            var clients = 10;

            while (clients > 0)
            {
                new FileMessageClient(serverName, serverPort, messageSize).SendData();

                clients--;
            }

            Console.Read();
        }
    }
}
