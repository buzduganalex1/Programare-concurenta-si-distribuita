# Homework1 description
Write a program that measure the time to transfer various amount of data (500MB, 1GB (1.5
milion of WhatsApp messages, 4000 photos, 10.000 emails or reuse a large buffer)) under
various conditions (Make sure you are sending bytes. Do not use higher-level functions that
assume you are writing strings.)

## Requirements

- Supported protocols as parameters (for both client/server): UDP, TCP
- Message size: 1 to 65535 bytes
- Two mechanisms should be implemented: streaming and stop-and-wait (acknowledge is
performed before the sending of the next message)

**Requirements for output**:

- After each server session, the server will print: used protocol, number of messages read,
number of bytes reads

- At the end of execution, the client will print: transmission time, number of sent messages,
number of bytes sent.

## Technologies

We used :

- NET Core
- [System.Net.Sockets](https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets?view=netframework-4.7.2)
- Visual Studio 2017
- Visual Code
## Build

To build an executable for windows run the command:
```
dotnet publish -c Release -r win10-x64
```

To build for linux/ubuntu run the command:
```
dotnet publish -c Release -r ubuntu.16.10-x64
```

**You will find the executables under the bin/Release/folderWithFrameworkName**

## Implementation Details

The application structure is comprised of a:

- Client project
- Core project
- Server project

The core project exposes all the required functionality to connect to a UDP/TCP client, to start a TCP/UDP server, to read data and to monitor the elapsed time.

We used the **FileMessage** model for sending different types for data.

```c#
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
```

We are using the factory pattern for getting a client/server instance of type UDP/TCP.
```c#
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
```

More protocols can be added by extending the abstract class BaseMessageSender.

To monitor the execution time we are using a FileMessageSenderWatcher which measures actions and saves the elapsed time, both per action as per total.

```c#
  public class FileMessageSenderWatcher : IFileMessageSenderWatcher
    {
        private readonly Stopwatch stopwatch;

        public FileMessageSenderWatcher()
        {
            this.stopwatch = new Stopwatch();
        }

        public void MeasureElapsedTime(Action action)
        {
            this.stopwatch.Start();

            action.Invoke();

            this.stopwatch.Stop();

            this.ElapsedTimePerAction = stopwatch.Elapsed;

            this.TotalElapsedTime = this.TotalElapsedTime.Add(stopwatch.Elapsed);

            this.stopwatch.Reset();
        }
        
        public TimeSpan ElapsedTimePerAction { private set; get; }
        
        public TimeSpan TotalElapsedTime { private set; get; }
    }
```
To create a server simlpy use:
```c#
 public static void Main(string[] args)
        {
            var serverInitiator = new ServerInitiator();

            serverInitiator.Start(ProtocolTypeEnum.UDP);
        }
```

To create a client simply use:
```c#
 public class ClientInit
    {
        private const int messageSize = 65535;

        public static void Main()
        {
            var fileMessageSender = new FileMessagesesSender(ConnectionCredentials.ServerName, ConnectionCredentials.UDPServerPort, messageSize);

            var fileMessages = new MockMessageProvider().GetFileMessages().ToList();
            
            fileMessageSender.SendBatched(fileMessages, ProtocolTypeEnum.UDP);    
        }
    }
```

We are loading the data using a IFileMessageDataProvider which retrives files from disk or simply mocks.
```c#
    public class FileMessageProvider : IFileMessageProvider
    {
        public IEnumerable<FileMessage> GetFileMessages()
        {
            var fileMessages = new List<FileMessage>();

            var path = @"C:\GitRepos\Programare-concurenta-si-distribuita\Homework1\TcpUdp\TcpUdp.Core\TestResources";

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
```

## Benchmarks
For testing purposes we used a [kaggle image dataset](:https://www.kaggle.com/thedownhill/art-images-drawings-painting-sculpture-engraving ) comprised of 9000 images total size of 500 MB.

Other than this we tried sending zip, mp4, and iso extensions.

We ran multiple test on the Tcp and Udp server both with streaming and with stop and wait.

## Results

On the TCP server with the streaming option we processed 9000 images in 30 seconds.

Sending the 500mb zip took us 2 seconds on computer and 8 seconds on the laptop.

Sending the 250mb video took us 1 second on the computer and 4 on the laptop.


| __*TCP*__     | 10MB | 100MB | 1000MB  |   |   |   |   |   |   |   |   |   |   |   |
|---------------|------|-------|---------|---|---|---|---|---|---|---|---|---|---|---|
| Streaming     | 0.02 | 0.4   | 4       |   |   |   |   |   |   |   |   |   |   |   |
| Stop and wait | 0.03 | 0.5   | 6       |   |   |   |   |   |   |   |   |   |   |   |
| __*UDP*__     |      |       |         |   |   |   |   |   |   |   |   |   |   |   |
| Streaming     | 0.02 | 2     | 4       |   |   |   |   |   |   |   |   |   |   |   |
| Stop and wait | 0.03 | 3     | 7       |   |   |   |   |   |   |   |   |   |   |   |
|               |      |       |         |   |   |   |   |   |   |   |   |   |   |   |
|               |      |       |         |   |   |   |   |   |   |   |   |   |   |   |
|               |      |       |         |   |   |   |   |   |   |   |   |   |   |   |
|               |      |       |         |   |   |   |   |   |   |   |   |   |   |   |