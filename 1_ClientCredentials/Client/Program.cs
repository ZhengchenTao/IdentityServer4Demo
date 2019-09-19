using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private static string ApiUrl = "https://localhost:44302";
        private static string IdentityServerUrl = "https://localhost:44301";
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(IdentityServerUrl);
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            //call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var apiResponse = await apiClient.GetAsync($"{ApiUrl}/Identity");
            if (!apiResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(apiResponse.StatusCode);
            }
            else
            {
                var content = await apiResponse.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
