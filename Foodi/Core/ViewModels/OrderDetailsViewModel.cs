using Foodi.Core.Enums;
using Foodi.Core.Models;
using System.Linq.Expressions;

namespace Foodi.Core.ViewModels
{
    public class OrderDetailsViewModel : OrderViewModel
    {
       
        public int CustomerId { get; set; }


        public IEnumerable<OrderItemViewModel> OrderItems { get; set; }

        public static Expression<Func<Order,OrderDetailsViewModel>> OrderDetailsSelector
        {
            get
            {
                return o => new OrderDetailsViewModel
                {
                    Id = o.Id,
                    CustomerId = o.UserId,
                    PaymentMethod = o.Payment.PaymentMethod,
                    Total=o.Payment.Amount,
                    Status=o.Status,
                    OrderDate = o.OrderDate,
                    OrderItems= o.OrderItems.Select( i => new OrderItemViewModel
                    {
                        OrderItemId = i.Id,
                        Price=i.UnitPrice,
                        Quantity=i.Quantity,
                        ProductName=i.Product.Name
                    })
                };
            }
        }
    }
}
