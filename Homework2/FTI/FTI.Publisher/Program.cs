using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FTI.Api.Models;
using Google.Cloud.PubSub.V1;

namespace FTI.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var projectId = "fastticketinterpreter";
            var topicId = "ticketsTopic";
            var subscriptionId = "archiveSubscription";

            // First create a topic.
            PublisherServiceApiClient publisherService = PublisherServiceApiClient.CreateAsync().Result;
            TopicName topicName = new TopicName(projectId, topicId);
            publisherService.CreateTopic(topicName);

            // Subscribe to the topic.
            SubscriberServiceApiClient subscriberService =  SubscriberServiceApiClient.CreateAsync().Result;
            SubscriptionName subscriptionName = new SubscriptionName(projectId, subscriptionId);

            subscriberService.CreateSubscription(subscriptionName, topicName, pushConfig: null, ackDeadlineSeconds: 60);

            // Publish a message to the topic using PublisherClient.
            PublisherClient publisher =  PublisherClient.CreateAsync(topicName).Result;

            var receipt = new Receipt();

            receipt.AddCustomerNumber("123");
            receipt.AddItem(new Item("Milk", new Amount(CurrencyEnum.EUR, 10.0f)));
            receipt.AddItem(new Item("Egs", new Amount(CurrencyEnum.EUR, 5.0f)));
            receipt.AddItem(new Item("Honey", new Amount(CurrencyEnum.EUR, 2.0f)));

            // PublishAsync() has various overloads. Here we're using the string overload.
            string messageId =  publisher.PublishAsync(receipt.ToJson()).Result;
            
            // PublisherClient instance should be shutdown after use.
            // The TimeSpan specifies for how long to attempt to publish locally queued messages.
            publisher.ShutdownAsync(TimeSpan.FromSeconds(15));


            // Pull messages from the subscription using SimpleSubscriber.
            SubscriberClient subscriber =  SubscriberClient.CreateAsync(subscriptionName).Result;
            List<PubsubMessage> receivedMessages = new List<PubsubMessage>();

            TEST(subscriber, receivedMessages);

            // Start the subscriber listening for messages.
            // Tidy up by deleting the subscription and the topic.
            subscriberService.DeleteSubscription(subscriptionName);
            publisherService.DeleteTopic(topicName);
        }

        private static async void TEST(SubscriberClient subscriber, List<PubsubMessage> receivedMessages)
        {
            await subscriber.StartAsync((msg, cancellationToken) =>
            {
                receivedMessages.Add(msg);
                Console.WriteLine($"Received message {msg.MessageId} published at {msg.PublishTime.ToDateTime()}");
                Console.WriteLine($"Text: '{msg.Data.ToStringUtf8()}'");
                // Stop this subscriber after one message is received.
                // This is non-blocking, and the returned Task may be awaited.
                subscriber.StopAsync(TimeSpan.FromSeconds(15));
                // Return Reply.Ack to indicate this message has been handled.
                return Task.FromResult(SubscriberClient.Reply.Ack);
            });
        }
    }
}
