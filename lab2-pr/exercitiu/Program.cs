using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace exercitiu
{
    class Program
    {
        private static ManualResetEvent thread23Step = new ManualResetEvent(false);
        private static ManualResetEvent thread56Step = new ManualResetEvent(false);
        private static ManualResetEvent thread47Step = new ManualResetEvent(false);
        

        static void Main(string[] args)
        {
            Thread[] threads = new Thread[7];

            RandomNumber index = new RandomNumber();

            for (int iteration = 0; iteration < 10; iteration++)
            {
                if(iteration != 0)
                    Console.WriteLine("\n");
                Console.WriteLine("Iteration: " + (iteration+1));


                threads[0] = new Thread(print1);
                threads[1] = new Thread(print2);
                threads[2] = new Thread(print3);
                threads[3] = new Thread(print4);
                threads[4] = new Thread(print5);
                threads[5] = new Thread(print6);
                threads[6] = new Thread(print7);

                thread23Step = new ManualResetEvent(false);
                thread56Step = new ManualResetEvent(false);
                thread47Step = new ManualResetEvent(false);


                for (int i = 0; i < 7; i++)
                {
                    index.NewNumber();
                    threads[index.getNumber()].Start();
                }
                
                index.Empty();
                Console.ReadKey();
            }
            
            Console.WriteLine("\nFinish!");
            Console.ReadKey();
        }

        public static void print1()
        {
            Console.Write("1 ");
            thread23Step.Set();
        }

        public static void print2()
        {
            thread23Step.WaitOne();
            Console.Write("2 ");
            //thread23Step.Set();
            thread56Step.Set();
        }

        public static void print3()
        {
            thread23Step.WaitOne();
            Console.Write("3 ");
            //thread23Step.Set();
            thread47Step.Set();
        }

        public static void print4()
        {
            thread47Step.WaitOne();
            Console.Write("4 ");
            //thread47Step.Set();
        }

        public static void print5()
        {
            thread56Step.WaitOne();
            Console.Write("5 ");
            //thread56Step.Set();
        }

        public static void print6()
        {
            thread56Step.WaitOne();
            Console.Write("6 ");
            //thread56Step.Set();
        }

        public static void print7()
        {
            thread47Step.WaitOne();
            Console.Write("7 ");
            //thread47Step.Set();
        }

    }

    // Semaphore, AutoResetEvent, ManualResetEvent
}
