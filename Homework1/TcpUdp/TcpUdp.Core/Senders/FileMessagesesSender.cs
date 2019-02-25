using System;
using System.Collections.Generic;
using TcpUdp.Core.Interfaces;
using TcpUdp.Core.Models;
using TcpUdp.Core.Utilities;

namespace TcpUdp.Core.Senders
{
    public class FileMessagesesSender : IFileMessagesSender
    {
        private readonly IEnumerable<BaseFileMessageSender> senders;

        private readonly IFileMessageSenderWatcher fileMessageSenderWatcher;

        public FileMessagesesSender(string serverName, int serverPort, int maxMessageSize)
        {
            this.fileMessageSenderWatcher = new FileMessageSenderWatcher();

            this.senders = new List<BaseFileMessageSender>
            {
                new TCPFileMessageSender(serverName, serverPort, maxMessageSize),
                new UDPFileMessageSender(serverName, serverPort, maxMessageSize)
            };
        }

        public void Send(FileMessage fileMessage, ProtocolTypeEnum protocolType)
        {
            foreach (var sender in senders)
            {
                if (sender.Type == protocolType)
                {
                    this.fileMessageSenderWatcher.MeasureElapsedTime(() => { sender.Send(fileMessage); });
                }
            }
        }

        public void SendBatched(IEnumerable<FileMessage> fileMessages, ProtocolTypeEnum protocolType)
        {
            foreach (var sender in senders)
            {
                if (sender.Type == protocolType)
                {
                    this.fileMessageSenderWatcher.MeasureElapsedTime(() => { sender.SendBatched(fileMessages); });
                }
            }
        }

        public TimeSpan TransferTimeForMessage => this.fileMessageSenderWatcher.ElapsedTimePerAction;

        public TimeSpan TotalTransferTime => this.fileMessageSenderWatcher.TotalElapsedTime;
    }

}