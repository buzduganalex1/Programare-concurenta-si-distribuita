using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using TcpUdp.Core;
using TcpUdp.Core.Models;

namespace TcpUdp.Client
{
    public class FileMessageClient
    {
        private readonly TcpClient tcpClient;
        private readonly int maxMessageSize;
        
        public FileMessageClient(string serverName, int serverPort, int maxMessageSize)
        {
            this.maxMessageSize = maxMessageSize;

            this.tcpClient = new TcpClient(serverName, serverPort);
        }

        public void SendData()
        {
            try
            {
                Console.WriteLine($"Client{Guid.NewGuid().ToString()}");

                var fileMessages = this.GetFileMessages();

                if (fileMessages.Any())
                {
                    var stream = this.tcpClient.GetStream();

                    foreach (var fileMessage in fileMessages)
                    {
                        var fileMessageByteArray = fileMessage.ToByteArray();

                        var packages = fileMessageByteArray.Split(maxMessageSize);
                        
                        var messageSize = BitConverter.GetBytes(fileMessageByteArray.Length);

                        stream.Write(messageSize, 0, messageSize.Length);

                        foreach (var package in packages)
                        {
                            stream.Write(package, 0, package.Length);
                            
                            Console.WriteLine($"Number of bytes sent: {package.Length}");
                        }
                    }
                }

                tcpClient.GetStream().Close();
                tcpClient.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        private IEnumerable<FileMessage> GetFileMessages()
        {
            var fileMessages = new List<FileMessage>();

            var path = @"..\..\..\Resources\";

            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);

                var format = fileName.Split('.')[1];

                var name = fileName.Split('.')[0];

                fileMessages.Add(new FileMessage
                {
                    Name = name,
                    Format = format,
                    Data = File.ReadAllBytesAsync($"{path}{name}.{format}").Result
                });
            }

            return fileMessages;
        }
    }
}