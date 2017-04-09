using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPI;

namespace FindElemScatter
{
    class Program
    {
        static void Main(string[] args)
        {
            using (new MPI.Environment(ref args))
            {
                int ntasks, rank;
                int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 12 };
                int searchedElem = 12;
                int found = -1;
                //int max = -1;



                var comm = Communicator.world;
                rank = comm.Rank;
                ntasks = comm.Size;

                comm.Scatter(array, 0);

                for (int i = rank; i < array.Length; i = i + ntasks)
                {
                    if (array[i] == searchedElem)
                    {
                        found = i;
                        //if (found > max)
                        //    max = found;
                    }
                }

                comm.Gather(found, 0);
                

                if (rank == 0)
                {

                    if (found < 0)
                        Console.WriteLine("Not found!");
                    else
                        Console.WriteLine("Last position: " + found);
                }

                Console.ReadLine();

            }
        }
    }
}
