using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class ProductTests
    {
        [TestMethod()]
        public void SayHelloTest()
        {
            // Arrange
            var currentProduct = new Product(); // object initialization type 1:setting properties tecq
            currentProduct.ProductName = "Saw";
            currentProduct.ProductId = 1;
            currentProduct.Description = "15-inch steel blade hand saw";
            currentProduct.ProductVendor.CompanyName = "ABC Corp";
            var expected = "Hello Saw (1): 15-inch steel blade hand saw" + "Available on: ";
            // Act
            var actual = currentProduct.SayHello();
            // Assert
            Assert.AreEqual(expected, actual);
            // Assert.Fail();
        }

        [TestMethod()]
        public void SayHelloPrameterizedConstructorTest()
        {
            // Arrange
            var currentProduct = new Product(1, "Saw", "15-inch steel blade hand saw");// object initialization type 2: Paameterized Constructor
            var expected = "Hello Saw (1): 15-inch steel blade hand saw" + "Available on: ";
            // Act
            var actual = currentProduct.SayHello();
            // Assert
            Assert.AreEqual(expected, actual);
            // Assert.Fail();
        }

        public void SayHello_ObjectInitializer()
        {
            // Arrange
            var currentProduct = new Product  // object initialization type 3: Using Object Initializer
            {
                ProductId = 1,
                ProductName = "Saw",
                Description = "15-inch steel blade hand saw"
            };

            var expected = "Hello Saw (1): 15-inch steel blade hand saw" + "Available on: ";
            // Act
            var actual = currentProduct.SayHello();
            // Assert
            Assert.AreEqual(expected, actual);
            // Assert.Fail();
        }
        [TestMethod()]
        public void Product_Null()
        {
            // Arrange
            Product currentProduct = null;
            var companyName = currentProduct?.ProductVendor?.CompanyName;// c# 6 da olan ozellik null checking icin "?." null conditional operator kullan uzun if lerden kurtul
            string expected = null;
            // Act
            var actual = companyName;
            // Assert
            Assert.AreEqual(expected, actual);
            // Assert.Fail();
        }
        [TestMethod()]
        public void ConvertMetersToInchesTest()
        {
            // Arrange
            var expected = 78.74;
            // Act
            var actual = 2 * Product.InchesPerMeter; // constant cagırırken direkt classtan cagırdıgımıza dıkkat et
            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void MinimumPriceTest_Default()
        {
            // Arrange
            var currentProduct = new Product(); // we access the value using object instance
            var expected = .96m;
            // Act
            var actual = currentProduct.MinimumPrice;
            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void MinimumPriceTest_Bulk()
        {
            // Arrange
            var currentProduct = new Product(1, "Bulk Tools", ""); // we access the value using object instance
            var expected = 9.99m;
            // Act
            var actual = currentProduct.MinimumPrice;
            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void ProductName_Format()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.ProductName = "  Steel Hammer   ";

            var expected = "Steel Hammer";
            // Act
            var actual = currentProduct.ProductName;
            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void ProductName_TooShort()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.ProductName = "aw";

            string expected = null;
            string expectedMessage = "Product Name must be at least three characters";
            // Act
            var actual = currentProduct.ProductName;
            var actualMessage = currentProduct.ValidationMessage;
            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessage, actualMessage);
        }
        [TestMethod()]
        public void ProductName_TooLong()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.ProductName = "Steel Blended Hand Saw";

            string expected = null;
            string expectedMessage = "Product Name cannot be more than 20 characters";
            // Act
            var actual = currentProduct.ProductName;
            var actualMessage = currentProduct.ValidationMessage;
            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessage, actualMessage);
        }
        [TestMethod()]
        public void ProductName_JustRight()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.ProductName = "Saw";

            string expected = "Saw";
            string expectedMessage = null;
            // Act
            var actual = currentProduct.ProductName;
            var actualMessage = currentProduct.ValidationMessage;
            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessage, actualMessage);
        }
        [TestMethod()]
        public void ProductCode_DefaultValue()
        {
            // Arrange
            var currentProduct = new Product();
            var expected = "Tools-1";


            // Act
            var actual = currentProduct.ProductCode;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CalculateSuggestedPriceTest()
        {
            // Arrange
            var currentProduct = new Product(1, "Saw", "");
            currentProduct.Cost = 50m;
            var expected = 55m;


            // Act
            var actual = currentProduct.CalculateSuggestedPrice(10m);
            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}