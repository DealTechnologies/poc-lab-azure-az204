using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using domain;
using MongoDB.Driver;
using System.Text.Json;

namespace function.company
{
    public static class Register
    {
        [FunctionName("register")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            
            Order order = JsonSerializer.Deserialize<Order>(requestBody);

            var client = new MongoClient(Environment.GetEnvironmentVariable("MongoDBAtlasConnectionString"));

            await client.GetDatabase("Delivery").GetCollection<Order>("Order").InsertOneAsync(order);

            return new OkObjectResult(order);
        }
    }
}
