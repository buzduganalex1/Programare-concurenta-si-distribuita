using System.Collections.Generic;
using TcpUdp.Core.Models;

namespace TcpUdp.Core.Interfaces
{
    public interface IFileMessageProvider
    {
        IEnumerable<FileMessage> GetFileMessages();
    }
}