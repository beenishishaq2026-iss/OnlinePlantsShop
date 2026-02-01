using Microsoft.AspNetCore.Mvc;
using OnlinePlantsShop_.Models;
using OnlinePlantsShop_.Repositories;
using OnlinePlantsShop_.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using OnlinePlantsShop.Data;
using Microsoft.EntityFrameworkCore;

namespace OnlinePlantsShop_.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PlantsController : Controller
    {
    }

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IPlant _repository;
        private readonly IHubContext<PlantsHub> _hubContext;
        private readonly OrdersDbContext _ordersDb; 

        public AdminController(IPlant repository, IHubContext<PlantsHub> hubContext, OrdersDbContext ordersDb)
        {
            _repository = repository;
            _hubContext = hubContext;
            _ordersDb = ordersDb ?? throw new ArgumentNullException(nameof(ordersDb));
        }

        public async Task<IActionResult> ManageOrders()
        {
            var ordersFromDb = await _ordersDb.Orders.ToListAsync(); 

            var orders = ordersFromDb
                .Select(o => new OrderViewModel
                {
                    OrderId = o.Id,
                    PlantNames = o.PlantNames,
                    Address = o.Address,
                    OrderStatus = o.OrderStatus
                })
                .ToList(); 

            return View(orders);
        }


        public IActionResult ManagePlants()
        {
            var plants = _repository.GetAll();
            return View(plants);
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Plant plant)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(plant);
                _repository.Save();

                await _hubContext.Clients.All.SendAsync("ReceivePlantNotification", $"🌱 New plant added: {plant.Name}");

                return RedirectToAction("ManagePlants");
            }
            return View(plant);
        }

        public IActionResult Edit(int id)
        {
            var plant = _repository.GetById(id);
            if (plant == null) return NotFound();
            return View(plant);
        }

        [HttpPost]
        public IActionResult Edit(Plant plant)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(plant);
                _repository.Save();
                return RedirectToAction("ManagePlants");
            }
            return View(plant);
        }

        public IActionResult Delete(int id)
        {
            var plant = _repository.GetById(id);
            if (plant == null) return NotFound();
            return View(plant);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            _repository.Save();
            return RedirectToAction("ManagePlants");
        }
    }
}
