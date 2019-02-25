using TcpUdp.Core.Utilities;

namespace TcpUdp.Server
{
    public class ServerInit
    {
        public static void Main(string[] args)
        {
            var serverInitiator = new ServerInitiator();

            serverInitiator.Start(ProtocolTypeEnum.UDP);
        }
    }
}

