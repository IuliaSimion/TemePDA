using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPI;

namespace FindElement
{
    class Program
    {
        static void Main(string[] args)
        {
            using(new MPI.Environment(ref args))
            {
                int ntasks, rank;
                int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
                int searchedElem = 12;
                int found = 0;
                int message = 0;
                MPI.CompletedStatus status;
                //MPI.Request req;

                var comm = Communicator.world;
                rank = comm.Rank;
                ntasks = comm.Size;

                for (int i = rank; i < array.Length; i = i + ntasks)
                {
                    if (array[i] == searchedElem)
                    {
                        Console.WriteLine(i);
                        found = 1;
                    }
                }

                comm.Send(found, 0, 1);

                if (rank == 0)
                {
                    for (int i = 0; i < ntasks; i++)
                    {
                        comm.Receive(i, 1, out message, out status);
                        if (message == 1)
                        {
                            found = message;
                        }
                    }
                    if (found != 1)
                        Console.WriteLine("Not found!");
                }

                Console.ReadLine();

            }
        }
    }
}
