using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPI;

namespace MatrixMultMPI
{
    class Program
    {
        static void Main(string[] args)
        {

            using (new MPI.Environment(ref args))
            {
                int rank, ntasks;
                var comm = Communicator.world;
                rank = comm.Rank;
                ntasks = comm.Size;

                int n, m, o;
                n = Convert.ToInt32(Console.ReadLine());
                m = Convert.ToInt32(Console.ReadLine());
                o = Convert.ToInt32(Console.ReadLine());
                long[,] matA = new long[n, m];
                long[,] matB = new long[m, o];
                long[,] matC = new long[n, o];
                long[,] result = new long[n, o];

                var rnd = new Random(int.Parse(DateTime.Now.ToString("HHmmssfff")) + (int)(n * o));
                if (rank == 0)
                {
                    Console.WriteLine("First matrix is: ");
                    for (long i = 0; i < n; ++i)
                    {
                        for (long j = 0; j < m; ++j)
                        {
                            matA[i, j] = rnd.Next(0, 100);
                            Console.Write(matA[i, j] + "\t");
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine("Second matrix is: ");
                    for (long i = 0; i < m; ++i)
                    {
                        for (long j = 0; j < o; ++j)
                        {
                            matB[i, j] = rnd.Next(0, 100);
                            Console.Write(matB[i, j] + "\t");
                        }
                        Console.WriteLine();
                    }

                    for (long i = 0; i < n; i++)
                    {
                        for (long j = 0; i < o; j++)
                        {
                            matC[i, j] = 0;
                        }
                    }
                }

                comm.Broadcast(ref matA[n, m], 0);
                comm.Broadcast(ref matB[m, o], 0);
                comm.Broadcast(ref matC[n, o], 0);

                for (long k = rank; k < n; k = k + ntasks)
                {
                    for (long j = 0; j < o; j++)
                    {
                        for (long i = 0; i < m; i++)
                        {
                            matC[k, j] = matC[k, j] + matA[k, i] * matB[i, j];
                        }

                    }
                }

                comm.Reduce(matC[n, o], Operation<long>.Add, 0);



                Console.WriteLine("The result is:");


                for (long i = 0; i < n; ++i)
                {
                    for (long j = 0; j < o; ++j)
                    {
                        Console.Write(matC[i, j] + "\t");
                    }
                    Console.WriteLine();
                }

                Console.ReadLine();
            }
          
        }
    }
}