using Chingari.Services.OrderAPI.Models;

namespace Chingari.Services.OrderAPI.Repository
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(OrderDetails orderDetails);
    }
}
