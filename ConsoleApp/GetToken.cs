using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class GetToken
    {
        public static string getToken(string ID4url) {
            var _token = "";
            try
            {
                Console.WriteLine("Getting toknen ...");
                Console.WriteLine("\r\n=====================================");
                Console.WriteLine("  ID4 server ... conecting to ");
                Console.WriteLine("\t{0}", ID4url);
                ///* using RestSharp; // https://www.nuget.org/packages/RestSharp/ */
                var client2 = new RestClient(ID4url);
                var request2 = new RestRequest(Method.POST);
                request2.AddHeader("cache-control", "no-cache");
                request2.AddHeader("content-type", "application/x-www-form-urlencoded");
                request2.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&scope=api1.read&client_id=oauthClient&client_secret=SuperSecretPassword", ParameterType.RequestBody);
                IRestResponse response2 = client2.Execute(request2);

                dynamic resp = JObject.Parse(response2.Content);
                _token = resp.access_token;
                string utNme = _token.ToString().Substring(0, 12);

                Console.WriteLine("\r\ntoken: " + utNme + " .... \r\n\r\n");
            }
            catch (Exception e)
            {
                //code for any other type of exception
                Console.WriteLine(e.Message);
                _token = "";
            }

            string returntoken = _token.ToString();
            return returntoken;
        }

 
    }
}
