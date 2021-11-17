using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Part13
{
    class Program
    {
        static void Main(string[] args)
        {
            //UseMutex();
            UseMutexs();
            //CrossProgram();

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        static void UseMutex()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();
            var mutex = new Mutex();

            for (int i = 1; i < 10; i++)
            {
                tasks.Clear();

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 1; j <= 1000; j++)
                    {
                        bool token = mutex.WaitOne();
                        try
                        {
                            ba.Deposit(1);
                        }
                        finally
                        {
                            if (token)
                            {
                                mutex.ReleaseMutex();
                            }
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 1; j <= 1000; j++)
                    {
                        bool token = mutex.WaitOne();
                        try
                        {
                            ba.Withdraw(1);
                        }
                        finally
                        {
                            if (token)
                            {
                                mutex.ReleaseMutex();
                            }
                        }
                    }
                }));

                Task.WaitAll(tasks.ToArray());
                Console.WriteLine($"{i},Final balance is {ba.Balance}.");

            }

        }

        static void UseMutexs()
        {
            var tasks = new List<Task>();
            var ba1 = new BankAccount();
            var ba2 = new BankAccount();
            var mutex1 = new Mutex();
            var mutex2 = new Mutex();


            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int j = 1; j <= 1000; j++)
                {
                    bool token = mutex1.WaitOne();
                    try
                    {
                        ba1.Deposit(1);
                    }
                    finally
                    {
                        if (token)
                        {
                            mutex1.ReleaseMutex();
                        }
                    }
                }
            }));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int j = 1; j <= 1000; j++)
                {
                    bool token = mutex2.WaitOne();
                    try
                    {
                        ba2.Deposit(1);
                    }
                    finally
                    {
                        if (token)
                        {
                            mutex2.ReleaseMutex();
                        }
                    }
                }
            }));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int j = 1; j <= 1000; j++)
                {
                    bool token = WaitHandle.WaitAll(new[] { mutex1, mutex2 });

                    try
                    {
                        ba1.Tranfer(ba2, 1);
                    }
                    finally
                    {
                        if (token)
                        {
                            mutex1.ReleaseMutex();
                            mutex2.ReleaseMutex();
                        }
                    }
                }


            }));

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final ba1 balance is {ba1.Balance}.");
            Console.WriteLine($"Final ba2 balance is {ba2.Balance}.");

        }

        static void CrossProgram()
        {
            const string appName = "App";
            Mutex mutex;

            try
            {
                mutex = Mutex.OpenExisting(appName);
                Console.WriteLine($"{appName} is running.");
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                Console.WriteLine($"{appName} can run.");
                mutex = new Mutex(false, appName);

            }

            Console.WriteLine($"Press any key to end the program .");
            Console.ReadKey();
            mutex.ReleaseMutex();
        }
    }
}
