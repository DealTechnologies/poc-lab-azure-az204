using domain;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace function.company
{
    public static class Delivery
    {
        [FunctionName("order-delivery")]
        public static async Task Run([QueueTrigger("myqueue-items", Connection = "")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            var tasks = new List<Task>();

            var @event = JsonConvert.DeserializeObject<EventGridEvent>(myQueueItem);

            var @order = JsonConvert.DeserializeObject<Order>((string)@event.Data);

            @order.Delivered = true;

            var client = new MongoClient(Environment.GetEnvironmentVariable("MongoDBAtlasConnectionString"));

            await client.GetDatabase("Delivery").GetCollection<Order>("Order").ReplaceOneAsync(o => o.Id == order.Id, @order);

            // todo some logic
            // send e-mail
            // push message
        }
    }
}
