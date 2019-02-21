using System;

namespace TcpUdp.Core
{
    [Serializable]
    public class FileMessage
    {
        public string Name;

        public string Format;

        public byte[] Data;
    }
}
