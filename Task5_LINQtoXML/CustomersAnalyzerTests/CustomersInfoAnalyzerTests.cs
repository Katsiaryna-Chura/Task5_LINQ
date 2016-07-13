using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.Collections.Generic;
using Task5_LINQtoXML;
using System.Linq;

namespace CustomersAnalyzerTests
{
    [TestClass]
    public class CustomersInfoAnalyzerTests
    {
        CustomersInfoAnalyzer analyzer;

        [TestInitialize]
        public void ClassInitialize()
        {
            analyzer = new CustomersInfoAnalyzer(CustomersFileInfo.Path);
        }

        //task 1
        [TestMethod]
        public void GetCustomersWithSumOfOrdersBiggerThanXTest()
        {
            int customersNumber1 = analyzer.GetCustomersWithSumOfOrdersBiggerThanX(4272).Count;
            analyzer.Customers.RemoveAt(0);
            int customersNumber2 = analyzer.GetCustomersWithSumOfOrdersBiggerThanX(4272).Count;
            Assert.IsTrue(customersNumber2 == customersNumber1 - 1);

            //Assert.IsTrue(analyzer.GetCustomersWithSumOfOrdersBiggerThanX(104874).Count > analyzer.GetCustomersWithSumOfOrdersBiggerThanX(104875).Count);
        }

        //task 2
        [TestMethod]
        public void GetCustomersGroupedByCountryTest()
        {
            int countryCustomersNumber1 = analyzer.GetCustomersGroupedByCountry()["Germany"].Count;
            analyzer.Customers.RemoveAt(0);
            int countryCustomersNumber2 = analyzer.GetCustomersGroupedByCountry()["Germany"].Count;
            Assert.IsTrue(countryCustomersNumber2 == countryCustomersNumber1 - 1);

            //List<XElement> countryCustomers = analyzer.GetCustomersGroupedByCountry()["Germany"];
            //List<XElement> otherCustomers = analyzer.Customers.Except(countryCustomers).ToList();
            //Assert.IsTrue(otherCustomers.All(c => c.Element("country").Value != "Germany"));
        }

        //task 3
        [TestMethod]
        public void FindCustomersWithAnyOrderBiggerThanXTest()
        {
            int customersNumber1 = analyzer.FindCustomersWithAnyOrderBiggerThanX(933).Count;
            analyzer.Customers.RemoveAt(0);
            int customersNumber2 = analyzer.FindCustomersWithAnyOrderBiggerThanX(933).Count;
            Assert.IsTrue(customersNumber2 == customersNumber1 - 1);

            //Assert.IsTrue(analyzer.FindCustomersWithAnyOrderBiggerThanX(15809).Count > analyzer.FindCustomersWithAnyOrderBiggerThanX(15810).Count);
        }

        //task 4 ?
        [TestMethod]
        public void GetCustomersWithStartDatesTest()
        {
            Dictionary<XElement, string> customersWithDates = analyzer.GetCustomersWithStartDates();
            List<string> dates = customersWithDates.Select(d => d.Value).ToList();
            Assert.IsTrue(dates.Where(d => Convert.ToDateTime(d) == DateTime.MinValue).Count() == 2);
        }

        //task 5 ?
        [TestMethod]
        public void GetSortedListOfCustomersTest()
        {
            List<XElement> sortedCustomers = analyzer.GetSortedListOfCustomers();
            double sumOfOrders1 = sortedCustomers.First().Element("orders").Elements("order").Sum(order => double.Parse(order.Element("total").Value));
            analyzer.Customers.Remove(sortedCustomers.ElementAt(0));
            analyzer.Customers.Remove(sortedCustomers.ElementAt(1));
            List<XElement> sortedCustomers2 = analyzer.GetSortedListOfCustomers();
            double sumOfOrders2 = sortedCustomers2.First().Element("orders").Elements("order").Sum(order => double.Parse(order.Element("total").Value));
            Assert.IsTrue(sumOfOrders1 == 0 && sumOfOrders2 > 0);

            //List<XElement> sortedCustomers = analyzer.GetSortedListOfCustomers();
            //CollectionAssert.AreNotEqual(analyzer.Customers, sortedCustomers);
        }

