using System.Collections.Generic;
using TcpUdp.Core.Interfaces;
using TcpUdp.Core.Models;
using TcpUdp.Core.Utilities;

namespace TcpUdp.Core.Senders
{
    public abstract class BaseFileMessageSender : IFileMessageSender
    {  
        protected int ServerPort { get; }
        protected int MaxMessageSize { get; }
        protected string ServerName { get; }
        public FileMessageTransferResults Results { get; set; }

        protected BaseFileMessageSender(string serverName, int serverPort, int maxMessageSize)
        {
            ServerName = serverName;
            ServerPort = serverPort;
            MaxMessageSize = maxMessageSize;
            this.Results = new FileMessageTransferResults();
        }

        public virtual ProtocolTypeEnum Type => ProtocolTypeEnum.Default;

        public virtual void Send(FileMessage fileMessage){}

        public virtual void SendBatched(IEnumerable<FileMessage> fileMessage){}

        public virtual string GetResultsMessage =>
            $"Protocol type: {this.Type.ToString()}\nBytes sent: {this.Results.BytesSent}\nNumber of messages sent: {this.Results.NumberOfMessages}";
    }
}