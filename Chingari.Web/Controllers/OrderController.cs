using Chingari.Web.Models;
using Chingari.Web.Services;
using Chingari.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace Chingari.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OrderIndex()
        {
            List<ProductDetails> list = new();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _orderService.GetAllOrderListAsync<ResponseDto>(accessToken);
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDetails>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}
