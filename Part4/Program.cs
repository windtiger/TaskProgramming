using System;
using System.Threading;
using System.Threading.Tasks;

namespace Part4
{
    class Program
    {
        private static void CancelTaskByBreak()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    if (token.IsCancellationRequested) break;
                    else Console.WriteLine(i++);
                }
            }, token);
            t.Start();

            Console.ReadKey();
            cts.Cancel();
        }

        private static void CancelTaskByThrow()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    if (token.IsCancellationRequested) throw new OperationCanceledException();
                    else Console.WriteLine(i++);
                }
            }, token);
            t.Start();

            Console.ReadKey();
            cts.Cancel();
        }

        private static void CancelTaskByTokenThrow()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine(i++);
                }
            }, token);
            t.Start();

            Console.ReadKey();
            cts.Cancel();
        }

        static void Main(string[] args)
        {
            //CancelTaskByBreak();
            //CancelTaskByThrow();
            CancelTaskByTokenThrow();

            Console.WriteLine("Main thread end.");
            Console.ReadKey();
        }
    }
}
