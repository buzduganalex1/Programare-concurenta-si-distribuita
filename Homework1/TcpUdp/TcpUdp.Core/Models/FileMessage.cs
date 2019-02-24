using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TcpUdp.Core.Models
{
    [Serializable]
    public class FileMessage
    {
        public string Name;

        public string Format;

        public byte[] Data;

        public byte[] ToByteArray()
        {
            var bf = new BinaryFormatter();
            var ms = new MemoryStream();

            bf.Serialize(ms, this);

            return ms.ToArray();
        }
    }
}
