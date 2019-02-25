using TcpUdp.Core.Utilities;

namespace TcpUdp.Server
{
    public class UDPServer : BaseServer
    {
        public UDPServer(string serverName, int serverPort) : base(serverName, serverPort)
        {
        }

        public override ProtocolTypeEnum Type => ProtocolTypeEnum.UDP;

        public override void Start()
        {

        }
    }
}