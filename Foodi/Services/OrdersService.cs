using Foodi.Core;
using Foodi.Core.Enums;
using Foodi.Core.Models;
using Foodi.Core.Services;
using Foodi.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foodi.Services
{
    public class OrdersService : IOrdersService
    {

        private readonly IUnitOfWork _UnitOfWork;

        public OrdersService(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public OrderDetailsViewModel GetOrderById(int OrderId)
        {
            return _UnitOfWork.Orders.GetById<OrderDetailsViewModel>(o => o.Id == OrderId, OrderDetailsViewModel.OrderDetailsSelector);
        }

        public async Task<IEnumerable<Order>> GetAllOrder() 
        {
            return await _UnitOfWork.Orders.GetAll(["Payment"]);
        }

        public async Task<IEnumerable<OrderHistoryViewModel>> GetUserOrdersHistoryAsync(int userId)
        {
            return await _UnitOfWork.Orders.GetAll<OrderHistoryViewModel>(o => o.UserId == userId, OrderHistoryViewModel.OrderHistorySelector);
        }

        public OrderDetailsViewModel GetUserOrderDetailsAsync(int userId , int orderId)
        {
            return  _UnitOfWork.Orders.GetById<OrderDetailsViewModel>(o => o.Id == orderId && o.UserId == userId , OrderDetailsViewModel.OrderDetailsSelector);
        }


        public async Task<int> GetOrderCount()
        {
            return await _UnitOfWork.Orders.CountAsync();
        }

        public async Task<Order> CreateOrderAsync(IEnumerable<Cart> cartItems, int paymentId) 
        {
            var order = new Order
            {
                UserId = cartItems.Select(c=>c.UserId).FirstOrDefault(),
                OrderNo= Guid.NewGuid().ToString(),  //DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                OrderDate = DateTime.UtcNow,
                PaymentId= paymentId,
                Status = OrderStatus.Pending,
                OrderItems = cartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                }).ToList()
            };

            await _UnitOfWork.Orders.AddAsync(order);
             _UnitOfWork.Save();

            return order;
        }

        public (bool success, string message) UpdateOrderStatus(OrderUpdateStatusViewModel model)
        {

            var order =  _UnitOfWork.Orders.Find(o => o.Id == model.OrderId);

            if (order == null)
            {
                return (success:false, message: "Order not found"); 
            }

            if (order.Status == model.NewStatus)
            {
                return (success:false ,message:"Cannot change to the same status");
            }

            order.Status = model.NewStatus;
            _UnitOfWork.Orders.Update(order);
            _UnitOfWork.Save();

            return (success:true, message:"Order status updated successfully");
        }
    }
}
