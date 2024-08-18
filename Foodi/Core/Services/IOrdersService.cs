using Foodi.Core.Enums;
using Foodi.Core.Models;
using Foodi.Core.ViewModels;
using Foodi.Infrastructure;

namespace Foodi.Core.Services
{
    public interface IOrdersService
    {
        OrderDetailsViewModel GetOrderById(int OrderId);
       
        Task<IEnumerable<Order>> GetAllOrder();

        Task<IEnumerable<OrderHistoryViewModel>> GetUserOrdersHistoryAsync(int userId);

        OrderDetailsViewModel GetUserOrderDetailsAsync(int userId, int orderId);

        Task<int> GetOrderCount();

        Task<Order> CreateOrderAsync(IEnumerable<Cart> cartItems, int paymentId);

        (bool success, string message) UpdateOrderStatus(OrderUpdateStatusViewModel model);
    }
}
