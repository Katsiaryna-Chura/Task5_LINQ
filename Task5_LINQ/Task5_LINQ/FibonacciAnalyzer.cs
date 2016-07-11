using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Task5_LINQ
{
    public class FibonacciAnalyzer
    {
        List<BigInteger> fibList;

        public FibonacciAnalyzer()
        {
            fibList = new List<BigInteger>();
            Initialize();
        }

        public void Initialize()
        {
            for (int i = 0; i < 200; i++)
            {
                fibList.Add(Fibonacci(i));
            }
        }

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

        public List<BigInteger> GetPrimeNumbers()
        {
            return fibList
                .Where((n,i) => n.IsFibonacciNumberPrime(i))
                .ToList();
        }

        public List<BigInteger> GetNumbersDivisibleBySumOfDigits()
        {
            return fibList
                .Where(n => (double)n % (double)n.ToString().Sum(c => c - '0') == 0)
                .ToList();
        }

        public bool IsExistNumberDivisibleByFive()
        {
            return fibList
                .Where(n => n % 5 == 0)
                .ToList()
                .Any();
        }

        public List<BigInteger> GetListOfSqrtsOfNumbersWithDigitTwo()
        {
            return fibList
                .Where(n => n.ToString().Contains("2"))
                .Select(x => (BigInteger)(x.Sqrt()))
                .ToList();
        }

        public List<BigInteger> SortByDescendingOfSecondDigit()
        {
            return fibList
                .OrderByDescending(n => (int)n.ToString().ElementAtOrDefault(1))
                .ToList();
        }

        public List<string> GetListOfTwoLastDigitsOfNumbersDivisibleBy3AndHavingNeighboursDivisibleBy5()
        {
            return fibList
                .Where((n, i) => n % 3 == 0)
                .Select((n, i) => new
                {
                    Number = n,
                    Neighbours = fibList.GetRange((i < 5 ? 0 : i - 5), i < 5 ? i : 5).Concat(fibList.GetRange(i + 1, (i + 6 > fibList.Count ? fibList.Count - i : 5)))
                })
                .Where(a => a.Neighbours.Any(z => z % 5 == 0))
                .Select(a => a.Number.ToString().Substring((a.Number.ToString().Length < 2 ? 0 : a.Number.ToString().Length - 2), (a.Number.ToString().Length < 2 ? a.Number.ToString().Length : 2)))
                .ToList();
        }

        public BigInteger GetNumberWithMaxSumOfSquaresOfDigits()
        {
            return fibList
                .OrderByDescending(n => n.ToString().Sum(c => (c - '0') * (c - '0')))
                .First();
        }

        public double CountAverageNumberOfZeroDigit()
        {
            return fibList
                .Average(n => n.ToString().Count(c => c == '0'));
        }

        public string GetListOfFibonacciNumbers()
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
