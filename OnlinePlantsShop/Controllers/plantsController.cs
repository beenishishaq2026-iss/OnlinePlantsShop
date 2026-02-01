using Microsoft.AspNetCore.Mvc;
using OnlinePlantsShop_.Data;  
using OnlinePlantsShop_.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using OnlinePlantsShop.Data;

namespace OnlinePlantsShop_.Controllers
{
    public class plantsController : Controller
    {
        private readonly PlantsDbContext _context;
        private readonly OrdersDbContext _ordersDb;


        public plantsController(PlantsDbContext plantsDb, OrdersDbContext ordersDb)
        {
            _context = plantsDb;        
            _ordersDb = ordersDb;
        }


        public IActionResult Index()
        {
            var plants = _context.Plants.ToList();
            return View(plants);
        }

        public IActionResult Details(int id)
        {
            var plant = _context.Plants.FirstOrDefault(p => p.Id == id);
            if (plant == null) return NotFound();

            
            var reviews = _ordersDb.Reviews.Where(r => r.PlantId == id).ToList();
            plant.Reviews = reviews;

            return View(plant);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Plant plant)
        {
            if (ModelState.IsValid)
            {
                _context.Plants.Add(plant);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult ReviewSubmitted()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitReview(int id, string userName, int rating, string comment)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(comment) || rating < 1 || rating > 5)
            {
                return RedirectToAction("Details", new { id });
            }

            var review = new review
            {
                PlantId = id,
                UserName = userName,
                Rating = rating,
                Comment = comment
            };

            _ordersDb.Reviews.Add(review);
            await _ordersDb.SaveChangesAsync();

            return RedirectToAction("Details", new { id });
        }

    }
}