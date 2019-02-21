using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TcpUdp.Core;

namespace TcpUdp.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var protocol = "TCP";
            
            var server = new TcpListener(IPAddress.Any, 9999);

            server.Start();

            Console.WriteLine("Hello");

            while (true)
            {
                try
                {
                    var client = server.AcceptTcpClient();

                    var networkStream = client.GetStream();

                    var myReadBuffer = new byte[1024];

                    var numberOfMessages = 0;
                    
                    Console.WriteLine(client.Connected);

                    while (client.Connected)
                    {
                        if (networkStream.CanRead)
                        {
                            if (networkStream.DataAvailable)
                            {
                                var result = new List<byte>();

                                while (networkStream.Read(myReadBuffer, 0, myReadBuffer.Length) > 0)
                                {
                                    result.AddRange(myReadBuffer);

                                    numberOfMessages++;
                                }
                                
                                var file = ByteArrayToObject(result.ToArray()) as FileMessage;

                                if (file != null)
                                {
                                    var path = @"..\..\..\Resources\" + file.Name + "." + file.Format;

                                    File.WriteAllBytes(path, file.Data);

                                    Console.WriteLine($"Number of packages: {numberOfMessages}");
                                    Console.WriteLine($"Number of bytes read: {result.Count}");
                                    Console.WriteLine($"{file.Name} + {file.Format}");
                                }

                                Console.WriteLine("test");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }            
            }
        }

        private static Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }
    }
}

