using Microsoft.AspNetCore.Mvc;
using OnlinePlantsShop_.Models;
using OnlinePlantsShop_.Data;  // Your DbContext namespace
using Microsoft.EntityFrameworkCore;
using OnlinePlantsShop.Data;
using OnlinePlantsShop.Models;

namespace OnlinePlantsShop_.Controllers
{
    public class OrdersController : Controller
    {
        private readonly PlantsDbContext _plantsDb;
        private readonly OrdersDbContext _ordersDb;  

        public OrdersController(PlantsDbContext plantsDb, OrdersDbContext ordersDb)
        {
            _plantsDb = plantsDb;
            _ordersDb = ordersDb;
        }

        public async Task<IActionResult> PlaceOrder(int plantId)
        {
            var plant = await _plantsDb.Plants.FirstOrDefaultAsync(p => p.Id == plantId);
            if (plant == null)
            {
                return NotFound();
            }

            var model = new PlaceOrderViewModel
            {
                PlantId = plant.Id,
                PlantName = plant.Name,
                PlantPrice = plant.Price,
                Quantity = 1 
            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> PlaceOrder(PlaceOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var order = new Order
            {
                OrderDate = DateTime.Now,
                PlantNames = model.PlantName,
                Quantities = model.Quantity.ToString(),
                Prices = model.PlantPrice.ToString(),
                TotalAmount = model.PlantPrice * model.Quantity,
                Address = model.Address,
                OrderStatus = "Pending"
            };

            _ordersDb.Orders.Add(order);
            await _ordersDb.SaveChangesAsync();

            ViewBag.OrderId = order.Id;

            return View("OrderConfirmation");
        }

        public IActionResult OrderConfirmation()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _ordersDb.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var model = new PlaceOrderViewModel
            {
                OrderId = order.Id,
                PlantName = order.PlantNames,
                PlantPrice = decimal.Parse(order.Prices),
                Quantity = int.Parse(order.Quantities),
                Address = order.Address
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PlaceOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var order = await _ordersDb.Orders.FindAsync(model.OrderId);
            if (order == null)
            {
                return NotFound();
            }

            order.PlantNames = model.PlantName;
            order.Quantities = model.Quantity.ToString();
            order.Prices = model.PlantPrice.ToString();
            order.TotalAmount = model.PlantPrice * model.Quantity;
            order.Address = model.Address;

            _ordersDb.Orders.Update(order);
            await _ordersDb.SaveChangesAsync();

            ViewBag.OrderId = order.Id;

            return View("OrderConfirmation");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await _ordersDb.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _ordersDb.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _ordersDb.Orders.Remove(order);
            await _ordersDb.SaveChangesAsync();

            ViewBag.OrderId = id;

            return View("OrderConfirmation");
        }


    }
}
