using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Part12
{
    class Program
    {
        static void Main(string[] args)
        {
            LockRecursion(5);
            //UseCriticalSection();

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        static SpinLock s1 = new SpinLock(false);
        static void LockRecursion(int x)
        {
            if (x == 0) return;
            
            var lockToken = false;
            try
            {
                s1.Enter(ref lockToken);
            }
            catch (LockRecursionException ex)
            {
                Console.WriteLine($"exception : {ex.Message}");
            }
            finally
            {
                if (lockToken)
                {
                    Console.WriteLine($"took a lock, x = {x}");
                    LockRecursion(x - 1);
                    s1.Exit();
                }
                else
                {
                    Console.WriteLine($"Failed to took a lock, x = {x}");
                }
            }
        }

        static void UseCriticalSection()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();
            var spinLock = new SpinLock();

            for (int i = 1; i < 10; i++)
            {
                tasks.Clear();

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 1; j <= 1000; j++)
                    {
                        bool token = false;

                        try
                        {
                            spinLock.Enter(ref token);
                            ba.Deposit(1);
                        }
                        finally
                        {
                            if(token)
                            {
                                spinLock.Exit();
                            }
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 1; j <= 1000; j++)
                    {
                        bool token = false;

                        try
                        {
                            spinLock.Enter(ref token);
                            ba.Withdraw(1);
                        }
                        finally
                        {
                            if (token)
                            {
                                spinLock.Exit();
                            }
                        }
                    }
                }));

                Task.WaitAll(tasks.ToArray());
                Console.WriteLine($"{i},Final balance is {ba.Balance}.");

        }

    }
    }
}
