using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace shared
{
    public static class Rest
    {
        public static async Task<T> GetJsonFromContent<T>(string baseUri, string uri)
        {
            var client = new HttpClient { BaseAddress = new Uri(baseUri) };

            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            if (Environment.GetEnvironmentVariable("USEAPIMANAGEMENT") == "true")
                request.Headers.TryAddWithoutValidation("Ocp-Apim-Subscription-Key", Environment.GetEnvironmentVariable("OCPAPIMKEY"));

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<T>();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return default;
        }
    }
}
