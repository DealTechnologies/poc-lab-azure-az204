using domain;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace publisher
{
    public class Program
    {
        private const string topicEndpoint = "";
        private const string topicKey = "";

        public static async Task Main(string[] args)
        {
            TopicCredentials credentials = new TopicCredentials(topicKey);
            EventGridClient client = new EventGridClient(credentials);

            List<EventGridEvent> events = new List<EventGridEvent>();

            var order = new Order
            {
                Id = "1",
                PostalCode = "80610040",
                Product = "Combo Batata Maneira",
            };

            var @event = new EventGridEvent
            {
                Id = Guid.NewGuid().ToString(),
                EventTime = DateTime.Now,
                EventType = "Notification.Delivery.Test",
                Subject = $"Manual order delivery",
                Data = JsonSerializer.Serialize(order, new JsonSerializerOptions { IgnoreNullValues = true }),
                DataVersion = "1.0.0"
            };

            events.Add(@event);

            string topicHostname = new Uri(topicEndpoint).Host;

            await client.PublishEventsAsync(topicHostname, events);

            Console.WriteLine("Events published");
        }
    }
}