        //task 6
        [TestMethod]
        public void GetCustomersWithIncompleteInfoTest()
        {
            int customersNumber1 = analyzer.GetCustomersWithIncompleteInfo().Count;
            analyzer.Customers.RemoveAt(0);
            int customersNumber2 = analyzer.GetCustomersWithIncompleteInfo().Count;
            Assert.IsTrue(customersNumber2 == customersNumber1 - 1);

            //List<XElement> customersWithIncompleteInfo = analyzer.GetCustomersWithIncompleteInfo();
            //List<XElement> customerWithCompleteInfo = analyzer.Customers.Except(customersWithIncompleteInfo).ToList();
            //Assert.IsTrue(customerWithCompleteInfo
            //    .All(c =>
            //    c.Element("postalcode").Value.All(char.IsDigit)
            //    && c.Element("region") != null
            //    && (c.Element("phone").Value.Contains('(') && c.Element("phone").Value.Contains(')'))));
        }

        //task 7
        [TestMethod]
        public void GetCitiesAverageProfitabilityTest()
        {
            Dictionary<string, double> citiesWithProfitability = analyzer.GetCitiesAverageProfitability();
            double cityProfitability = citiesWithProfitability["Berlin"];

            var cityCustomers = analyzer.Customers.Where(c => c.Element("city").Value.Equals("Berlin")).ToList();
            double sumOfOrders = cityCustomers.Select(c => c.Element("orders").Elements("order").Sum(order => double.Parse(order.Element("total").Value))).ToList().Sum();
            double expectedCityProfitability = sumOfOrders / (double)cityCustomers.Sum(c => c.Element("orders").Elements("order").Count());

            Assert.AreEqual(expectedCityProfitability, cityProfitability, 0.001);
        }

        [TestMethod]
        public void GetCitiesAverageIntensityTest()
        {
            Dictionary<string, double> citiesWithIntensity = analyzer.GetCitiesAverageIntensity();
            double cityIntensity = citiesWithIntensity["Berlin"];

            var cityCustomers = analyzer.Customers.Where(c => c.Element("city").Value.Equals("Berlin")).ToList();
            int numOfOrders = cityCustomers.Select(c => c.Element("orders").Elements("order").Count()).ToList().Sum();
            double expectedCityIntensity = (double)numOfOrders / (double)cityCustomers.Count;

            Assert.AreEqual(expectedCityIntensity, cityIntensity, 0.001);
        }

        //task 8 
        [TestMethod]
        public void GetCustomersActivityByMonthsTest()
        {
            int monthActivity1 = analyzer.GetCustomersActivityByMonths()[1];
            analyzer.Customers.RemoveAt(0);
            int monthActivity2 = analyzer.GetCustomersActivityByMonths()[1];
            Assert.IsTrue(monthActivity2 == monthActivity1 - 1);

            //Dictionary<int, int> customersActivity = analyzer.GetCustomersActivityByMonths();
            //int overallActivity = customersActivity.Sum(a => a.Value);
            //int expectedOverallActivity = analyzer.Customers.Select(c => c.Element("orders").Elements("order").Count()).ToList().Sum();
            //Assert.AreEqual(expectedOverallActivity, overallActivity);
        }

        [TestMethod]
        public void GetCustomersActivityByYearsTest()
        {
            int yearActivity1 = analyzer.GetCustomersActivityByYears()[1997];
            analyzer.Customers.RemoveAt(0);
            int yearActivity2 = analyzer.GetCustomersActivityByYears()[1997];
            Assert.IsTrue(yearActivity2 == yearActivity1 - 3);

            //Dictionary<int, int> customersActivity = analyzer.GetCustomersActivityByYears();
            //int overallActivity = customersActivity.Sum(a => a.Value);
            //int expectedOverallActivity = analyzer.Customers.Select(c => c.Element("orders").Elements("order").Count()).ToList().Sum();
            //Assert.AreEqual(expectedOverallActivity, overallActivity);
        }

        [TestMethod]
        public void GetCustomersActivityByMonthsOfYearsTest()
        {
            int monthYearActivity1 = analyzer.GetCustomersActivityByMonthsOfYears()["1998-01"];
            analyzer.Customers.RemoveAt(0);
            int monthYearActivity2 = analyzer.GetCustomersActivityByMonthsOfYears()["1998-01"];
            Assert.IsTrue(monthYearActivity2 == monthYearActivity1 - 1);

            //Dictionary<string, int> customersActivity = analyzer.GetCustomersActivityByMonthsOfYears();
            //int overallActivity = customersActivity.Sum(a => a.Value);
            //int expectedOverallActivity = analyzer.Customers.Select(c => c.Element("orders").Elements("order").Count()).ToList().Sum();
            //Assert.AreEqual(expectedOverallActivity, overallActivity);
        }
    }
}
