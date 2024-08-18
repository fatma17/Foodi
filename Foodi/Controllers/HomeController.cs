using Foodi.Core.Models;
using Foodi.Core.Services;
using Foodi.Core.ViewModels;
using Foodi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Foodi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderItemsService _orderItemsService;

        public HomeController(ILogger<HomeController> logger , IOrderItemsService orderItemsService)
        {
            _logger = logger;
            _orderItemsService = orderItemsService;
        }

        public IActionResult Index()
        {
            var products = _orderItemsService.GetBestProducts(6);
            var contactUs = new Contact();

            var Model = new HomeViewModel
            {
                BestProducts = products,
                ContactUs = contactUs
            };

            return View(Model);
        }

        [HttpGet]
        public IActionResult BestProducts()
        {
            return PartialView("_BestProducts", _orderItemsService.GetBestProducts(6));
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
    }
}
