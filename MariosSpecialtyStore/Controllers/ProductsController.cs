
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MariosSpecialtyStore.Models;
using MariosSpecialtyStore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace MariosSpecialtyStore.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(productRepo.Products.Include(products => products.Reviews).ToList());
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var thisProduct = productRepo.Products.Include(p => p.Reviews).FirstOrDefault(products => products.ProductId == id);
            return View(thisProduct);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("ProductId,Name,Cost,Country,ProductImg")] Product product, ICollection<IFormFile> files=null)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            byte[] fileBytes = ms.ToArray();
                            product.ProductImg = fileBytes;
                        }
                    }
                }
            }
            if(ModelState.IsValid)
            {
                productRepo.Save(product);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public IActionResult Edit(int id)
        {
            var thisProduct = productRepo.Products.FirstOrDefault(product => product.ProductId == id);
            return View(thisProduct);
        }

        [HttpPost]
        public IActionResult Edit(Product product, ICollection<IFormFile> files=null)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            byte[] fileBytes = ms.ToArray();
                            product.ProductImg = fileBytes;
                        }
                    }
                }
            }
            if(ModelState.IsValid)
            {
                productRepo.Edit(product);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);

            }
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
