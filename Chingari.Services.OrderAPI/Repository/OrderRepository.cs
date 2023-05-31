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

        public async Task<IEnumerable<OrderDetails>> GetOrderList()
        {
            await using var _db = new ApplicationDbContext(_dbContext);
            List<OrderDetails> orderList = await _db.OrderDetails.ToListAsync();
            return orderList;
        }

        public async Task UpdateOrderPaymentStatus(int orderHeaderId, bool paid)
        {
            await using var _db = new ApplicationDbContext(_dbContext);
            var orderHeaderFromDb = await _db.OrderDetails.FirstOrDefaultAsync(u => u.Id == orderHeaderId);
            if (orderHeaderFromDb != null)
            {
                orderHeaderFromDb.PaymentStatus = paid;
                await _db.SaveChangesAsync();
            }
        }
    }
}
