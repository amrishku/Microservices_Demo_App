using Chingari.MessageBus;
using Chingari.Web.Models;
using Chingari.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Chingari.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IMessageBus _messageBus;
        public HomeController(ILogger<HomeController> logger, IProductService productService, IMessageBus messageBus)
        {
            _logger = logger;
            _productService = productService;
            _messageBus = messageBus;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> list = new();
            var response = await _productService.GetAllProductsAsync<ResponseDto>("");
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> Details(int productId)
        {
            ProductDto model = new();
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, "");
            if (response != null && response.IsSuccess)
            {
                model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("Details")]
        //[Authorize]
        public async Task<IActionResult> DetailsPost(ProductDto productDto)
        {
            //send message in azure service bus
            await _messageBus.PublishMessage(productDto, "checkoutmessagetopic");
            return View(productDto);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}