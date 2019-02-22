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
                    var client = server.AcceptTcpClientAsync().Result;

                    using (var stream = client.GetStream())
                    {
                        Console.WriteLine(client.Connected);

                        while (client.Connected)
                        {
                            if (stream.CanRead)
                            {
                                if (stream.DataAvailable)
                                {
                                    Console.WriteLine("Start");

                                    while (stream.DataAvailable)
                                    {
                                        var result = new List<byte>();

                                        var myReadBuffer = new byte[65535];

                                        var count = new byte[4];

                                        var numberOfMessages = 0;

                                        var size = stream.ReadAsync(count, 0, 4).Result;

                                        int i = BitConverter.ToInt32(count, 0);

                                        while (result.Count < i)
                                        {
                                            var test2 = stream.ReadAsync(myReadBuffer, 0, myReadBuffer.Length).Result;

                                            result.AddRange(myReadBuffer);

                                            numberOfMessages++;
                                        }

                                        try
                                        {
                                            var file = ByteArrayToObject(result.ToArray()) as FileMessage;

                                            if (file != null)
                                            {
                                                var path = @"..\..\..\Resources\" + file.Name + "." + file.Format;

                                                File.WriteAllBytes(path, file.Data);

                                                Console.WriteLine($"Number of packages: {numberOfMessages}");
                                                Console.WriteLine($"Number of bytes read: {result.Count}");
                                                Console.WriteLine($"{file.Name} + {file.Format}");
                                            }

                                            Console.WriteLine("Done");
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e);
                                        }
                                    }
                                }
                                else
                                {
                                    if (client.Client.Poll(0, SelectMode.SelectRead))
                                    {
                                        System.Threading.Thread.Sleep(50);
                                    }

                                    if (client.Client.Poll(0, SelectMode.SelectRead))
                                    {
                                        client.Dispose();
                                    }
                                }
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

