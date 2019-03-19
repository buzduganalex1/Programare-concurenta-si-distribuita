using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.PubSub.V1;

namespace FTI.Business.Subscribers
{
    public abstract class BaseSubscriber : ISubscriber
    {
        protected BaseSubscriber(string subscriptionId)
        {
            SubscriptionId = subscriptionId;
        }
        
        protected virtual string SubscriptionId { get; }

        public virtual async Task Subscribe()
        {
            Console.WriteLine($"{this.SubscriptionId} subscribed");

            var subscriptionName = new SubscriptionName(EnvResources.ProjectId, this.SubscriptionId);
            ////var topicName = new TopicName(EnvResources.ProjectId, EnvResources.TopicId);
            ////var subscriberService = SubscriberServiceApiClient.CreateAsync().Result;
            ////subscriberService.CreateSubscription(subscriptionName, topicName, pushConfig: null, ackDeadlineSeconds: 60);
            
            var subscriber = SubscriberClient.CreateAsync(subscriptionName).Result;
            var receivedMessages = new List<PubsubMessage>();

            await subscriber.StartAsync((msg, cancellationToken) =>
            {
                receivedMessages.Add(msg);

                Console.WriteLine($"Received message {msg.MessageId} published at {msg.PublishTime.ToDateTime()}");
                Console.WriteLine($"Text: '{msg.Data.ToStringUtf8()}'");

                return Task.FromResult(SubscriberClient.Reply.Ack);
            });
        }
    }
}