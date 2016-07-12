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
        public List<XElement> Customers { get; private set; }
        public List<XElement> OrdersList { get; private set; }
        NumberFormatInfo nfi;

        public CustomersInfoAnalyzer(string filePath)
        {
            nfi = CultureInfo.CurrentCulture.NumberFormat;
            XDocument document = XDocument.Load(filePath);
            Customers = document.Root.Elements().ToList();
            OrdersList = new List<XElement>();
            OrderListInitialize();
        }

        public void OrderListInitialize()
        {
            foreach (var customer in Customers)
            {
                OrdersList.AddRange(customer.Element("orders").Elements("order"));
            }
        }

        public List<XElement> GetCustomersWithSumOfOrdersBiggerThanX(double x)
        {
            return Customers
                .Where(c => c.Element("orders").Elements("order").Sum(order => double.Parse(order.Element("total").Value.Replace(',', '.').Replace('.', nfi.NumberDecimalSeparator[0]))) > x)
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
                .Where(c => c.Element("orders").Elements("order").Any(order => (double.Parse(order.Element("total").Value.Replace(',', '.').Replace('.', nfi.NumberDecimalSeparator[0]))) > x))
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
                g => $"{g.StartDate.Year:0000}-{g.StartDate.Month:00}"
                //g => g.StartDate == DateTime.MinValue ? "this customer hasn't made orders yet" : $"{g.StartDate.Month:00}-{g.StartDate.Year:0000}"
                );

        }

        public List<XElement> GetSortedListOfCustomers()
        {
            return Customers.Select(c => new
            {
                Customer = c,
                StartDate = Convert.ToDateTime(c.Element("orders").Elements("order").FirstOrDefault()?.Element("orderdate")?.Value),
                SumOfOrders = c.Element("orders").Elements("order").Sum(order => double.Parse(order.Element("total").Value.Replace(',', '.').Replace('.', nfi.NumberDecimalSeparator[0])))
            })
           .OrderBy(info => info.StartDate.Year)
           .ThenBy(info => info.StartDate.Month)
           .ThenByDescending(info => info.SumOfOrders)
           .ThenBy(info => info.Customer.Element("name").Value)
           .Select(info => info.Customer)
           .ToList();

        }

        //public Dictionary<XElement, string> GetSortedListOfCustomers()
        //{
        //    return Customers.Select(c => new
        //    {
        //        Customer = c,
        //        StartDate = Convert.ToDateTime(c.Element("orders").Elements("order").FirstOrDefault()?.Element("orderdate")?.Value),
        //        SumOfOrders = c.Element("orders").Elements("order").Sum(order => double.Parse(order.Element("total").Value.Replace(',', '.').Replace('.', nfi.NumberDecimalSeparator[0])))
        //    })
        //    .OrderBy(info => info.StartDate.Year)
        //    .ThenBy(info => info.StartDate.Month)
        //    .ThenByDescending(info => info.SumOfOrders)
        //    .ThenBy(info => info.Customer.Element("name").Value)
        //    .Select(info => new
        //    {
        //        customer = info.Customer,
        //        description = $"{info.Customer.Element("name").Value,-40} StartDate: {info.StartDate,-15:yyyy-MM} Sum of orders = {info.SumOfOrders}"
        //    })
        //    .ToDictionary(
        //        s => s.customer,
        //        s => s.description
        //        );
        //}

        public List<XElement> GetCustomersWithIncompleteInfo()
        {
            return Customers
                .Where(c =>
                c.Element("postalcode") == null
                || !c.Element("postalcode").Value.All(char.IsDigit)
                || c.Element("region") == null
                || (!(c.Element("phone").Value.Contains('(') && c.Element("phone").Value.Contains('('))))
                .ToList();
        }

        public Dictionary<string, double> GetCitiesAverageProfitability()
        {
            return Customers
                .GroupBy(c => c.Element("city").Value)
                .ToDictionary(
                g => g.Key,
                g => g.ToList().Average(c => c.Element("orders").Elements("order").Sum(order => double.Parse(order.Element("total").Value.Replace(',', '.').Replace('.', nfi.NumberDecimalSeparator[0]))))
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
            return OrdersList
                .GroupBy(order => Convert.ToDateTime(order?.Element("orderdate")?.Value).Date.ToString("yyyy-MM"))
                .OrderBy(g => g.Key)
                .ToDictionary(
                g => g.Key,
                g => g.ToList().Count
                );
        }
    }
}
