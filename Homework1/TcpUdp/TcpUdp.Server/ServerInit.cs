using System;
using System.Net;
using System.Net.Sockets;

namespace TcpUdp.Server
{
    public class ServerInit
    {
        public static void Main(string[] args)
        {
            var server = new TcpListener(IPAddress.Any, 9999);

            var messageReceiver = new TcpServerMessageReceiver();

            var clientCount = 0;

            server.Start();

            Console.WriteLine("Server started\nWaiting for clients...");

            while (true)
            {
                try
                {
                    var client = server.AcceptTcpClientAsync().Result;

                    var clientIp = ((IPEndPoint) client.Client.RemoteEndPoint).Address.ToString();

                    var clientId = $"Client{clientCount}";

                    Console.WriteLine($"Client {clientId}-{clientIp} connected.");
                    
                    clientCount++;

                    messageReceiver.ProcessMessageFromClient(client, clientId).Start();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }            
            }
        }

       

    }
}

