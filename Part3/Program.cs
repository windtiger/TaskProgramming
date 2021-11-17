using System;
using System.Threading.Tasks;

namespace Part3
{
    class Program
    {
        private static void Test(object obj)
        {
            for (int i = 1; i <= 100; i++)
            {
                Console.WriteLine(obj);
            }

            Console.WriteLine($"{obj} thread end.");
        }

        public static int GetTextLength(object obj)
        {
            return obj.ToString().Length;
        }

        static void Main(string[] args)
        {
            Task.Factory.StartNew(() => Test("test1"));
            Task t = new Task(() => Test("test2"));
            t.Start();

            Task t1 = new Task(Test, "Test3");
            t1.Start();
            Task t2 = new Task(() => Test("Test4"));
            t2.Start();

            var t3 = new Task<int>(() => GetTextLength("Test5"));
            t3.Start();
            Console.WriteLine($"{t3.Result}");
            var t4 = Task.Factory.StartNew<int>(() => GetTextLength("Test6"));
            Console.WriteLine($"{t4.Result}");

            Console.WriteLine("Main thread end.");
            Console.ReadKey();
        }
    }
}
