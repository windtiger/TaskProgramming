using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Part10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("No Use Critical Section----------");
            NoUseCriticalSection();
            Console.WriteLine("Use Critical Section----------");
            UseCriticalSection();

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        static void NoUseCriticalSection()
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

        static void UseCriticalSection()
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
                        ba.DepositByLock(100);
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 1; j <= 1000; j++)
                    {
                        ba.WithdrawByLock(100);
                    }
                }));

                Task.WaitAll(tasks.ToArray());
                Console.WriteLine($"{i},Final balance is {ba.Balance}.");

            }

        }
    }
}
