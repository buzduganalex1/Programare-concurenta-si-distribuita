using System.Collections.Generic;
using System.Threading.Tasks;
using FTI.Business.Subscribers;

namespace FTI.Subscribers
{
    class Program
    {
        static void Main(string[] args)
        {
            ////new ReceiptPublisher().CreateTopic();

            var task = new List<Task>();
            var handlers = new List<ISubscriber>
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