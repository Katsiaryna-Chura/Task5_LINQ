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
        public List<BigInteger> FibList { get; private set; }

        public FibonacciAnalyzer(List<BigInteger> fibList)
        {
            this.FibList = new List<BigInteger>();
            this.FibList.AddRange(fibList);
        }

        public List<BigInteger> GetPrimeNumbers()
        {
            return FibList
                .Where((n, i) => n.IsProbablePrime())
                .ToList();
        }

        public List<BigInteger> GetNumbersDivisibleBySumOfDigits()
        {
            return FibList
                .Where(n => (double)n % (double)n.ToString().Sum(c => c - '0') == 0)
                .ToList();
        }

        public bool IsExistNumberDivisibleByFive()
        {
            return FibList
                .Where(n => n % 5 == 0)
                .ToList()
                .Any();
        }

        public List<BigInteger> GetNumbersDivisibleByFive()
        {
            return FibList
                .Where(n => n % 5 == 0)
                .ToList();
        }

        //public List<BigInteger> GetListOfSqrtsOfNumbersWithDigitTwo()
        //{
        //    return fibList
        //        .Where(n => n.ToString().Contains("2"))
        //        .Select(x => (BigInteger)(x.Sqrt()))
        //        .ToList();
        //}

        public Dictionary<BigInteger, BigInteger> GetListOfSqrtsOfNumbersWithDigitTwo()
        {
            return FibList
                .Where(n => n.ToString().Contains("2"))
                .Select(x => new
                {
                    number = x,
                    sqrt = (BigInteger)(x.Sqrt())
                })
                .ToDictionary(s => s.number, s => s.sqrt);
        }

        public List<BigInteger> SortByDescendingOfSecondDigit()
        {
            return FibList
                .OrderByDescending(n => (int)n.ToString().ElementAtOrDefault(1))
                .ToList();
        }

        //public List<string> GetListOfTwoLastDigitsOfNumbersDivisibleBy3AndHavingNeighboursDivisibleBy5()
        //{
        //    return fibList
        //        .Where((n, i) => n % 3 == 0)
        //        .Select((n, i) => new
        //        {
        //            Number = n,
        //            Neighbours = fibList.GetRange((i < 5 ? 0 : i - 5), i < 5 ? i : 5).Concat(fibList.GetRange(i + 1, (i + 6 > fibList.Count ? fibList.Count - i : 5)))
        //        })
        //        .Where(a => a.Neighbours.Any(z => z % 5 == 0))
        //        .Select(a => a.Number.ToString().Substring((a.Number.ToString().Length < 2 ? 0 : a.Number.ToString().Length - 2), (a.Number.ToString().Length < 2 ? a.Number.ToString().Length : 2)))
        //        .ToList();
        //}

        public Dictionary<BigInteger, string> GetListOfTwoLastDigitsOfNumbersDivisibleBy3AndHavingNeighboursDivisibleBy5()
        {
            return FibList
                .Where((n, i) => n % 3 == 0)
                .Select((n, i) => new
                {
                    Number = n,
                    Neighbours = FibList.GetRange((i < 5 ? 0 : i - 5), i < 5 ? i : 5).Concat(FibList.GetRange(i + 1, (i + 6 > FibList.Count ? FibList.Count - i : 5)))
                })
                .Where(a => a.Neighbours.Any(z => z % 5 == 0))
                .Select(a => new {
                    number = a.Number,
                    lastDigits = a.Number.ToString().Substring((a.Number.ToString().Length < 2 ? 0 : a.Number.ToString().Length - 2), (a.Number.ToString().Length < 2 ? a.Number.ToString().Length : 2))
                })
                .ToDictionary(s => s.number, s=> s.lastDigits);
        }

        public BigInteger GetNumberWithMaxSumOfSquaresOfDigits()
        {
            return FibList
                .OrderByDescending(n => n.ToString().Sum(c => (c - '0') * (c - '0')))
                .First();
        }

        public double CountAverageNumberOfZeroDigit()
        {
            return FibList
                .Average(n => n.ToString().Count(c => c == '0'));
        }




        public List<int> GetSumsOfSquaresOfDigits()
        {
            return FibList
                .Select(n => n.ToString().Sum(c => (c - '0') * (c - '0')))
                .OrderByDescending(s => s)
                .ToList();
        }

        public List<BigInteger> SortByDescendingSumOfSquaresOfDigits()
        {
            return FibList
                .OrderByDescending(n => n.ToString().Sum(c => (c - '0') * (c - '0')))
                .ToList();
        }
    }
}
