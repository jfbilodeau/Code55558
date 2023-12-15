using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client;


namespace ConsoleApp1
{
    class Program
    {
        private const string _clientId = "<put APP ID here>"; 
        private const string _tenantId = "<put Tenant ID here>";
        public static async Task Main(string[] args)
        {
            var app = PublicClientApplicationBuilder
                .Create(_clientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
                .WithRedirectUri("http://localhost/")
                .Build();
            string[] scopes = { "User.Read" };
            
            AuthenticationResult result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();
            
            Console.WriteLine($"ID Token:\t{result.IdToken}");
            Console.WriteLine($"Access Token:\t{result.AccessToken}");
            // go to an api, on behalf of the user.... 


            Console.ReadLine();

        }
    }
}
