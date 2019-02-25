using TcpUdp.Core.Utilities;

namespace TcpUdp.Server
{
    public abstract class BaseServer
    {
        protected string ServerName { get; }
        protected int ServerPort { get; }

        protected BaseServer(string serverName, int serverPort)
        {
            ServerName = serverName;
            ServerPort = serverPort;
        }

        public virtual ProtocolTypeEnum Type => ProtocolTypeEnum.Default;

        public virtual void Start() { }
    }
}