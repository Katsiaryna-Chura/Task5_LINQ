using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Numerics;

namespace Task5_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            FibonacciGenerator generator = new FibonacciGenerator();
            FibonacciAnalyzer analyzer = new FibonacciAnalyzer(generator.GetFibonacciNumbers(200));

            Console.WriteLine(generator.GetViewOfFibonacciNumbers());
            Console.WriteLine();

            List<BigInteger> primes = analyzer.GetPrimeNumbers();
            Console.WriteLine($"Count of prime numbers = {primes.Count}");
            Console.WriteLine("Prime numbers:");
            foreach (var p in primes)
            {
                Console.WriteLine(p);
            }

            List<BigInteger> numbersDivisibleBySumOfDigits = analyzer.GetNumbersDivisibleBySumOfDigits();
            Console.WriteLine($"Count of numbers divisible by the sum of its digits = {numbersDivisibleBySumOfDigits.Count}");
            Console.WriteLine("Numbers divisible by the sum of its digits:");
            foreach (var n in numbersDivisibleBySumOfDigits)
            {
                Console.WriteLine(n);
            }

            Console.WriteLine($"Are exist numbers divisible by five: {analyzer.IsExistNumberDivisibleByFive()}");

            Console.WriteLine("List of square roots of numbers with digit two:");
            List<BigInteger> sqrts = analyzer.GetListOfSqrtsOfNumbersWithDigitTwo();
            foreach (var s in sqrts)
            {
                Console.WriteLine(s);
            }

            List<BigInteger> sortedBySecondDigit = analyzer.SortByDescendingOfSecondDigit();
            Console.WriteLine("List of numbers sorted by descending of the second digit:");
            foreach (var i in sortedBySecondDigit)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine($"Number with max sum of squares of digits = {analyzer.GetNumberWithMaxSumOfSquaresOfDigits()}");

            Console.WriteLine($"Average number of zero digit = {analyzer.CountAverageNumberOfZeroDigit()}");
            List<string> firstTwoDigits = analyzer.GetListOfTwoLastDigitsOfNumbersDivisibleBy3AndHavingNeighboursDivisibleBy5();
            Console.WriteLine("List of two last digits of numbers divisible by 3 and having neighbours divisible by 5:");
            foreach (var n in firstTwoDigits)
            {
                Console.WriteLine(n);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
