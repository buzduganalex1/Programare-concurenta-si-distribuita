using System;
using System.Collections.Generic;
using System.IO;
using TcpUdp.Core.Interfaces;
using TcpUdp.Core.Models;

namespace TcpUdp.Core.Database
{
    public class FileMessageProvider : IFileMessageProvider
    {
        public IEnumerable<FileMessage> GetFileMessages()
        {
            var fileMessages = new List<FileMessage>();

            var path = @"C:\GitRepositories\Programare-concurenta-si-distribuita\Homework1\TcpUdp\TcpUdp.Core\TestResources\";

            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                try
                {
                    var fileName = Path.GetFileName(file);

                    var format = fileName.Split('.')[1];

                    var name = fileName.Split('.')[0];

                    fileMessages.Add(new FileMessage
                    {
                        Name = name,
                        Format = format,
                        Data = File.ReadAllBytesAsync($"{path}{name}.{format}").Result
                    });
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return fileMessages;
        }
    }
}