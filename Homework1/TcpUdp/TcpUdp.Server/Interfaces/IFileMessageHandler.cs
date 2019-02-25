using TcpUdp.Core.Models;

namespace TcpUdp.Server.Interfaces
{
    public interface IFileMessageHandler
    {
        void Handle(string clientId, FileMessage message);
    }
}