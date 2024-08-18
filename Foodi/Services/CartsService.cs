using Foodi.Core;
using Foodi.Core.Models;
using Foodi.Core.Services;
using Foodi.Core.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Foodi.Services
{
    public class CartsService : ICartsService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public CartsService(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task AddAsync(int userId, int productId, int quantity)
        {
            var cartItem = _UnitOfWork.Carts.Find(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem == null)
            {
                Cart cart = new()
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity
                };
                await _UnitOfWork.Carts.AddAsync(cart);
            }

            else
            {
                cartItem.Quantity += quantity;
                _UnitOfWork.Carts.Update(cartItem);
            }

            _UnitOfWork.Save();
        }

        public async Task<IEnumerable<Cart>> GetCartItems(int userId)
        {
            return await _UnitOfWork.Carts.FindAllAsync(c => c.UserId == userId, ["Product"]);
        }

        public async Task<Cart> GetCartItemAsync(int userId, int Id)
        {
             return  _UnitOfWork.Carts.Find(ci => ci.UserId == userId && ci.Id == Id);
        }

        public void UpdateCartItem(Cart cartItem)
        {
            _UnitOfWork.Carts.Update(cartItem);
            _UnitOfWork.Save();
        }

        public bool DeleteItems(IEnumerable<Cart> cartItems)
        {
            _UnitOfWork.Carts.DeleteRange(cartItems);
            _UnitOfWork.Save();
            return true;
        }
        public bool DeleteItem(int Id , int UserId)
        {
            var isDeleted = false;

            var cartitem = _UnitOfWork.Carts.Find(c => c.Id == Id &&c.UserId== UserId);

            if (cartitem == null)
                return isDeleted;

            _UnitOfWork.Carts.Delete(cartitem);


            var effectRows = _UnitOfWork.Save();
            if (effectRows > 0)
            {
                isDeleted = true;
            }
            return isDeleted;
        }

        
    }
}
