using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPI;

namespace RoyFloyd
{
    class Program
    {
        static void Main(string[] args)
        {
            

            int INF = RoyFloyd.INF;
            int[,] graph = {
            { 0,   5,  INF, 10 },
            { INF, 0,   3, INF },
            { INF, INF, 0,   1 },
            { INF, INF, INF, 0 }};

            using (new MPI.Environment(ref args))
            {
                int ntasks, rank;
                Intracommunicator comm = Communicator.world;
                rank = comm.Rank;
                ntasks = comm.Size;

                RoyFloyd.FloydWarshall(graph, 4, rank, ntasks, comm);

                Console.ReadLine();
            }
        }
    }
}
