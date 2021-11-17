using System;
using System.Diagnostics;
using System.Threading;
/// <summary>
/// 2021/09/29
/// ● Part2 : 說明single-thread與multi-thread的不同
///     //single    :一個時間做一件事，做完繼續往下
///     //multi     :將時間切割，做多件事，不會因為某件事卡住
/// ● Part3: 
/// 1.Thread宣告時，若沒用ThreadStart的話，CLR自動幫忙做的事
///     //檢查傳入的 function 參數、回傳型態是否符合標準的 ThreadStart，符合可自動轉
/// 2.宣告ThreadStart的四種方式
///     //1.新物件，以 function 為參數
///     //2.直指 function name
///     //3.匿名 delegate
///     //4.Lambda Action
/// 3.function有參數時該如何宣告Thread，須注意什麼
///     //因為參數只能用 object 傳遞，要小心不能傳入錯誤型態，不然會發生例外
/// ● Part4:  1.Join的作用；Join參數的作用
///     //等待執行緒完成；參數可以指定 time out 時間
///     
/// 2021/10/06
/// ● Part5:  
/// 1.呈現single thread 的執行結果
/// 2.呈現multithrad，沒有lock的執行結果
/// 3.呈現multithrad，有lock的執行結果
/// 4.用2、3差異來說明lock的用意
/// lock 的部分同時間內只允許一個執行緒使用
/// ● Part6: 
/// 1.如何停掉Thread ?
///     //Thread.Abort  //編輯器顯示已不支援
/// 2.priority的5個level,default是哪個level
///     //Highest
///     //AboveNormal
///     //Normal(default)
///     //BelowNormal
///     //Lowest
/// 3.不同priority的差異在哪 ?
///     //可使用多少 CPU 資源
/// 4.有沒有加priorty的差異
///     //高優先權的得到較多資源，同時間內執行進度較高
/// ● Part7: 1.請說明程式結果
///     //以我的測試程式，多執行緒執行速度是單執行緒的兩倍
/// </summary>

namespace ThreadTest
{
    class Program
    {
        static private string TimeStamp { get { return DateTime.Now.ToString("mm:ss.ff"); } }

        static void Main(string[] args)
        {
            //TestThreadStart();
            //TestJoinThread();
            //TestLock_SingleThread();
            //TestLock_MultiThread();
            //TestLock_MultiThreadWithLock();
            //TestPriority();
            TestPerformance();
            Console.ReadKey();
        }

        static void TestThreadStart()
        {
            var ts = new ThreadStart(Print);
            ThreadStart ts2 = Print;
            ThreadStart ts3 = delegate () { Print(); };
            ThreadStart ts4 = () => Print();
            var pts = new ParameterizedThreadStart(Print);
            var t = new Thread(ts);
            t.Start();
            var pt = new Thread(pts);
            pt.Start(3);
        }

        static void TestJoinThread()
        {
            Console.WriteLine("Test Start");
            var ta = new Thread(FuncA);
            var tb = new Thread(FuncB);
            ta.Start();
            tb.Start();
            //Thread.Sleep(1);
            ta.Join();
            Console.WriteLine("Test Completed");
        }

        static void TestLock_SingleThread()
        {
            FuncWithoutLock("eat");
            FuncWithoutLock("walk");
            FuncWithoutLock("sleep");
        }

        static void TestLock_MultiThread()
        {
            new Thread(() => FuncWithoutLock("eat")).Start();
            new Thread(() => FuncWithoutLock("walk")).Start();
            new Thread(() => FuncWithoutLock("sleep")).Start();
        }

        static void TestLock_MultiThreadWithLock()
        {
            var test = new LockObject();
            new Thread(() => test.FuncWithLock("eat")).Start();
            new Thread(() => test.FuncWithLock("walk")).Start();
            new Thread(() => test.FuncWithLock("sleep")).Start();
        }

        static void TestPriority()
        {
            var stop = false;
            var a = 0;
            var b = 0;
            var ta = new Thread(() =>
              {
                  while (!stop) a++;
              });
            var tb = new Thread(() =>
              {
                  while (!stop) b++;
              });
            ta.Priority = ThreadPriority.Highest;
            tb.Priority = ThreadPriority.Lowest;
            ta.Start();
            tb.Start();
            Console.WriteLine("count start");
            Console.ReadKey();
            stop = true;
//#pragma warning disable SYSLIB0006 // 類型或成員已經過時
//            ta.Abort();
//            tb.Abort();
//#pragma warning restore SYSLIB0006 // 類型或成員已經過時
            Console.WriteLine($"a = {a}");
            Console.WriteLine($"b = {b}");
        }

        static void TestPerformance()
        {
            Action<int> cnt = (t) =>
            {
                var i = t;
                while (i-- > 0) ;
            };

            var sw = new Stopwatch();
            var total = 999999999;

            Console.WriteLine("single start");
            sw.Start();
            cnt(total);
            cnt(total);
            sw.Stop();
            Console.WriteLine($"single stop, Elapsed : {sw.ElapsedMilliseconds}");

            var t1 = new Thread(() => cnt(total));
            var t2 = new Thread(() => cnt(total));
            Console.WriteLine("multi start");
            sw.Restart();
            t1.Start(); t2.Start();
            t1.Join(); t2.Join();
            sw.Stop();
            Console.WriteLine($"multi stop, Elapsed : {sw.ElapsedMilliseconds}");
        }







        static void Print()
        {
            Print(5);
        }

        static void Print(object count)
        {
            var i = (int)count;
            while (--i >= 0)
            {
                Console.WriteLine($"{DateTime.Now.ToString("mm:ss.ff")}\ttest\t{(int)count - i}");
            }
        }


        static void FuncA()
        {
            var i = 50;
            while (--i >= 0)
            {
                Console.WriteLine($"{DateTime.Now.ToString("mm:ss.ff")}\t\tFuncA\t{50 - i}");
            }
        }

        static void FuncB()
        {
            var i = 50;
            while (--i >= 0)
            {
                Console.WriteLine($"{DateTime.Now.ToString("mm:ss.ff")}\tFuncB\t{50 - i}");
            }
        }


        static void FuncWithoutLock(string name)
        {
            Console.WriteLine($"{TimeStamp}\t{name}\tstart");
            Thread.Sleep(3333);
            Console.WriteLine($"{TimeStamp}\t{name}\tend");
        }

        class LockObject
        {
            internal void FuncWithLock(string name)
            {
                lock (this)
                {
                    Console.WriteLine($"{TimeStamp}\t{name}\tstart");
                    Thread.Sleep(3333);
                    Console.WriteLine($"{TimeStamp}\t{name}\tend");
                }
            }
        }
    }
}
