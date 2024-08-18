using Foodi.Core.Enums;
using Foodi.Core.Models;

namespace Foodi.Core.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<Cart> CartItems { get; set; }
        public int Amount { get; set; }
    }
}
