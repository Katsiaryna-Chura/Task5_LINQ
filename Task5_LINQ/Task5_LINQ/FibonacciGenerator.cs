using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Task5_LINQ
{
    class FibonacciGenerator
    {
        List<BigInteger> fibList;

        public BigInteger Fibonacci(int n)
        {
            int oldN = 0;
            if (n < 0)
            {
                oldN = n;
                n = -n;
            }
            BigInteger a = BigInteger.Zero;
            BigInteger b = BigInteger.One;
            for (int i = 31; i >= 0; i--)
            {
                BigInteger d = a * (b * 2 - a);
                BigInteger e = a * a + b * b;
                a = d;
                b = e;
                if ((((uint)n >> i) & 1) != 0)
                {
                    BigInteger c = a + b;
                    a = b;
                    b = c;
                }
            }
            if ((oldN < 0) && (oldN % 2 != 0))
            {
                return -a;
            }
            return a;
        }

        public List<BigInteger> GetFibonacciNumbers(int n)
        {
            fibList = new List<BigInteger>();
            for (int i = 0; i < 200; i++)
            {
                fibList.Add(Fibonacci(i));
            }
            return fibList;
        }

        public string GetViewOfFibonacciNumbers()
        {
            StringBuilder fibNumbers = new StringBuilder();
            fibNumbers.Append("List of Fibonacci numbers:\n");
            foreach (var fibNumber in fibList)
            {
                fibNumbers.Append($"{fibNumber}\n");
            }
            return fibNumbers.ToString();
        }
    }
}
