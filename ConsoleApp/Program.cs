using Grpc.Core;
using Grpc.Net.Client;
using GrpcService;
using System;
using System.Threading.Tasks;
using static GrpcService.Greeter;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var _token = "22222eyJhbGciOiJSUzI1NiIsImtpZCI6IjNGRTYyODE4RTJCNjc3OEY3Nzg2NjAxMjc2RkM5Qzg5IiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2MjU2NjQxOTIsImV4cCI6MTYyNTY2Nzc5MiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzOTAiLCJhdWQiOiJhcGkxIiwiY2xpZW50X2lkIjoib2F1dGhDbGllbnQiLCJqdGkiOiJGOTk3NTE2MTNCRjA4NzlEODJCMjhEQjgxNTAxMUUxOCIsImlhdCI6MTYyNTY2NDE5Miwic2NvcGUiOlsiYXBpMS5yZWFkIl19.fyGagZ2oB7T77U5P-5aQ_Z4fkK-CRR6sQBDibub8oX1ckuR5DuQYzKpN5GTqeimfEWfvG9aVvK9e9-ZkM7ocBy3PgaUdwERCxXO60HbZUk_wisK39w-vhzHKEipeWYkVyTv-wM2HhLUe5j3_P7MZJYx6vGYgBOa1Ilxq5kxYDbE15F6fTfJoGmxfSns2jww_dTdkUlCrxDFUVDaQZpi6ECOiCyYXa5QX7snlHz5Ick1sfrjtWuEtAGgJQNJMHLCPOnJwGvvChj5OaroFnY7ZLsHfbHRo_KrxwVtfeuJD-RxFKVDwa5wrP9u6ZmmqQndvdDrF5MGMnPpYq31dZO8W4g";
            var headers = new Metadata();
            headers.Add("Authorization", $"Bearer {_token}");

            Console.WriteLine("Hello World!");
            Console.WriteLine("=====================================");
            Console.WriteLine("Wait for response");
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
