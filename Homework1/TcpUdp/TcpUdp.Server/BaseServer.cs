using TcpUdp.Core.Utilities;

namespace TcpUdp.Server
{
    public abstract class BaseServer
    {
        private readonly string _serverName;
        private readonly int _serverPort;

        protected BaseServer(string serverName, int serverPort)
        {
            _serverName = serverName;
            _serverPort = serverPort;
        }

        public virtual ProtocolTypeEnum Type => ProtocolTypeEnum.Default;

        public virtual void Start() { }
    }
}