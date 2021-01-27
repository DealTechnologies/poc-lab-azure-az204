using domain;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace function.company
{
    public static class Dispacher
    {
        [FunctionName("order-dispacher")]
        public static async Task RunAsync([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var client = new MongoClient(Environment.GetEnvironmentVariable("MongoDBAtlasConnectionString"));

            var database = client.GetDatabase("Delivery");

            var collection = database.GetCollection<Order>("Order");

            var orders = await collection.Find(a => !a.Delivered).ToListAsync();

            await DispachEventToGrid(orders);

            static async Task DispachEventToGrid(List<Order> orders)
            {
                foreach (var order in orders)
                {
                    var events = new List<EventGridEvent>();

                    var credentials = new TopicCredentials(Environment.GetEnvironmentVariable("TRANSACTIONTOPICKEY"));

                    var client = new EventGridClient(credentials);

                    var @event = new EventGridEvent
                    {
                        Id = Guid.NewGuid().ToString(),
                        EventTime = DateTime.Now,
                        EventType = "Notification.Delivery",
                        Subject = $"Dispacher new order to find address",
                        Data = JsonSerializer.Serialize(orders, new JsonSerializerOptions { IgnoreNullValues = true }),
                        DataVersion = "1.0.0"
                    };

                    events.Add(@event);

                    var topicHostname = new Uri(Environment.GetEnvironmentVariable("TRANSACTIONTOPICENDPOINT")).Host;

                    await client.PublishEventsAsync(topicHostname, events);
                }
            }
        }
    }
}
