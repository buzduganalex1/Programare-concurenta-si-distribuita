using TcpUdp.Core;
using TcpUdp.Core.Models;

namespace TcpUdp.Server
{
    public interface IFileMessageHandler
    {
        void Handle(string clientId, FileMessage message);
    }
}