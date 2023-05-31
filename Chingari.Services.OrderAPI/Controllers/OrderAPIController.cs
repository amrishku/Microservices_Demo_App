using Chingari.Services.OrderAPI.Repository;
using Chingari.Services.OrderAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Chingari.Services.OrderAPI.Controllers
{
    [Route("api/orders")]
    public class OrderAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private IOrderRepository _orderRepository;

        public OrderAPIController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            this._response = new ResponseDto();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<OrderDetails> orderLists = await _orderRepository.GetOrderList();
                _response.Result = orderLists;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
