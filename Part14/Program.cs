using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Part14
{
    class Program
    {

        static ReaderWriterLockSlim padlock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        static Random random = new Random();

        static void Main(string[] args)
        {
            //ReaderWriterLockTest();
            UpgradeableReadLockTest();
            
            Console.WriteLine("Hello World!");
        }

        static private void ReaderWriterLockTest()
        {
            int x = 0;

            var tasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    padlock.EnterReadLock();

                    Console.WriteLine($"Entered read lock, x = {x}");
                    Thread.Sleep(5000);
                    padlock.ExitReadLock();
                    Console.WriteLine($"Exit read lock, x = {x}");


                }));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e.Message);
                    return true;
                });
            }

            while (true)
            {
                Console.ReadKey();
                padlock.EnterWriteLock();
                Console.Write("write lock acuqired ");
                int newValue = random.Next(11, 20);
                x = newValue;
                Console.WriteLine($"Set x = {x}");
                padlock.ExitWriteLock();
                Console.WriteLine("write lock released");
            }
        }

        static private void UpgradeableReadLockTest()
        {
            int x = 0;

            var tasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    padlock.EnterUpgradeableReadLock();
                    //padlock.EnterReadLock();
                    Console.WriteLine($"Entered read lock, x = {x}");

                    if (i % 2 == 0)
                    {
                        padlock.EnterWriteLock();
                        x = 123;
                        padlock.ExitWriteLock();
                    }

                    Thread.Sleep(5000);

                    padlock.ExitUpgradeableReadLock();
                    //padlock.EnterReadLock();
                    Console.WriteLine($"Exit read lock, x = {x}");
                }));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e.Message);
                    return true;
                });
            }

        }
    }
}
