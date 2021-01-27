using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MongoDB.Driver;
using domain;

namespace function
{
    public static class List
    {
        [FunctionName("order-list")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                var client = new MongoClient(Environment.GetEnvironmentVariable("MongoDBAtlasConnectionString"));

                var database = client.GetDatabase("Delivery");

                var collection = database.GetCollection<Order>("Order");

                var response = await collection.Find(a => true).ToListAsync();

                return new OkObjectResult(response);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult("Error refreshing - " + e.Message);
            }
        }
    }
}
