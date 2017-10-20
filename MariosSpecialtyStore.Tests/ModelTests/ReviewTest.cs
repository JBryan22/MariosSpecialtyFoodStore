using System;
using MariosSpecialtyStore.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace MariosSpecialtyStore.Tests
{
	[TestClass]
	public class ReviewTest
	{
		[TestMethod]
		public void GetPropertiesTest()
		{
			var product = new Review("Jesse", "This pepperoni was amazing. Nice and spicy. Gave me really bad acid reflux but it was worth it.", 5);

			var authorResult = product.Author;
			var contentResult = product.Content_Body;
			var ratingResult = product.Rating;

			Assert.AreEqual("Jesse", authorResult);
			Assert.AreEqual("This pepperoni was amazing. Nice and spicy. Gave me really bad acid reflux but it was worth it.", contentResult);
			Assert.AreEqual(5, ratingResult);

		}

		[TestMethod]
		public void EqualityTest()
		{
			var product1 = new Review("Jesse", "This pepperoni was amazing. Nice and spicy. Gave me really bad acid reflux but it was worth it.", 5);
			var product2 = new Review("Jesse", "This pepperoni was amazing. Nice and spicy. Gave me really bad acid reflux but it was worth it.", 5);

			Assert.AreEqual(product1, product2);
		}
	}
}