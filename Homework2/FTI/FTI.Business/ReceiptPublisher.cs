using System;
using Google.Cloud.PubSub.V1;

namespace FTI.Business
{
    public class ReceiptPublisher : IPublisher
    {
        const string projectId = "friendly-path-234919";
        const string topicId = "ticketsTopic";
        
        public void PublishMessage(string message)
        {
            try{
                TopicName topicName = new TopicName(projectId, topicId);

                var publisher = PublisherClient.CreateAsync(topicName).Result;
                var messageId = publisher.PublishAsync(message).Result;
                
                publisher.ShutdownAsync(TimeSpan.FromSeconds(15));
            }catch(Exception e){
                Console.WriteLine(e);
            }
        }
    }
}