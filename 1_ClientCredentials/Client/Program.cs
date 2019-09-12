using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private static string apiUrl = "https://localhost:44364";
        private static string identityServerUrl = "https://localhost:44342";
        private static string apiAndIdentityServerUrl = "https://localhost:44302";
        static async Task Main(string[] args)
        {
            await ResourceOwnerPassword();
        }

        public static async Task ResourceOwnerPassword()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(apiAndIdentityServerUrl);
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "ro.client",
                ClientSecret = "secret",
                UserName = "alice",
                Password = "password",
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

            var apiResponse = await apiClient.GetAsync($"{apiAndIdentityServerUrl}/Identity");
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

        public static async Task ClientCredentials()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(identityServerUrl);
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

            var apiResponse = await apiClient.GetAsync($"{apiUrl}/Identity");
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
