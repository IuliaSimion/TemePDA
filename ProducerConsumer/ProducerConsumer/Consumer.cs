using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public class Consumer
    {
        Queue<string> queue;
        Object lockObject;
        string name;
        public Consumer(Queue<string> queue, Object lockObject, string name)
        {
            this.queue = queue;
            this.lockObject = lockObject;
            this.name = name;
        }

        public void consume()
        {
            string item;
            while (true)
            {
                lock (lockObject)
                {
                    while (queue.Count == 0)
                    {
                        Monitor.Wait(lockObject);
                    }
                    item = queue.Dequeue();
                    Console.WriteLine(" {0} Consuming {1}", name, item);
                }
            }
        }
    }
}
