using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task5_LINQtoXML
{
    public class CustomersInfoAnalyzer
    {
        public List<XElement> Customers { get; set; }
        public List<XElement> OrdersList { get; set; }
        NumberFormatInfo nfi;

        public CustomersInfoAnalyzer(string filePath)
        {
            XDocument document = XDocument.Load(filePath);
            Customers = document.Root.Elements().ToList();
            OrdersList = new List<XElement>();
            nfi = CultureInfo.CurrentCulture.NumberFormat;
        }

        private void OrderListInitialize()
        {
            OrdersList.Clear();
            foreach (var customer in Customers)
            {
                OrdersList.AddRange(customer.Element("orders").Elements("order"));
            }
        }
        
        private double GetTotalValueOfOrder(XElement order)
        {
            return double.Parse(order.Element("total").Value.Replace(',', '.').Replace('.', nfi.NumberDecimalSeparator[0]));
        }

        private double GetSumOfCustomerOrders(XElement customer)
        {
            return customer.Element("orders").Elements("order").Sum(order => GetTotalValueOfOrder(order));
        }

        public List<XElement> GetCustomersWithSumOfOrdersBiggerThanX(double x)
        {
            return Customers
                .Where(c => GetSumOfCustomerOrders(c) > x)
                .ToList();
        }

        public Dictionary<string, List<XElement>> GetCustomersGroupedByCountry()
        {
            return Customers
                .GroupBy(c => c.Element("country").Value)
                .OrderBy(g => g.Key)
                .ToDictionary(
                g => g.Key,
                g => g.ToList()
                );
        }

        public List<XElement> FindCustomersWithAnyOrderBiggerThanX(double x)
        {
            return Customers
                .Where(c => c.Element("orders").Elements("order").Any(order => GetTotalValueOfOrder(order) > x))
                .ToList();
        }

        public Dictionary<XElement, string> GetCustomersWithStartDates()
        {
            return Customers.Select(c => new
            {
                Customer = c,
                StartDate = Convert.ToDateTime(c.Element("orders").Elements("order").FirstOrDefault()?.Element("orderdate")?.Value)
            })
            .ToDictionary(
                g => g.Customer,
                g => g.StartDate.ToString("yyyy-MM")
                //g => g.StartDate == DateTime.MinValue ? "this customer hasn't made orders yet" : g.StartDate.ToString("yyyy-MM")
                );

        }

        public List<XElement> GetSortedListOfCustomers()
        {
            return Customers.Select(c => new
            {
                Customer = c,
                StartDate = Convert.ToDateTime(c.Element("orders").Elements("order").FirstOrDefault()?.Element("orderdate")?.Value),
                SumOfOrders = GetSumOfCustomerOrders(c)
            })
           .OrderBy(info => info.StartDate.Year)
           .ThenBy(info => info.StartDate.Month)
           .ThenByDescending(info => info.SumOfOrders)
           .ThenBy(info => info.Customer.Element("name").Value)
           .Select(info => info.Customer)
           .ToList();

        }

        public List<XElement> GetCustomersWithIncompleteInfo()
        {
            return Customers
                .Where(c =>
                c.Element("postalcode") == null
                || !c.Element("postalcode").Value.Trim().All(char.IsDigit)
                || c.Element("region") == null
                || (!(c.Element("phone").Value.Contains('(') && c.Element("phone").Value.Contains(')'))))
                .ToList();
        }

        public Dictionary<string, double> GetCitiesAverageProfitability()
        {
            return Customers
                .GroupBy(c => c.Element("city").Value)
                .ToDictionary(
                g => g.Key,
                g => (g.ToList().Sum(c => GetSumOfCustomerOrders(c))) / (double)(g.ToList().Sum(c => c.Element("orders").Elements("order").Count()))
                );
        }

        public Dictionary<string, double> GetCitiesAverageIntensity()
        {
            return Customers
                .GroupBy(c => c.Element("city").Value)
                .ToDictionary(
                g => g.Key,
                g => g.ToList().Average(c => c.Element("orders").Elements("order").Count())
                );
        }

        public Dictionary<int, int> GetCustomersActivityByMonths()
        {
            OrderListInitialize();
            return OrdersList
                .GroupBy(order => Convert.ToDateTime(order?.Element("orderdate")?.Value).Month)
                .OrderBy(g => g.Key)
                .ToDictionary(
                g => g.Key,
                g => g.ToList().Count
                );
        }

        public Dictionary<int, int> GetCustomersActivityByYears()
        {
            OrderListInitialize();
            return OrdersList
                .GroupBy(order => Convert.ToDateTime(order?.Element("orderdate")?.Value).Year)
                .OrderBy(g => g.Key)
                .ToDictionary(
                g => g.Key,
                g => g.ToList().Count
                );
        }

        public Dictionary<string, int> GetCustomersActivityByMonthsOfYears()
        {
            OrderListInitialize();
            return OrdersList
                .GroupBy(order => Convert.ToDateTime(order?.Element("orderdate")?.Value).Date.ToString("yyyy-MM"))
                .OrderBy(g => g.Key)
                .ToDictionary(
                g => g.Key,
                g => g.ToList().Count
                );
        }

        //public void GetSumsOfOrders()
        //{
        //    List<double> sums = new List<double>();
        //    foreach(var c in Customers)
        //        sums.Add(c.Element("orders").Elements("order").Sum(order => GetTotalValueOfOrder(order)));
        //    sums = sums.OrderBy(sum => sum).ToList();
        //    foreach(var s in sums)
        //        Console.WriteLine(s);
        //}
    }
}
