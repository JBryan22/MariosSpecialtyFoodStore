
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
	public class ReviewsController : Controller
	{
		private IReviewRepository reviewRepo;

		public ReviewsController(IReviewRepository thisRepo = null)
		{
			if (thisRepo == null)
			{
				this.reviewRepo = new EFReviewRepository();
			}
			else
			{
				this.reviewRepo = thisRepo;
			}
		}

		public IActionResult Index()
		{
			return View(reviewRepo.Reviews.ToList());
		}

		public IActionResult Details(int id)
		{
			var thisReview = reviewRepo.Reviews.FirstOrDefault(reviews => reviews.ReviewId == id);
			return View(thisReview);
		}

		public IActionResult Create(int id)
		{
            ViewBag.ProductId = id;
			return View();
		}

		[HttpPost]
		public IActionResult Create(Review review)
		{
            if (ModelState.IsValid)
            {
                reviewRepo.Save(review);
                return RedirectToAction("Index");
            }
            else
            {
                return View(review);
            }
		}

		public IActionResult Edit(int id)
        {
            var thisReview = reviewRepo.Reviews.FirstOrDefault(review => review.ReviewId == id);
            return View(thisReview);
		}

		[HttpPost]
		public IActionResult Edit(Review review)
		{
            if (ModelState.IsValid)
            {
                reviewRepo.Edit(review);
                return RedirectToAction("Index", "Products");
            }
            else
            {
                return View(review);
            }
		}

		public IActionResult Delete(int id)
		{
			var thisReview = reviewRepo.Reviews.FirstOrDefault(review => review.ReviewId == id);
			return View(thisReview);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			var thisReview = reviewRepo.Reviews.FirstOrDefault(review => review.ReviewId == id);
			reviewRepo.Remove(thisReview);
            return RedirectToAction("Index", "Products");
			
		}
	}
}
