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
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
                    

        static async Task Main(string[] args)
        {

            // URL to Identity server
            string ID4url = "https://localhost:44390/connect/token";
            string _token = "";

            
            //string temp_token = Console.ReadLine();
            string temp_token = "";
            //var _token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjNGRTYyODE4RTJCNjc3OEY3Nzg2NjAxMjc2RkM5Qzg5IiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2MjU3Mzk2MzUsImV4cCI6MTYyNTc0MzIzNSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzOTAiLCJhdWQiOiJhcGkxIiwiY2xpZW50X2lkIjoib2F1dGhDbGllbnQiLCJqdGkiOiI5NDc4NkYwNEQ4N0ZBRjM3MTIxQjlCMUYwRTAwQzU3QyIsImlhdCI6MTYyNTczOTYzNCwic2NvcGUiOlsiYXBpMS5yZWFkIl19.iXDMWKW-5eUx5sY0lDm9dDN6VoSUe3nR4RvwVLkQUa3MmesK_97raTtvgDCGkTsj6gRhe6K9AcQxLE2aA617sM2QtmRVsWmjo1zxQiwJ6eaooMVDpjrMnd-4EI76-V28COW8ifxc7CdkiCUWdds_2W55JG7kRpWx7yIAwPIPuoiXceN9X6sHIB2mfmcFqgRjsbzWKbdSWyai5WdPMOCRNGYFTqAnToKISBBMh0d3igXiRr4HMNQ_u0bDpyeHP7lq4ZoO0mdGN1y7vklAMjXr95W9IdJ41V9f9zSx5GkF8435SfPeDoYruAf82tS5Kh-zH4uHMi-HN8oO9LuWhbNlVA";
            


            // timeout
            ConsoleKeyInfo k = new ConsoleKeyInfo();            
            for (int cnt = 7; cnt > 0; cnt--)
            {
                if (Console.KeyAvailable == true)
                {
                    k = Console.ReadKey();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Press any key to enter a token.");
                    Console.WriteLine("Timeout :  {0} s", cnt.ToString());
                }
            }

            //Console.WriteLine("The key pressed was " + k.Key);

            if (k.Key.ToString() == "0") { 
                Console.WriteLine("No key pressed ... continue");
            } else {
                Console.WriteLine("The key pressed was " + k.Key);
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Enter token to continue");
                Console.WriteLine("(Leave empty to retive token for ID4 server)\r\n");
                temp_token = Console.ReadLine();
            }





            // getting token from server or manually insert token.
            if (temp_token == "") {
                Console.WriteLine("(2) Getting token from ID4 server ...\r\n");
                _token = GetToken.getToken(ID4url);
            } else {
                Console.WriteLine("(1) Token Retrieved!\r\n");
                _token = temp_token;
                temp_token = _token.ToString().Substring(0, 12);
                Console.WriteLine("\r\ntoken: " + temp_token + " .... \r\n\r\n");
            }

 

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
                Console.WriteLine("-------------------------------------\r\ngRPC message:\r\n");
                Console.WriteLine(response.Message + "\r\n\r\n\r\n");
                Console.ReadKey();
            }
            catch {
                Console.WriteLine("-------------------------------------");                
                Console.WriteLine("\r\n---  NOT Authenticated!\r\n\r\n\r\n");
                Console.ReadKey();
            }
        }

    }
}
