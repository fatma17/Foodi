using Foodi.Core.Models;
using Foodi.Core.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Foodi.Core.Services
{
    public interface ICartsService
    {
        Task AddAsync(int userId, int productId, int quantity);
        Task<IEnumerable<Cart>> GetCartItems(int userId);
        Task<Cart> GetCartItemAsync(int userId, int itemId);
        void UpdateCartItem(Cart cartItem);
        bool DeleteItems(IEnumerable<Cart> cartIems);
        bool DeleteItem(int Id , int UserId);
    }
}
