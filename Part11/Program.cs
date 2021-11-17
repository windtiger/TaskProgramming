using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Part11
{
    class Program
    {
        static void Main(string[] args)
        {
            TestInterlock();

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }



        static void TestInterlock()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();

            for (int i = 1; i < 10; i++)
            {
                tasks.Clear();

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 1; j <= 1000; j++)
                    {
                        ba.Deposit(100);
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 1; j <= 1000; j++)
                    {
                        ba.Withdraw(100);
                    }
                }));

                Task.WaitAll(tasks.ToArray());
                Console.WriteLine($"{i},Final balance is {ba.Balance}.");

            }

        }
    }
}
