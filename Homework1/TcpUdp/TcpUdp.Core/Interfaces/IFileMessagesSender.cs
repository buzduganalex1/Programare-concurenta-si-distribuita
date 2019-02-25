using System.Collections.Generic;
using TcpUdp.Core.Models;
using TcpUdp.Core.Utilities;

namespace TcpUdp.Core.Interfaces
{
    public interface IFileMessagesSender
    {
        void Send(FileMessage fileMessage, ProtocolTypeEnum protocolType);

        void SendBatched(IEnumerable<FileMessage> fileMessages, ProtocolTypeEnum protocolType);
    }
}