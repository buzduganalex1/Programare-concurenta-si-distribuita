using System.Collections.Generic;
using System.Threading.Tasks;
using FTI.Subscribers.Subscribers;

namespace FTI.Subscribers
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = new List<Task>();

            var handlers = new List<ISubscriber>()
            {
                new ArchiveSubscriber(),
                new IndexerSubscriber(),
                new InterpreterSubscriber()
            };

            foreach (var handler in handlers)
            {
                task.Add(handler.Subscribe());
            }

            Task.WaitAll(task.ToArray());
        }
    }
}
