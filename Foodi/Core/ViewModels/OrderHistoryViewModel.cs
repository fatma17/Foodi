using Foodi.Core.Enums;
using Foodi.Core.Models;
using System.Linq.Expressions;

namespace Foodi.Core.ViewModels
{
    public class OrderHistoryViewModel : OrderViewModel
    {
       
        public static Expression<Func<Order, OrderHistoryViewModel>> OrderHistorySelector
        {
            get
            {
                return o => new OrderHistoryViewModel
                {
                    Id = o.Id,
                    PaymentMethod = o.Payment.PaymentMethod,
                    Total = o.Payment.Amount,
                    Status = o.Status,
                    OrderDate = o.OrderDate,
                   
                };
            }
        }
    }
}
