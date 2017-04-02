using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumbers
{
    public class Prime
    {
       public void IsPrime(int n)
        {
            int i, count = 0;

            for (i = 1; i <= n / 2; i++)
            {
                if (n % i == 0)
                    count++;
            }

            if (count == 1)
                Console.WriteLine(n);

        }

    }
}
