using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Task5_LINQ
{
    public class Menu
    {
        FibonacciGenerator generator;
        FibonacciAnalyzer analyzer;

        public Menu()
        {
            generator = new FibonacciGenerator();
            analyzer = new FibonacciAnalyzer(generator.GetFibonacciNumbers(200));
        }

        public bool PerformMenuItem(int menuItem)
        {
            switch (menuItem)
            {
                case 1:
                    Console.WriteLine(generator.GetViewOfFibonacciNumbers());
                    break;
                case 2:
                    List<BigInteger> primes = analyzer.GetPrimeNumbers();
                    Console.WriteLine($"Count of prime numbers = {primes.Count}");
                    Console.WriteLine("Prime numbers:");
                    foreach (var p in primes)
                    {
                        Console.WriteLine(p);
                    }
                    break;
                case 3:
                    List<BigInteger> numbersDivisibleBySumOfDigits = analyzer.GetNumbersDivisibleBySumOfDigits();
                    Console.WriteLine($"Count of numbers divisible by the sum of its digits = {numbersDivisibleBySumOfDigits.Count}");
                    Console.WriteLine("Numbers divisible by the sum of its digits:");
                    foreach (var n in numbersDivisibleBySumOfDigits)
                    {
                        Console.WriteLine(n);
                    }
                    break;
                case 4:
                    Console.WriteLine($"Are exist numbers divisible by five: {analyzer.IsExistNumberDivisibleByFive()}");
                    List<BigInteger> numbersDivisibleBy5 = analyzer.GetNumbersDivisibleByFive();
                    Console.WriteLine("Numbers divisible by 5:");
                    foreach (var n in numbersDivisibleBy5)
                    {
                        Console.WriteLine(n);
                    }
                    break;
                case 5:
                    Console.WriteLine("List of rounded square roots of numbers with digit two:");
                    Dictionary<BigInteger, BigInteger> sqrts = analyzer.GetListOfSqrtsOfNumbersWithDigitTwo();
                    foreach (var s in sqrts)
                    {
                        Console.WriteLine($"Number: {s.Key,-45} - Sqrt: {s.Value}");
                    }
                    break;
                case 6:
                    List<BigInteger> sortedBySecondDigit = analyzer.SortByDescendingOfSecondDigit();
                    Console.WriteLine("List of numbers sorted by descending of the second digit:");
                    foreach (var s in sortedBySecondDigit)
                    {
                        Console.WriteLine(s);
                    }
                    break;
                case 7:
                    Dictionary<BigInteger, string> firstTwoDigits = analyzer.GetListOfTwoLastDigitsOfNumbersDivisibleBy3AndHavingNeighboursDivisibleBy5();
                    Console.WriteLine("List of two last digits of numbers divisible by 3 and having neighbours divisible by 5:");
                    foreach (var n in firstTwoDigits)
                    {
                        Console.WriteLine($"{n.Value,-2} - Number: {n.Key}");
                    }
                    break;
                case 8:
                    Console.WriteLine($"Number with max sum of squares of digits = {analyzer.GetNumberWithMaxSumOfSquaresOfDigits()}");
                    List<int> sumsOfSquares = analyzer.GetSumsOfSquaresOfDigits();
                    List<BigInteger> numbers = analyzer.SortByDescendingSumOfSquaresOfDigits();
                    int i = 0;
                    foreach(var s in sumsOfSquares)
                    {
                        Console.WriteLine($"{numbers[i],-45} - Sum of squares of digits: {s}");
                        i++;
                    }
                    break;
                case 9:
                    Console.WriteLine($"Average number of zero digits = {analyzer.CountAverageNumberOfZeroDigit()}");
                    break;
                case 10:
                    return false;
                default:
                    Console.WriteLine("There is no such menu item.");
                    break;
            }
            return true;
        }

        public void ViewMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Select the action to perform with Fibonacci numbers:");
            Console.WriteLine("1 - View Fibonacci numbers");
            Console.WriteLine("2 - View prime numbers");
            Console.WriteLine("3 - View numbers divisible by the sum of its digits");
            Console.WriteLine("4 - View numbers divisible by 5");
            Console.WriteLine("5 - View square roots of numbers containing digit 2");
            Console.WriteLine("6 - View numbers sorted by descending of the second digit");
            Console.WriteLine("7 - View two last digits of numbers divisible by 3 and having neighbours divisible by 5");
            Console.WriteLine("8 - View number with max sum of squares of digits");
            Console.WriteLine("9 - View average number of zero digits");
            Console.WriteLine("10 - exit");
        }

        public int SelectMenuItem()
        {
            int menuItem;
            bool isNumber;
            do
            {
                isNumber = int.TryParse(Console.ReadLine(), out menuItem);
                if (!isNumber)
                {
                    Console.WriteLine("Enter a number please!");
                }
            } while (!isNumber);
            return menuItem;
        }
    }
}
