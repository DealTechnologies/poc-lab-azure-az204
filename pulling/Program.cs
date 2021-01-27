using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using domain;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace pulling
{
    public class Program
    {
        private const string storageConnectionString = "";
        private const string queueName = "messagequeue";

        public static async Task Main(string[] args)
        {
            var client = new QueueClient(storageConnectionString, queueName);

            await client.CreateAsync();

            await CreateMessageQueue(client);

            Console.ReadKey();

            await ReadMessageQueue(client);
        }

        internal static async Task CreateMessageQueue(QueueClient client)
        {
            Console.WriteLine($"---New Messages---");

            var order = new Order
            {
                Id = "1",
                PostalCode = "80610040",
                Product = "Combo Batata Maneira",
            };

            var @event = JsonSerializer.Serialize(new EventGridEvent
            {
                Id = Guid.NewGuid().ToString(),
                EventTime = DateTime.Now,
                EventType = "Notification.Delivery",
                Subject = $"Dispacher new order to find address",
                Data = JsonSerializer.Serialize(order, new JsonSerializerOptions { IgnoreNullValues = true }),
                DataVersion = "1.0.0"
            });

            await client.SendMessageAsync(@event);

            Console.WriteLine($"Sent Message:\t{@event}");
        }

        internal static async Task ReadMessageQueue(QueueClient client)
        {
            Console.WriteLine($"---Account Metadata---");
            
            Console.WriteLine($"Account Uri:\t{client.Uri}");

            Console.WriteLine($"---Existing Messages---");

            int batchSize = 10;
            TimeSpan visibilityTimeout = TimeSpan.FromSeconds(2.5d);

            Response<QueueMessage[]> messages = await client.ReceiveMessagesAsync(batchSize, visibilityTimeout);

            foreach (QueueMessage message in messages?.Value)
            {
                Console.WriteLine($"[{message.MessageId}]");

                var @event = JsonSerializer.Deserialize<EventGridEvent>(message.MessageText);

                await client.DeleteMessageAsync(message.MessageId, message.PopReceipt);
            }
        }
    }
}
