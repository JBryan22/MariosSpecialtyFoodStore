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
    public class HomeController : Controller
    {
        private IProductRepository productRepo;

        public HomeController(IProductRepository thisRepo = null)
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
            List<Product> allProducts = productRepo.Products.Include(products => products.Reviews).ToList();
            List<Product> mostRatedProducts = new List<Product> { };
            List<Product> newestProducts = new List<Product> { };

            for (int i = allProducts.Count - 1; i >= 0 && i > allProducts.Count - 4; i--)
            {
                newestProducts.Add(allProducts[i]);
            }

            for (int i = 0; i < 3; i++)
            {
                if (allProducts.Count < 1)
                {
                    break;
                }
                int ratingsCount = -1;
                Product mostRated = allProducts[0];
                foreach (var product in allProducts)
                {
                    if (product.Reviews.Count > ratingsCount)
                    {
                        mostRated = product;
                        ratingsCount = product.Reviews.Count;
                    }
                }
                mostRatedProducts.Add(mostRated);
                allProducts.Remove(mostRated);
            }

            ViewBag.newest = newestProducts;
            return View(mostRatedProducts);

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
