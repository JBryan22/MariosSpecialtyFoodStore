using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MariosSpecialtyStore.Models;
using MariosSpecialtyStore.Models.Repositories;
using MariosSpecialtyStore.Controllers;
using Moq;
using System.Linq;
using System;

namespace MariosSpecialtyStore.Tests.ControllerTests
{
	[TestClass]
	public class ReviewsControllerTest : IDisposable
	{
		Mock<IReviewRepository> mock = new Mock<IReviewRepository>();
		EFProductRepository db = new EFProductRepository(new TestDbContext());
		EFReviewRepository reviewDb = new EFReviewRepository(new TestDbContext());

		public void Dispose()
		{
			db.RemoveAll();
			reviewDb.RemoveAll();
		}

		private void DbSetup()
		{
			mock.Setup(mock => mock.Products).Returns(new Product[]
			{
				new Product {ProductId = 1, Name = "Pepperoni", Cost=12.75, Country="USA"},
				new Product {ProductId = 2, Name = "Pinot Noir", Cost=47.50, Country="France"},
				new Product {ProductId = 3, Name = "Brie", Cost=17.99, Country="Italy"}

			}.AsQueryable());

			mock.Setup(mock => mock.Reviews).Returns(new Review[]
			{
				new Review {ReviewId = 1, Author="Jesse", Content_Body="Pretty Good", Rating= 3, ProductId = 1},
				new Review {ReviewId = 2, Author="Bob", Content_Body="Amazing. Loved it", Rating= 5, ProductId = 1},
				new Review {ReviewId = 3, Author="Michael", Content_Body="Too spicy. Terrible", Rating= 1, ProductId = 1},

			}.AsQueryable());
		}

		//MOCK DB AREA

		[TestMethod]
		public void Mock_GetViewResultIndex_Test()
		{
			DbSetup();
			ReviewsController controller = new ReviewsController(mock.Object);

			var result = controller.Index();

			Assert.IsInstanceOfType(result, typeof(ActionResult));
		}

		[TestMethod]
		public void Mock_IndexListOfReviews_Test()
		{
			DbSetup();
			ViewResult indexView = new ReviewsController(mock.Object).Index() as ViewResult;

			var result = indexView.ViewData.Model;

			Assert.IsInstanceOfType(result, typeof(List<Review>));
		}

		[TestMethod]
		public void Mock_ConfirmEntryExists_Test()
		{
			DbSetup();
			ReviewsController controller = new ReviewsController(mock.Object);
			var testReview = new Review { ReviewId = 1, Author="Jesse", Content_Body = "So good.", Rating = 5, ProductId=1 };

			ViewResult indexView = controller.Index() as ViewResult;
			var collection = indexView.ViewData.Model as List<Review>;

			CollectionAssert.Contains(collection, testReview);
		}

		[TestMethod]
		public void Mock_ConfirmAllReviewsExist_Test()
		{
			DbSetup();
			ReviewsController controller = new ReviewsController(mock.Object);
			var testReviewList = new List<Review>
			{
				new Review {ReviewId = 1, Author="Jesse", Content_Body="Pretty Good", Rating= 3, ProductId = 1},
				new Review {ReviewId = 2, Author="Bob", Content_Body="Amazing. Loved it", Rating= 5, ProductId = 1},
				new Review {ReviewId = 3, Author="Michael", Content_Body="Too spicy. Terrible", Rating= 1, ProductId = 1}
			};

			ViewResult indexView = controller.Index() as ViewResult;
			var collection = indexView.ViewData.Model as List<Review>;

			Assert.AreEqual(collection.Count(), testReviewList.Count());
		}

		//TEST DB AREA

		[TestMethod]
		public void DB_CreateNewEntry_Test()
		{
			ReviewsController controller = new ReviewsController(reviewDb);
			ProductsController controller2 = new ProductsController(db);

			Product testProduct = new Product();
			testProduct.ProductId = 1;
			testProduct.Name = "Pepperoni";
			testProduct.Cost = 12.75;
			testProduct.Country = "USA";

			controller2.Create(testProduct);

			Review testReview = new Review();
			testReview.Author = "Jesse";
			testReview.Content_Body = "Wow";
			testReview.Rating = 4;
			testReview.ProductId = 1;

			controller.Create(testReview);
			var collection = (controller.Index() as ViewResult).ViewData.Model as List<Review>;

			CollectionAssert.Contains(collection, testReview);
		}

		[TestMethod]
		public void DB_EditEntry_Test()
		{
			ReviewsController controller = new ReviewsController(reviewDb);
			ProductsController controller2 = new ProductsController(db);

            Product testProduct = new Product();
            testProduct.ProductId = 1;
			testProduct.Name = "Pepperoni";
			testProduct.Cost = 12.75;
			testProduct.Country = "USA";

            controller2.Create(testProduct);

			Review testReview = new Review();
			testReview.Author = "Jesse";
			testReview.Content_Body = "Wow";
			testReview.Rating = 4;
			testReview.ProductId = 1;

			controller.Create(testReview);

			testReview.Author = "Kaili";
			controller.Edit(testReview);

			var collection = (controller.Index() as ViewResult).ViewData.Model as List<Review>;

			Assert.AreEqual(testReview.Author, "Kaili");

		}



		//      [TestMethod]
		//      public void DB_DeleteEntry_Test()
		//      {
		//  ReviewsController controller = new ReviewsController(reviewDb);

		//  Review testReview = new Review();
		//          testReview.ReviewId = 1;
		//  testReview.Name = "Pepperoni";
		//  testReview.Cost = 12.75;
		//  testReview.Country = "USA";

		//  controller.Create(testReview);

		//          controller.Delete(testReview.ReviewId);

		//  var collection = (controller.Index() as ViewResult).ViewData.Model as List<Review>;

		//          CollectionAssert.DoesNotContain(collection, testReview);
		//}
	}
}
