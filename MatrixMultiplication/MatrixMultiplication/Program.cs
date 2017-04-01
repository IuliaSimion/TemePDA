using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMultiplication
{
    class Program
    {
        static void Main(string[] args)
        {
            
            int n, m, o;
            n = Convert.ToInt32(Console.ReadLine());
            m = Convert.ToInt32(Console.ReadLine());
            o = Convert.ToInt32(Console.ReadLine());
            long[,] matA = new long[n, m];
            long[,] matB = new long[m, o];
            long[,] matC = new long[n, o];

            var rnd = new Random(int.Parse(DateTime.Now.ToString("HHmmssfff")) + (int)(n * o));

            Console.WriteLine("First matrix is: ");
            for (long i = 0; i < n; ++i)
            {
                for (long j = 0; j < m; ++j)
                {
                    matA[i, j] = rnd.Next(0, n * o);
                    Console.Write(matA[i, j] + "\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Second matrix is: ");
            for (long i = 0; i < m; ++i)
            {
                for (long j = 0; j < o; ++j)
                {
                    matB[i, j] = rnd.Next(0, n * o);
                    Console.Write(matB[i, j] + "\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine("The result is:");

            Task[] tasks = new Task[n];

            for (long i = 0; i < n; ++i)
            {
                var multiplication = new Task((parameter) =>
                {
                    long ii = (long)parameter;
                    for (long jj = 0; jj < o; ++jj)
                    {
                        for (long k = 0; k < m; ++k)
                        {
                            matC[ii, jj] += matA[ii, k] * matB[k, jj];

                        }
                    }
                },
                i);
                tasks[i] = multiplication;
                multiplication.Start();
            }

            Task.WaitAll(tasks);

            for(long i = 0; i < n; ++i)
            {
                for(long j = 0; j < o; ++j)
                {
                    Console.Write(matC[i, j] + "\t");
                }
                Console.WriteLine();
            }

            Console.ReadLine();

        }
    }
}