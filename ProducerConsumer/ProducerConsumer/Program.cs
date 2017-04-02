using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Object lockObj = new object();
            List<string> listOfPossibleProducts = new List<string>();
            listOfPossibleProducts.Add("Orange juice");
            listOfPossibleProducts.Add("Lemonade");
            listOfPossibleProducts.Add("Soda");
            listOfPossibleProducts.Add("Sprite");
            listOfPossibleProducts.Add("Fanta");

            Queue<string> queue = new Queue<string>();
            int queue_size = 3;
            Producer p = new Producer(queue, queue_size, lockObj, listOfPossibleProducts);
            Consumer c1 = new Consumer(queue, lockObj, "c1");
            Consumer c2 = new Consumer(queue, lockObj, "c2");

            Thread t1 = new Thread(c1.consume);
            Thread t2 = new Thread(c2.consume);
            t1.Start();
            t2.Start();

            Thread t = new Thread(p.produce);
            t.Start();

            Console.ReadLine();
        }
    }
}

