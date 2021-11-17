using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Part43
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var url = "https://www.google.com.tw/";

            Console.WriteLine("(1) " + DateTime.Now);
            var task = MyDownloadPageAsync(url);
            Console.WriteLine("(2) " + DateTime.Now);

            var content = await task;
            Console.WriteLine("(3) " + DateTime.Now);

            Console.WriteLine($"此網頁內容共為 {content.Length} 個字元");

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        private static async Task<string> MyDownloadPageAsync(string url)
        {
            await Task.Delay(3000);

            var webClient = new WebClient();

            return await webClient.DownloadStringTaskAsync(url);
        }

    }
}
