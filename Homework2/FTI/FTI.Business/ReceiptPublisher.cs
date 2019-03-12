using System;
using Google.Cloud.PubSub.V1;

namespace FTI.Business
{
    public class ReceiptPublisher : IPublisher
    {
        const string projectId = "fastticketinterpreter";
        const string topicId = "ticketsTopic";
        
        public void PublishMessage(string message)
        {
            var publisher = PublisherClient.CreateAsync(new TopicName(projectId, topicId)).Result;

            var messageId = publisher.PublishAsync(message).Result;
            
            publisher.ShutdownAsync(TimeSpan.FromSeconds(15));
        }
    }
}