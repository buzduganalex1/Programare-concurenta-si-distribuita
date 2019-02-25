using System.Collections.Generic;
using TcpUdp.Core.Models;

namespace TcpUdp.Core.Interfaces
{
    public interface IFileMessageSender
    {
        void Send(FileMessage fileMessage);

        void SendBatched(IEnumerable<FileMessage> fileMessage);
    }
}