using Microsoft.EntityFrameworkCore;
using OnlinePlantsShop.Data;
using OnlinePlantsShop_.Models;

namespace OnlinePlantsShop.Repository
{
    public interface IOrderRepository
    {
        Task<List<OrderViewModel>> GetAllOrdersAsync();
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersDbContext _context;

        public OrderRepository(OrdersDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderViewModel>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Select(o => new OrderViewModel
                {
                    OrderId = o.Id,
                    PlantNames = o.PlantNames,
                    Address = o.Address,
                    OrderStatus = o.OrderStatus
                })
                .ToListAsync();
        }
    }
}