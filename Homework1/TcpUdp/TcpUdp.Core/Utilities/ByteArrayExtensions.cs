using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace TcpUdp.Core.Utilities
{
    public static class ByteArrayExtensions
    {
        public static object ByteArrayToObject(this byte[] arrBytes)
        {
            try
            {
                var memStream = new MemoryStream();
                var binForm = new BinaryFormatter();

                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);

                var deserializedObject = binForm.Deserialize(memStream);

                return deserializedObject;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                throw;
            }
        }

        public static IEnumerable<byte[]> Split(this byte[] value, int bufferLength)
        {
            var countOfArray = value.Length / bufferLength;

            if (value.Length % bufferLength > 0)
            {
                countOfArray++;
            }

            for (var i = 0; i < countOfArray; i++)
            {
                yield return value.Skip(i * bufferLength).Take(bufferLength).ToArray();
            }
        }
    }
}