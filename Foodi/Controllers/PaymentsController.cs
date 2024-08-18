using Foodi.Core.Models;
using Foodi.Core.Services;
using Foodi.Core.ViewModels;
using Foodi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stripe.Checkout;
using System.Security.Claims;
using Foodi.Core.Enums;
using Foodi.Core.Consts;
using Microsoft.AspNetCore.Authorization;


namespace Foodi.Controllers
{
    [Authorize(Roles = AppRoles.User)]
    public class PaymentsController : Controller
    {

        private readonly IPaymentsService _PaymentsService;
        private readonly IOrdersService _OrdersService;
        private readonly ICartsService _cartsService;

        public PaymentsController(IPaymentsService paymentsService , IOrdersService ordersService , ICartsService cartsService)
        {
            _PaymentsService = paymentsService;
            _OrdersService = ordersService;
            _cartsService = cartsService;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessPayment(PaymentMethod paymentMethod)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            if (paymentMethod == PaymentMethod.Cash)
            {
                return RedirectToAction("Success", new { paymentMethod = PaymentMethod.Cash });
            }

            else if(paymentMethod == PaymentMethod.Credit)
            {
                var cartItems = await _cartsService.GetCartItems(int.Parse(userId));
                //var totalPrice = cartItems.Sum(item => item.Quantity * item.Product.Price);

                var domain = "https://localhost:7018/";
                var options = new SessionCreateOptions
                {
                    //SuccessUrl = domain + $"Payments/OrderConfirmation",
                    SuccessUrl = Url.Action("Success", "Payments", new { paymentMethod = PaymentMethod.Credit }, Request.Scheme),
                    CancelUrl = domain + $"Carts/Index",

                    LineItems = new List<SessionLineItemOptions>(),
                    //{
                    //    new SessionLineItemOptions
                    //    {
                    //        PriceData = new SessionLineItemPriceDataOptions
                    //        {
                    //            Currency = "usd",
                    //            ProductData = new SessionLineItemPriceDataProductDataOptions
                    //            {
                    //                Name = "Total Price",
                    //            },
                    //            UnitAmount = totalPrice*100,
                    //        },
                    //        Quantity = 1,

                    //    },
                    //},
                    Mode = "payment",
                    //CustomerEmail=" ",
                };

                foreach (var item in cartItems)
                {
                    var SessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name,
                            },
                            UnitAmount = item.Product.Price * 100,
                        },
                        Quantity = item.Quantity,
                    };
                    options.LineItems.Add(SessionLineItem);

                }

                var service = new SessionService();
                Session session = service.Create(options);

                Response.Headers.Add("Location", session.Url);

                return new StatusCodeResult(303);
            }

            return BadRequest("Invalid payment type.");

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Success(PaymentMethod paymentMethod)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var cartItems = await _cartsService.GetCartItems(int.Parse(userId));

            var totalPrice = cartItems.Sum(item => item.Quantity * item.Product.Price);

            var payment = await _PaymentsService.Create(paymentMethod, totalPrice);

            await _OrdersService.CreateOrderAsync(cartItems, payment.Id);

            _cartsService.DeleteItems(cartItems);

            return View("Success");
        }

    }
}
