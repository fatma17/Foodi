using Foodi.Core.Consts;
using Foodi.Core.Enums;
using Foodi.Core.Models;
using Foodi.Core.Services;
using Foodi.Core.ViewModels;
using Foodi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Climate;
using System.Security.Claims;

namespace Foodi.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> Index()
        {
            return View(await _ordersService.GetAllOrder());
        }


        [Authorize(Roles = AppRoles.User)]
        public async Task<IActionResult> History()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var orders = await _ordersService.GetUserOrdersHistoryAsync(int.Parse(userId));
            return View(orders);
        }


        [Authorize(Roles = AppRoles.Admin)]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var orderDetails = _ordersService.GetOrderById(id);

            if (orderDetails is null)
                return NotFound();

            return View(orderDetails);
        }


        [Authorize(Roles = AppRoles.User)]
        [HttpGet]
        public IActionResult View(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var result = _ordersService.GetUserOrderDetailsAsync(int.Parse(userId), id);

            if (result == null)
            {
                return BadRequest(new { Message = "there is no order with that id " });
            }

            return View("Details", result);
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpPost]
        public IActionResult UpdateStatus(OrderUpdateStatusViewModel model)
        {
            if (model == null)
            {
                return Json(new { success=false, message="Invalid request data"});
            }

            var result =  _ordersService.UpdateOrderStatus(model);
            return Json(new {result.success,result.message});
        }

    }
}
