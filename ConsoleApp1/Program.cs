using System;
using System.IO;
using System.Net;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            String url = "https://localhost:44390";
            //url = "https://repo.sebank.se";

            var response4 = WebRequest.Create(url).GetResponse();
            var responseData = new StreamReader(response4.GetResponseStream()).ReadToEnd();
            Console.WriteLine(responseData);
            Console.WriteLine("");
            Console.WriteLine("Press any key to continue");

            Console.ReadLine();
        }
    }
}
