using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using TcpUdp.Core;
using TcpUdp.Core.Models;

namespace TcpUdp.Server
{
    public class TcpServerMessageReceiver
    {
        public Task ProcessMessageFromClient(TcpClient client, string clientId)
        {
            var task = new Task(() =>
            {
                var stream = client.GetStream();

                while (client.Connected)
                {
                    if (stream.CanRead  && stream.DataAvailable)
                    {
                        Console.WriteLine("Receiving messages...");

                        while (stream.DataAvailable)
                        {
                            var message = new List<byte>();

                            var readBuffer = new byte[65535];

                            var messageNumberOfBytes = new byte[4];

                            var messageNumber = 0;

                            var size = stream.ReadAsync(messageNumberOfBytes, 0, 4).Result;

                            var messageNumberOfBytesInt = BitConverter.ToInt32(messageNumberOfBytes, 0);

                            while (message.Count < messageNumberOfBytesInt)
                            {
                                var messageResult = stream.ReadAsync(readBuffer, 0, readBuffer.Length).Result;

                                message.AddRange(readBuffer);

                                messageNumber++;
                            }

                            if (message.ToArray().ByteArrayToObject() is FileMessage fileMessage)
                            {
                                var task1 = new Task(() =>
                                {
                                    new FileMessageHandler().Handle(Guid.NewGuid().ToString(), fileMessage);
                                });

                                task1.Start();
                            }
                            else
                            {
                                Console.WriteLine("Not Expected type.");
                            }

                            Console.WriteLine($"Number of packages received: {messageNumber}");
                            Console.WriteLine($"Number of bytes read: {message.Count}");
                        }

                        client.Dispose();
                    }
                }
            });

            return task;
        }
    }
}