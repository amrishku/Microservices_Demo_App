using Chingari.Services.OrderAPI.Models;

namespace Chingari.Services.OrderAPI.Repository
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(OrderDetails orderDetails);
        Task<IEnumerable<OrderDetails>> GetOrderList();

        Task UpdateOrderPaymentStatus(int orderHeaderId, bool paid);
    }
}
