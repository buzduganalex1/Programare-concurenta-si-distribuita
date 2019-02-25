using TcpUdp.Core.Models;
using TcpUdp.Core.Utilities;

namespace TcpUdp.Core.Senders
{
    public class UDPFileMessageSender : BaseFileMessageSender
    {
        public UDPFileMessageSender(string serverName, int serverPort, int maxMessageSize) : base(serverName, serverPort, maxMessageSize)
        {
        }

        public override ProtocolTypeEnum Type => ProtocolTypeEnum.UDP;

        public override void Send(FileMessage fileMessage)
        {

        }
    }
}