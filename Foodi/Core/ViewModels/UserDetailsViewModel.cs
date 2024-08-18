using Foodi.Core.Enums;
using Foodi.Core.Models;
using System.Linq.Expressions;

namespace Foodi.Core.ViewModels
{
    public class UserDetailsViewModel : UserViewModel
    {
        public DateTime joined { get; set; }
        public IEnumerable<OrderViewModel> Orders { get; set; }

        public static Expression<Func<User, UserDetailsViewModel>> UserDetailsSelector
        {
            get
            {
                return u => new UserDetailsViewModel
                {
                    Id = u.Id,
                    Name = u.FirstName,
                    Email = u.Email,
                    joined = u.CreatedDate,
                    Orders = u.Orders.Select(o => new OrderViewModel()
                    {
                        Id=o.Id,
                        Status = o.Status,
                        OrderDate = o.OrderDate,
                        Total= o.Payment.Amount,
                        PaymentMethod = o.Payment.PaymentMethod
                    })
                };
            }
        }

    }
}
