using System.Collections.Generic;
using System.Threading.Tasks;
using FTI.Subscribers.Subscribers;
using Google.Cloud.PubSub.V1;
using System;

namespace FTI.Subscribers
{
    class Program
    {
        static void Main(string[] args)
        {            
            ////CreateTopic();

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

        static void CreateTopic(){
            try{
                var projectId = "friendly-path-234919";
                var topicId = "ticketsTopic";
            
                PublisherServiceApiClient publisherService = PublisherServiceApiClient.CreateAsync().Result;
                TopicName topicName = new TopicName(projectId, topicId);
                publisherService.CreateTopic(topicName);
            }
            catch(Exception e){
                Console.WriteLine(e);
            }
        }
    }
}