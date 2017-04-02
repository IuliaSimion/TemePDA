using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public class Producer
    {
        Queue<string> queue;
        int queue_size;
        Object lockObject;
        List<string> listOfPossibleProducts;
        public Producer(Queue<string> queue, int queue_size, Object lockObject, List<string> listOfPossibleProducts)
        {
            this.queue = queue;
            this.queue_size = queue_size;
            this.lockObject = lockObject;
            this.listOfPossibleProducts = listOfPossibleProducts;
        }

        public void produce()
        {
            foreach (string item in listOfPossibleProducts)
            {
                lock (lockObject)
                {
                    while (queue.Count == queue_size){
                        Monitor.Wait(lockObject);
                    }
                    queue.Enqueue(item);
                    Console.WriteLine("Producing {0}", item);
                    //if (queue.Count > 0){
                        Monitor.PulseAll(lockObject);
                    //}
                }
            }
        }
    }

}


