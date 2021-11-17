using System;
using System.Threading;
using System.Threading.Tasks;

namespace Part6
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t1 = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"Task1 : {i}");
                    Thread.Sleep(1000);
                }

                Console.WriteLine($"Task1 done.");
            }, token);

            var t2 = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"Task2 : {i}");
                    Thread.Sleep(1000);
                }

                Console.WriteLine($"Task2 done.");
            }, token);

            //Console.ReadKey();
            //cts.Cancel();

            //t1.Wait();
            //t2.Wait();
            Task.WaitAll(new Task[] { t1, t2 }, 6000);
            //Task.WaitAny(t1, t2);

            Console.WriteLine($"t1 status : {t1.Status}");
            Console.WriteLine($"t2 status : {t2.Status}");

            Console.WriteLine("Main done.");
            Console.ReadKey();
        }

        static void Task1()
        {

        }

        static void Task2()
        {

        }
    }
}
