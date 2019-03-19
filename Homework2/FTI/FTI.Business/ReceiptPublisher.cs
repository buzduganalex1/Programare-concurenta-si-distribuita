using System;
using Google.Cloud.PubSub.V1;

namespace FTI.Business
{
    public class ReceiptPublisher : IPublisher
    {
        public void PublishMessage(string message)
        {
            try
            {
                var topicName = new TopicName(EnvResources.ProjectId, EnvResources.TopicId);
                var publisher = PublisherClient.CreateAsync(topicName).Result;
                var messageId = publisher.PublishAsync(message).Result;

                publisher.ShutdownAsync(TimeSpan.FromSeconds(15));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void CreateTopic()
        {
            try
            {
                var publisherService = PublisherServiceApiClient.CreateAsync().Result;
                var topicName = new TopicName(EnvResources.ProjectId, EnvResources.TopicId);

                publisherService.CreateTopic(topicName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}