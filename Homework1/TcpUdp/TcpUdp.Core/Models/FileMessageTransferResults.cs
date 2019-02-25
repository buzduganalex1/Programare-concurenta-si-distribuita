namespace TcpUdp.Core.Models
{
    public class FileMessageTransferResults
    {
        public int BytesSent { get; set; }

        public int NumberOfMessages { get; set; }
        
        public FileMessageTransferResults(int bytesSent = 0, int numberOfMessages = 0)
        {
            this.BytesSent = bytesSent;
            this.NumberOfMessages = numberOfMessages;
        }
    }
}