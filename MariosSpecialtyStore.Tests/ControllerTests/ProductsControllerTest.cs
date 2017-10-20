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
    public class ProductsControllerTest : IDisposable
    {
        Mock<IProductRepository> mock = new Mock<IProductRepository>();
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
            ProductsController controller = new ProductsController(mock.Object);

            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void Mock_IndexListOfProducts_Test()
        {
            DbSetup();
            ViewResult indexView = new ProductsController(mock.Object).Index() as ViewResult;

            var result = indexView.ViewData.Model;

            Assert.IsInstanceOfType(result, typeof(List<Product>));
        }

        [TestMethod]
        public void Mock_ConfirmEntryExists_Test()
        {
            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);
            var testProduct = new Product { ProductId = 1, Name = "Pepperoni", Cost = 12.75, Country = "USA" };

            ViewResult indexView = controller.Index() as ViewResult;
            var collection = indexView.ViewData.Model as List<Product>;

            CollectionAssert.Contains(collection, testProduct);
        }

		[TestMethod]
		public void Mock_ConfirmAllProductsExist_Test()
		{
            DbSetup();
			ProductsController controller = new ProductsController(mock.Object);
            var testProductList = new List<Product>
            {
                new Product { ProductId = 1, Name = "Pepperoni", Cost = 12.75, Country = "USA" },
                new Product { ProductId = 2, Name = "Pinot Noir", Cost = 47.50, Country = "France" },
                new Product { ProductId = 3, Name = "Brie", Cost = 17.99, Country = "Italy" }
            };

			ViewResult indexView = controller.Index() as ViewResult;
			var collection = indexView.ViewData.Model as List<Product>;

			Assert.AreEqual(collection.Count(), testProductList.Count());
		}

        [TestMethod]
        public void Mock_ConfirmAverageRating_Test()
        {
            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);

			ViewResult indexView = controller.Index() as ViewResult;
			var collection = indexView.ViewData.Model as List<Product>;
            var firstProductRating = collection[0].AverageRating();

            Assert.AreEqual(3, firstProductRating);
		}

        //TEST DB AREA

        [TestMethod]
        public void DB_CreateNewEntry_Test()
        {
            ProductsController controller = new ProductsController(db);

            Product testProduct = new Product();
            testProduct.Name = "Pepperoni";
            testProduct.Cost = 12.75;
            testProduct.Country = "USA";

            controller.Create(testProduct);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Product>;

            CollectionAssert.Contains(collection, testProduct);
        }

        [TestMethod]
        public void DB_EditEntry_Test()
        {
			ProductsController controller = new ProductsController(db);

			Product testProduct = new Product();
			testProduct.Name = "Pepperoni";
			testProduct.Cost = 12.75;
			testProduct.Country = "USA";

            controller.Create(testProduct);

            testProduct.Name = "Fromage";
            controller.Edit(testProduct);

			var collection = (controller.Index() as ViewResult).ViewData.Model as List<Product>;

            Assert.AreEqual(testProduct.Name, "Fromage");

		}



  //      [TestMethod]
  //      public void DB_DeleteEntry_Test()
  //      {
		//	ProductsController controller = new ProductsController(db);

		//	Product testProduct = new Product();
  //          testProduct.ProductId = 1;
		//	testProduct.Name = "Pepperoni";
		//	testProduct.Cost = 12.75;
		//	testProduct.Country = "USA";

		//	controller.Create(testProduct);

  //          controller.Delete(testProduct.ProductId);

		//	var collection = (controller.Index() as ViewResult).ViewData.Model as List<Product>;

  //          CollectionAssert.DoesNotContain(collection, testProduct);
		//}
    }
}
