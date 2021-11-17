using System;
using System.Threading.Tasks;

namespace Part7
{
    class Program
    {
        static void Main(string[] args)
        {
            //NoHandleException();
            //HandleTaskException();

            try
            {
                HandleTaskExceptionByChoose();
            }
            catch (AggregateException ae)
            {
                foreach (var ex in ae.InnerExceptions)
                {
                    Console.WriteLine($"elsewhare handle Exception {ex.GetType()} from {ex.Source}");
                }
            }

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        private static void NoHandleException()
        {
            var t1 = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("InvalidOperationException") { Source = "t1" };
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                throw new FormatException("FormatException") { Source = "t2" };
            });

            Task.WaitAll(t1, t2);
        }

        private static void HandleTaskExceptionByChoose()
        {
            var t1 = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("InvalidOperationException") { Source = "t1" };
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                throw new FormatException("FormatException") { Source = "t2" };
            });

            try
            {
                Task.WaitAll(t1, t2);
            }
            catch (AggregateException ae)
            {
                ae.Handle(ex =>
                {
                    if (ex is FormatException)
                    {
                        Console.WriteLine("Handle FormatException");
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                });

                foreach (var ex in ae.InnerExceptions)
                {
                    Console.WriteLine($"Exception {ex.GetType()} from {ex.Source}");
                }
            }
        }

        private static void HandleTaskException()
        {
            var t1 = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("InvalidOperationException") { Source = "t1" };
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                throw new FormatException("FormatException") { Source = "t2" };
            });

            try
            {
                Task.WaitAll(t1, t2);
            }
            catch (AggregateException ae)
            {
                foreach (var ex in ae.InnerExceptions)
                {
                    Console.WriteLine($"Exception {ex.GetType()} from {ex.Source}");
                }
            }
        }
    }

}
