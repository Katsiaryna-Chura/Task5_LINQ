using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5_LINQtoXML;
using System.Xml.Linq;

namespace LinqToXml
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomersInfoAnalyzer a = new CustomersInfoAnalyzer(Data.Path);
            //List<XElement> customersWithOrders = a.GetCustomersWithSumOfOrdersBiggerThanX(100000);
            //foreach (var c in customersWithOrders)
            //{
            //    Console.WriteLine(c);
            //}
            //int count1 = a.GetCustomersWithSumOfOrdersBiggerThanX(104875).Count;
            //int count2 = a.GetCustomersWithSumOfOrdersBiggerThanX(104874).Count;
            //Console.WriteLine($"{count1} - {count2}");

            //var dict = a.GetCustomersGroupedByCountry();
            //foreach (var item in dict)
            //{
            //    Console.WriteLine($"Key:{item.Key} - Value:{item.Value.Count}");
            //    foreach (var c in item.Value)
            //    {
            //        Console.WriteLine($"Customer - {c.Element("name").Value}");
            //    }
            //}

            //List<XElement> customersWithAnyOrder = a.FindCustomersWithAnyOrderBiggerThanX(9000);
            //foreach (var c in customersWithAnyOrder)
            //{
            //    Console.WriteLine(c);
            //}
            //int count3 = a.FindCustomersWithAnyOrderBiggerThanX(15810).Count;
            //int count4 = a.FindCustomersWithAnyOrderBiggerThanX(15809).Count;
            //Console.WriteLine($"{count3} - {count4}");


            //var custWithDates = a.GetCustomersWithStartDates();
            //var dates = custWithDates.Select(d => d.Value).ToList();
            //Console.WriteLine(dates.Where(d=>Convert.ToDateTime(d) == DateTime.MinValue).Count());
            //foreach (var customer in custWithDates)
            //{
            //    Console.WriteLine($"Customer:{customer.Key.Element("name").Value,-50} - Start date: {customer.Value}");
            //    Console.WriteLine(Convert.ToDateTime(customer.Value) == DateTime.MinValue);
            //}

            //var dictionary = a.GetSortedListOfCustomers();
            //foreach (var item in dictionary)
            //    Console.WriteLine($"{item.Key.Element("name").Value,-30} - {item.Value}");
            //var list = a.GetSortedListOfCustomers();
            //for(int i =0;i<7;i++)
            //{
            //    Console.WriteLine(list[i]);
            //    Console.WriteLine();
            //    Console.WriteLine();
            //}

            //var list = a.GetCustomersWithIncompleteInfo();
            //foreach (var item in list)
            //    Console.WriteLine(item);

            //var d = a.GetCitiesAverageProfitability();
            //Console.WriteLine("---profitability---");
            //foreach (var item in d)
            //    Console.WriteLine($"Key: {item.Key} - Value: {item.Value}");

            //var d2 = a.GetCitiesAverageIntensity();
            //Console.WriteLine("---intensity---");
            //foreach (var item in d2)
            //    Console.WriteLine($"Key: {item.Key} - Value: {item.Value}");

            //var d3 = a.GetCustomersActivityByMonthsOfYears();
            //Console.WriteLine("---activity---");
            //foreach (var item in d3)
            //{
            //    Console.WriteLine($"Key: {item.Key:0000} - Value: {item.Value}");
            //}

            Console.ReadLine();
        }
    }
}
