using Chingari.Services.OrderAPI.DbContexts;
using Chingari.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Chingari.Services.OrderAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContext;

        public OrderRepository(DbContextOptions<ApplicationDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddOrder(OrderDetails orderDetails)
        {
            await using var _db = new ApplicationDbContext(_dbContext);
            _db.OrderDetails.Add(orderDetails);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
