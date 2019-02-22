using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using TcpUdp.Core;

namespace TcpUdp.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            const int port = 9999;

            var maxMessageSize = 65535;
            
            try
            {
                using (var client = new TcpClient("localhost", port))
                {
                    var stream = client.GetStream();

                    var path = @"..\..\..\Resources\";

                    var files = Directory.GetFiles(path);

                    foreach (var file in files)
                    {
                        var fileName = Path.GetFileName(file);

                        var format = fileName.Split('.')[1];

                        var name = fileName.Split('.')[0];

                        var fileMessage = new FileMessage
                        {
                            Name = name,
                            Format = format,
                            Data = File.ReadAllBytesAsync($"{path}{name}.{format}").Result
                        };

                        var arrayOfBytesObject = ObjectToByteArray(fileMessage);

                        var test2 = Split(arrayOfBytesObject, maxMessageSize);

                        var sizeOfMessage = arrayOfBytesObject.Length;

                        var count = new byte[4];

                        count = BitConverter.GetBytes(sizeOfMessage);

                        var sw = new Stopwatch();

                        var transmissionTime = DateTime.Now;

                        var bytesSent = arrayOfBytesObject.Length;

                        stream.Write(count, 0, count.Length);

                        foreach (var testMessage in test2)
                        {
                            sw.Start();

                            Console.WriteLine("Start");

                            stream.Write(testMessage, 0, testMessage.Length);

                            System.Threading.Thread.Sleep(50);

                            sw.Stop();

                            Console.WriteLine(
                                $"TransmissionTime: {transmissionTime} \nNumber of bytes sent: {bytesSent}");
                        }

                        var buffer = new byte[1024];

                        Console.WriteLine(Encoding.UTF8.GetString(buffer));
                    }

                    client.GetStream().Close();

                    client.Close();
                }    
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }

        private static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        public static IEnumerable<byte[]> Split (byte[] value, int bufferLength)
        {
            int countOfArray = value.Length / bufferLength;
            if (value.Length % bufferLength > 0)
                countOfArray++;
            for (int i = 0; i < countOfArray; i++)
            {
                yield return value.Skip(i * bufferLength).Take(bufferLength).ToArray();

            }
        }

    }
}
