using TcpUdp.Core.Utilities;

namespace TcpUdp.Server.Interfaces
{
    public interface IServerFactory
    {
        void Start(ProtocolTypeEnum type);
    }
}