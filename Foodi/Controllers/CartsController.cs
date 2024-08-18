using Foodi.Core.Consts;
using Foodi.Core.Models;
using Foodi.Core.Services;
using Foodi.Core.ViewModels;
using Foodi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Foodi.Controllers
{

    [Authorize(Roles = AppRoles.User)]
    public class CartsController : Controller
    {
        private readonly ICartsService _CartsService;
        private readonly ILogger<CartsController> _logger;

        public CartsController(ICartsService cartsService, ILogger<CartsController> logger)
        {
            _CartsService = cartsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                _logger.LogWarning("Unauthorized access attempt.");
                return Unauthorized();
            }

            var cartItems = await _CartsService.GetCartItems(int.Parse(userId));
            
            var totalPrice = cartItems.Sum(item => item.Quantity * item.Product.Price);

            var model = new CartViewModel
            {
                CartItems = cartItems ,
                Amount = totalPrice
            };

            _logger.LogInformation("User {UserId} accessed the cart. Total items: {ItemCount}, Total price: {TotalPrice}", userId, cartItems.Count(), totalPrice);
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int foodItemId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                _logger.LogWarning("Unauthorized add to cart attempt.");
                return Unauthorized();
            }
            await _CartsService.AddAsync(int.Parse(userId), foodItemId, quantity);
            _logger.LogInformation("User {UserId} added item {FoodItemId} with quantity {Quantity} to cart.", userId, foodItemId, quantity);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateQuantityViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            _logger.LogInformation("User {UserId} is updating quantity for item {ItemId} with change {Change}.", userId, model.Id, model.Change);

            var cartItem = await _CartsService.GetCartItemAsync(int.Parse(userId), model.Id);

            if (cartItem == null)
            {
                _logger.LogWarning("User {UserId} attempted to update non-existent cart item {ItemId}.", userId, model.Id);
                return NotFound();
            }

            cartItem.Quantity += model.Change;
            if (cartItem.Quantity < 1)
            {
                cartItem.Quantity = 1;
            }

             _CartsService.UpdateCartItem(cartItem);
            //var total = (cartItem.Quantity * cartItem.Product.Price).ToString("C");
           // _logger.LogInformation("User {UserId} updated cart item {ItemId} to quantity {Quantity}. Total price: {Total}", userId, model.Id, updatedCartItem.Quantity, response.total);

            return Ok(new { quantity = cartItem.Quantity });
        }

        public IActionResult Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var isDeleted = _CartsService.DeleteItem(id,int.Parse(userId));
            _logger.LogInformation("User {UserId} deleted cart item {ItemId}.", userId, id);

            return isDeleted ? Ok() : BadRequest();
        }
    }


   
}
