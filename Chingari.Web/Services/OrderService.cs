using Chingari.Web.Models;
using Chingari.Web.Services.IServices;

namespace Chingari.Web.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IHttpClientFactory _clientFactory;

        public OrderService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<T> GetAllOrderListAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.OrderAPIBase + "/api/orders",
                AccessToken = token
            });
        }
    }
}
