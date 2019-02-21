using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using TcpUdp.Core;

namespace TcpUdp.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            const int port = 9999;

            try
            {
                var client = new TcpClient("localhost", port);

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
                    
                    var message = ObjectToByteArray(fileMessage);

                    var sw = new Stopwatch();

                    var transmissionTime = DateTime.Now;

                    var bytesSent = message.Length;

                    var sentMessages = 1;

                    sw.Start();

                    stream.Write(message, 0, message.Length);

                    sw.Stop();

                    Console.WriteLine(
                        $"TransmissionTime: {transmissionTime} \nNumber of messages sent: {sentMessages} \nNumber of bytes sent: {bytesSent}");

                }
                
                stream.Close();

                client.Close();
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

    }
}
