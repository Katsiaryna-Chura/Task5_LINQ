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
        static CustomersInfoAnalyzer analyzer;

        [ClassInitialize]
        public static void ClassInitialize(TestContext tc)
        {
            analyzer = new CustomersInfoAnalyzer(CustomersFileInfo.Path);
        }

        //task 1
        [TestMethod]
        public void GetCustomersWithSumOfOrdersBiggerThanXTest()
        {
            Assert.IsTrue(analyzer.GetCustomersWithSumOfOrdersBiggerThanX(104874).Count > analyzer.GetCustomersWithSumOfOrdersBiggerThanX(104875).Count);
        }

        //task 2
        [TestMethod]
        public void GetCustomersGroupedByCountryTest()
        {
            List<XElement> countryCustomers = analyzer.GetCustomersGroupedByCountry()["USA"];
            List<XElement> otherCustomers = analyzer.Customers.Except(countryCustomers).ToList();
            Assert.IsTrue(otherCustomers.All(c => c.Element("country").Value != "USA"));
        }

        //task 3
        [TestMethod]
        public void FindCustomersWithAnyOrderBiggerThanXTest()
        {
            Assert.IsTrue(analyzer.FindCustomersWithAnyOrderBiggerThanX(15809).Count > analyzer.FindCustomersWithAnyOrderBiggerThanX(15810).Count);
        }

        //task 4 ???
        [TestMethod]
        public void GetCustomersWithStartDatesTest()
        {
            Dictionary<XElement, string> customersWithDates = analyzer.GetCustomersWithStartDates();
            List<string> dates = customersWithDates.Select(d => d.Value).ToList();
            Assert.IsTrue(dates.Where(d => Convert.ToDateTime(d) == DateTime.MinValue).Count() == 2);
        }

        //task 5
        [TestMethod]
        public void GetSortedListOfCustomersTest()
        {
            List<XElement> sortedCustomers = analyzer.GetSortedListOfCustomers();
            CollectionAssert.AreNotEqual(analyzer.Customers, sortedCustomers);
            //double sumOfOrders = sortedCustomers.First().Element("orders").Elements("order").Sum(order => double.Parse(order.Element("total").Value));
            //Assert.AreEqual(0, sumOfOrders);
        }

        //task 6
        [TestMethod]
        public void GetCustomersWithIncompleteInfoTest()
        {
            //remove first customer => number of customers with incomlete info will decrease by 1 ?????

            List<XElement> customersWithIncompleteInfo = analyzer.GetCustomersWithIncompleteInfo();
            //Assert.IsTrue(analyzer.Customers.Count > customersWithIncompleteInfo.Count);
            List<XElement> customerWithCompleteInfo = analyzer.Customers.Except(customersWithIncompleteInfo).ToList();
            Assert.IsTrue(customerWithCompleteInfo
                .All(c =>
                c.Element("postalcode").Value.All(char.IsDigit)
                && c.Element("region") != null
                && (c.Element("phone").Value.Contains('(') && c.Element("phone").Value.Contains('('))));
        }

        //task 7
        [TestMethod]
        public void GetCitiesAverageProfitabilityTest()
        {
            Dictionary<string, double> citiesWithProfitability = analyzer.GetCitiesAverageProfitability();
            double cityProfitability = citiesWithProfitability["Berlin"];
            var cityCustomers = analyzer.Customers.Where(c => c.Element("city").Value.Equals("Berlin")).ToList();
            double sumOfOrders = cityCustomers.Select(c => c.Element("orders").Elements("order").Sum(order => double.Parse(order.Element("total").Value))).ToList().Sum();
            double expectedCityProfitability = sumOfOrders / (double)cityCustomers.Count;
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

        //task 8 ???
        [TestMethod]
        public void GetCustomersActivityByMonthsTest()
        {
            //remove first customer => activity for 01 month must be = old - 1 ????

            Dictionary<int, int> customersActivity = analyzer.GetCustomersActivityByMonths();
            int overallActivity = customersActivity.Sum(a => a.Value);
            int expectedOverallActivity = analyzer.Customers.Select(c => c.Element("orders").Elements("order").Count()).ToList().Sum();
            Assert.AreEqual(expectedOverallActivity, overallActivity);
        }

        [TestMethod]
        public void GetCustomersActivityByYearsTest()
        {
            //remove first customer => activity for 1997  must be = old - 3 ????????

            Dictionary<int, int> customersActivity = analyzer.GetCustomersActivityByYears();
            int overallActivity = customersActivity.Sum(a => a.Value);
            int expectedOverallActivity = analyzer.Customers.Select(c => c.Element("orders").Elements("order").Count()).ToList().Sum();
            Assert.AreEqual(expectedOverallActivity, overallActivity);
        }

        [TestMethod]
        public void GetCustomersActivityByMonthsOfYearsTest()
        {
            //remove first customer => activity for 1998-01 must be = old - 1 ?????

            Dictionary<string, int> customersActivity = analyzer.GetCustomersActivityByMonthsOfYears();
            int overallActivity = customersActivity.Sum(a => a.Value);
            int expectedOverallActivity = analyzer.Customers.Select(c => c.Element("orders").Elements("order").Count()).ToList().Sum();
            Assert.AreEqual(expectedOverallActivity, overallActivity);
        }
    }
}
