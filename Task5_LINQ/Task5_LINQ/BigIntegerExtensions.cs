using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;

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

        //public static bool IsFibonacciNumberPrime(this BigInteger n, BigInteger index)
        //{
        //    if (!index.IsPrime())
        //        return false;
        //    else
        //        return n.IsPrime();
        //}

        public static double Sqrt(this BigInteger n)
        {
            return Math.Exp(BigInteger.Log(n) / 2);
        }

        public static bool IsProbablePrime(this BigInteger source, int certainty = 15)
        {
            if (source == 2 || source == 3)
                return true;
            if (source < 2 || source % 2 == 0)
                return false;

            BigInteger d = source - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            // There is no built-in method for generating random BigInteger values.
            // Instead, random BigIntegers are constructed from randomly generated
            // byte arrays of the same length as the source.
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[source.ToByteArray().LongLength];
            BigInteger a;

            for (int i = 0; i < certainty; i++)
            {
                do
                {
                    rng.GetBytes(bytes);
                    a = new BigInteger(bytes);
                }
                while (a < 2 || a >= source - 2);

                BigInteger x = BigInteger.ModPow(a, d, source);
                if (x == 1 || x == source - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, source);
                    if (x == 1)
                        return false;
                    if (x == source - 1)
                        break;
                }

                if (x != source - 1)
                    return false;
            }

            return true;
        }
    }
}
