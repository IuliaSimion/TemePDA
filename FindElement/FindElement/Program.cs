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
                int aux = 0;               

                var comm = Communicator.world;
                rank = comm.Rank;
                ntasks = comm.Size;

                for (int i = rank; i < array.Length; i = i + ntasks)
                {
                    if (array[i] == searchedElem)
                    {
                        Console.WriteLine("Found on position:" + i);
                        found = 1;
                    }
                }

                comm.Send(found, 0, 1);

                if (rank == 0)
                {
                    for (int i = 0; i < ntasks; i++)
                    {
                       aux = comm.Receive<int>(i, 1);
                        if (aux == 1)
                        {
                            found = aux;
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
