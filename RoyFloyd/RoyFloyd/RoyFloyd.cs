using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPI;

namespace RoyFloyd
{
    public class RoyFloyd
    {
        public const int INF = 99999;

        private static void Print(int[,] distance, int verticesCount, int rank)
        {
            Console.WriteLine("Shortest distances between every pair of vertices:");

            for (int i = 0; i < verticesCount; ++i)
            {
                for (int j = 0; j < verticesCount; ++j)
                {
                    if (rank == 0)
                    {
                        if (distance[i, j] == INF)
                            Console.Write("INF".PadLeft(7));
                        else
                            Console.Write(distance[i, j].ToString().PadLeft(7));
                    }
                }

                Console.WriteLine();
            }
        }

        public static void FloydWarshall(int[,] graph, int verticesCount, int rank, int ntasks, Intracommunicator comm)
        {
            int[,] distance = new int[verticesCount, verticesCount];


            for (int i = rank; i < verticesCount; i += ntasks)
                for (int j = 0; j < verticesCount; ++j)
                    distance[i, j] = graph[i, j];

            for (int k = 0; k < verticesCount; ++k)
            {
                for (int i = rank; i < verticesCount; i += ntasks)
                {
                    for (int j = 0; j < verticesCount; ++j)
                    {
                        if (distance[i, k] + distance[k, j] < distance[i, j])
                            distance[i, j] = distance[i, k] + distance[k, j];
                    }
                }
            }

            comm.Gather<int[,]>(distance, 0);
            Print(distance, verticesCount, rank);
        }
    }
}
