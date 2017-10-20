
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MariosSpecialtyStore.Models;
using MariosSpecialtyStore.Models.Repositories;

namespace MariosSpecialtyStore.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository productRepo;

        public ProductsController(IProductRepository thisRepo = null)
        {
            if (thisRepo == null)
            {
                this.productRepo = new EFProductRepository();
            }
            else
            {
                this.productRepo = thisRepo;
            }
        }

        public IActionResult Index()
        {
            return View(productRepo.Products.Include(products => products.Reviews).ToList());
        }

        public IActionResult Details(int id)
        {
            var thisProduct = productRepo.Products.FirstOrDefault(products => products.ProductId == id);
            return View(thisProduct);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            productRepo.Save(product);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisProduct = productRepo.Products.FirstOrDefault(product => product.ProductId == id);
            return View(thisProduct);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            productRepo.Edit(product);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisProduct = productRepo.Products.FirstOrDefault(product => product.ProductId == id);
            return View(thisProduct);
        }

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
            var thisProduct = productRepo.Products.FirstOrDefault(product => product.ProductId == id);
			productRepo.Remove(thisProduct);
			return RedirectToAction("Index");
		}
    }
}
