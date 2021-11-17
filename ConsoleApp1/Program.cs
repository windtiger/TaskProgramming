using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var url = "https://www.google.com.tw/";

            //var content = MyDownloadPage(url);
            var content = await MyDownloadPageAsync(url);

            Console.WriteLine($"此網頁有 {content.Length} 個字元.");

            Console.ReadKey();
        }

        static string MyDownloadPage(string url)
        {
            Thread.Sleep(3000);

            var client = new WebClient();
            string content = client.DownloadString(url);
            return content;
        }

        static async Task<string> MyDownloadPageAsync(string url)
        {
            await Task.Delay(3000);

            var client = new HttpClient();
            return await client.GetStringAsync(url);
        }

    }

}
