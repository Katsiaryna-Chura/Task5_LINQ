using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Task5_LINQ
{
    public static class BigIntegerExtensions
    {
        public static bool IsPrime(this BigInteger n)
        {
            if (n < 2) return false;
            if (n % 2 == 0) return (n == 2);
            double root = n.Sqrt();
            for (BigInteger i = 3; (double)i <= root; i += 2)
            {
                if (n % i == 0)
                    return false;
            }
            return true;
        }

        public static bool IsFibonacciNumberPrime(this BigInteger n, BigInteger index)
        {
            if (!index.IsPrime())
                return false;
            else
                return n.IsPrime();
        }

        public static double Sqrt(this BigInteger n)
        {
            return Math.Exp(BigInteger.Log(n) / 2);
        }
    }
}
