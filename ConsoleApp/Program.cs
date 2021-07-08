using Grpc.Core;
using Grpc.Net.Client;
using GrpcService;
using IdentityModel.Client;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static GrpcService.Greeter;
using Newtonsoft.Json.Linq;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {

            
            //var _token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjNGRTYyODE4RTJCNjc3OEY3Nzg2NjAxMjc2RkM5Qzg5IiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2MjU3Mzk2MzUsImV4cCI6MTYyNTc0MzIzNSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzOTAiLCJhdWQiOiJhcGkxIiwiY2xpZW50X2lkIjoib2F1dGhDbGllbnQiLCJqdGkiOiI5NDc4NkYwNEQ4N0ZBRjM3MTIxQjlCMUYwRTAwQzU3QyIsImlhdCI6MTYyNTczOTYzNCwic2NvcGUiOlsiYXBpMS5yZWFkIl19.iXDMWKW-5eUx5sY0lDm9dDN6VoSUe3nR4RvwVLkQUa3MmesK_97raTtvgDCGkTsj6gRhe6K9AcQxLE2aA617sM2QtmRVsWmjo1zxQiwJ6eaooMVDpjrMnd-4EI76-V28COW8ifxc7CdkiCUWdds_2W55JG7kRpWx7yIAwPIPuoiXceN9X6sHIB2mfmcFqgRjsbzWKbdSWyai5WdPMOCRNGYFTqAnToKISBBMh0d3igXiRr4HMNQ_u0bDpyeHP7lq4ZoO0mdGN1y7vklAMjXr95W9IdJ41V9f9zSx5GkF8435SfPeDoYruAf82tS5Kh-zH4uHMi-HN8oO9LuWhbNlVA";


            ///* using RestSharp; // https://www.nuget.org/packages/RestSharp/ */
            var client2 = new RestClient("https://localhost:44390/connect/token");
            var request2 = new RestRequest(Method.POST);
            request2.AddHeader("cache-control", "no-cache");
            request2.AddHeader("content-type", "application/x-www-form-urlencoded");
            request2.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&client_id=oauthClient&client_secret=SuperSecretPassword", ParameterType.RequestBody);
            IRestResponse response2 = client2.Execute(request2);

            dynamic resp = JObject.Parse(response2.Content);
            var _token = resp.access_token;
            //Console.WriteLine("token: " + _token);


            var headers = new Metadata();
            headers.Add("Authorization", $"Bearer {_token}");

            Console.WriteLine("Hello gRPC service!");
            Console.WriteLine("=====================================");
            Console.WriteLine("Wait for response ... ");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");

            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new GreeterClient(channel);

            var request = new HelloRequest {
                Name = "Johan"
            };

            try
            {
                var response = await client.SayHelloAsync(request, headers);
                Console.WriteLine("Response Message    ");
                Console.WriteLine("                   ooo OK!    ");
                Console.WriteLine("=====================================");
                Console.WriteLine(response.Message);
                Console.ReadKey();
            }
            catch {
                Console.WriteLine("=====================================");
                Console.WriteLine("---  NOT Authenticated!");
                Console.WriteLine("---  NOT Authenticated!");
                Console.ReadKey();
            }

        }


    







    }
}
