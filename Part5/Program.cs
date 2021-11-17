using System;
using System.Threading;
using System.Threading.Tasks;

namespace Part5
{
    class Program
    {
        static void Main(string[] args)
        {

            WaitTask1();

            Console.ReadKey();
        }

        static void WaitTask1()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"5 sec");
                var isCancel = token.WaitHandle.WaitOne(5000);
                Console.WriteLine($"{(isCancel?"cancel":"not cancel")}");

            }, token);

            Console.ReadKey();
            cts.Cancel();
        }

    }
}
