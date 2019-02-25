using System.Collections.Generic;
using System.Text;
using TcpUdp.Core.Interfaces;
using TcpUdp.Core.Models;

namespace TcpUdp.Core.Database
{
    public class MockMessageProvider : IFileMessageProvider
    {
        public IEnumerable<FileMessage> GetFileMessages()
        {
            return new List<FileMessage>()
            {
                new FileMessage
                {
                    Name = "Test",
                    Format = "json",
                    Data = Encoding.UTF8.GetBytes("This is a test message")
                }
            };
        }
    }
}