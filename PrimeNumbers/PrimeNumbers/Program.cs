using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPI;

namespace PrimeNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            using(new MPI.Environment(ref args))
            {
                int n, mystart, stride, rank, ntasks;
                int LIMIT = 25;
                Prime prime = new Prime();
                var comm = Communicator.world;
                rank = comm.Rank;
                ntasks = comm.Size;

                mystart = (rank * 2) + 1;
                stride = ntasks * 2;

                if (rank == 0)
                {
                    for (n = mystart; n <= LIMIT; n = n + stride)
                    {
                        prime.IsPrime(n);

                    }
                }

                Console.WriteLine("Finished");


                if (rank > 0)
                {
                    for (n = mystart; n <= LIMIT; n = n + stride)
                        prime.IsPrime(n);

                }

                Console.ReadLine();
            }
            
        }
    }
}
