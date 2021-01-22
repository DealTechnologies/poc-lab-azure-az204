// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using domain;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using shared;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace function.company
{
    public static class Survey
    {
        [FunctionName("survey")]
        public static async Task Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation(eventGridEvent.Data.ToString());

            var order = JsonSerializer.Deserialize<Order>((string)eventGridEvent.Data);

            var address = await Rest.GetJsonFromContent<Postal>("https://viacep.com.br/", "ws/01001000/json");

            if (address != null)
            {
                order.Address = new Address
                {
                    Street = $"{address.Uf} - {address.Bairro} / {address.Logradouro}, {address.Complemento}"
                };

                var client = new MongoClient(Environment.GetEnvironmentVariable("MongoDBAtlasConnectionString"));

                await client.GetDatabase("Delivery").GetCollection<Order>("Order").ReplaceOneAsync(o => o.Id == order.Id, order);

                log.LogInformation($"address updated {order.Address?.Street}");
            }
        }
    }
}