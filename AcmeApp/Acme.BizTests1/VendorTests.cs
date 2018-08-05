using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Common;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class VendorTests
    {
        [TestMethod()]
        public void PlaceOrderTest()
        {

            //Arrange
            var vendor = new Vendor();
            var product = new Product(1, "Saw", "");
            var expected = new OperationResult(true, "Order from Acme, Inc\r\nProduct: Tools-1\r\nQuantity: 12");


            //Act 
            var actual = vendor.PlaceOrder(product, 12);

            // Assert
            Assert.AreEqual(expected.Success, actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void PlaceOrder_3Parameters()
        {

            //Arrange
            var vendor = new Vendor();
            var product = new Product(1, "Saw", "");
            var expected = new OperationResult(true, "Order from Acme, Inc\r\nProduct: Tools-1\r\nQuantity: 12" + "\r\nDeliver By: 10.10.2018 00:00:00 -07:00");

            //Act 
            var actual = vendor.PlaceOrder(product, 12, new DateTimeOffset(2018, 10, 10, 0, 0, 0, new TimeSpan(-7, 0, 0)));

            // Assert
            Assert.AreEqual(expected.Success, actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlaceOrder_NullProduct_Exception()
        {

            //Arrange
            var vendor = new Vendor();


            //Act 
            var actual = vendor.PlaceOrder(null, 12);

            // Assert
            // Expected Exception
        }

        [TestMethod()]
        public void ToStringTest()
        {
            var vendor = new Vendor();
            vendor.VendorId = 1;
            vendor.CompanyName = "ABC Corp";
            var expected = "Vendor: ABC Corp";


            var actual = vendor.ToString();


            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PrepareDirectionsTest()
        {

            var vendor = new Vendor();
            
            var expected = @"Insert \r\n to define a new line";


            var actual = vendor.PrepareDirections();
            Console.WriteLine(actual);


            Assert.AreEqual(expected, actual);
        }
    }
}