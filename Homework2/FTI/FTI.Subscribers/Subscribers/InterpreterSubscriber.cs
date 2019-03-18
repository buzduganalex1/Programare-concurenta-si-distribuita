using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.PubSub.V1;

namespace FTI.Subscribers.Subscribers
{
    public class InterpreterSubscriber : BaseSubscriber, ISubscriber
    {
        public async Task Subscribe()
        {
            Console.WriteLine("Interpreter subscribed");

            var projectId = "friendly-path-234919";
            var topicId = "ticketsTopic";
            var subscriptionId = "interpreterSubscription";
            var topicName = new TopicName(projectId, topicId);

            // Subscribe to the topic.
            SubscriberServiceApiClient subscriberService = SubscriberServiceApiClient.CreateAsync().Result;
            SubscriptionName subscriptionName = new SubscriptionName(projectId, subscriptionId);
            subscriberService.CreateSubscription(subscriptionName, topicName, pushConfig: null, ackDeadlineSeconds: 60);

            // Pull messages from the subscription using SimpleSubscriber.
            SubscriberClient subscriber = SubscriberClient.CreateAsync(subscriptionName).Result;

            List<PubsubMessage> receivedMessages = new List<PubsubMessage>();

            await subscriber.StartAsync((msg, cancellationToken) =>
            {
                receivedMessages.Add(msg);
                Console.WriteLine($"Received message {msg.MessageId} published at {msg.PublishTime.ToDateTime()}");
                Console.WriteLine($"Text: '{msg.Data.ToStringUtf8()}'");
                // Stop this subscriber after one message is received.
                // This is non-blocking, and the returned Task may be awaited.
                // Return Reply.Ack to indicate this message has been handled.
                return Task.FromResult(SubscriberClient.Reply.Ack);
            });
        }
    }
}
