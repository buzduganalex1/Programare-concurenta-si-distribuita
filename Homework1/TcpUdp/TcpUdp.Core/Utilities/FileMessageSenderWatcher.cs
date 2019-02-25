using System;
using System.Diagnostics;
using TcpUdp.Core.Interfaces;

namespace TcpUdp.Core.Utilities
{
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
}