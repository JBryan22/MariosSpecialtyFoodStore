using System;
using MariosSpecialtyStore.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace MariosSpecialtyStore.Tests
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void GetPropertiesTest()
        {
            var product = new Product("Spicy Pepperoni", 12.75, "USA");

            var nameResult = product.Name;
            var costResult = product.Cost;
            var countryResult = product.Country;

			Assert.AreEqual("Spicy Pepperoni", nameResult);
			Assert.AreEqual(12.75, costResult);
			Assert.AreEqual("USA", countryResult);

        }

        [TestMethod]
        public void EqualityTest()
        {
			var product1 = new Product("Spicy Pepperoni", 12.75, "USA");
			var product2 = new Product("Spicy Pepperoni", 12.75, "USA");

            Assert.AreEqual(product1, product2);
        }
    }
}
