using System;

namespace TcpUdp.Core.Interfaces
{
    public interface IFileMessageSenderWatcher
    {
        TimeSpan ElapsedTimePerAction { get; }

        TimeSpan TotalElapsedTime { get; }

        void MeasureElapsedTime(Action action);
    }
}