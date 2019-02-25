using System;
using System.IO;
using TcpUdp.Core.Models;
using TcpUdp.Server.Interfaces;

namespace TcpUdp.Server
{
    public class FileMessageHandler : IFileMessageHandler
    {
        public void Handle(string clientId, FileMessage message)
        {
            try
            {
                var directory = @"..\..\..\Resources\";

                var path = directory + $"/{message.Name}.{message.Format}";

                File.WriteAllBytes(path, message.Data);

                Console.WriteLine($"{message.Name}.{message.Format} from {clientId} saved.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}