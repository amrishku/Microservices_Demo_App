namespace Chingari.Web.Services.IServices
{
    public interface IOrderService : IBaseService
    {
        Task<T> GetAllOrderListAsync<T>(string token);
    }
}
