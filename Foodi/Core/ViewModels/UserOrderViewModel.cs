using Foodi.Core.Models;
using System.Linq.Expressions;

namespace Foodi.Core.ViewModels
{
    public class UserOrderViewModel: UserViewModel
    {
        public int NumberOfOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public DateTime? LastOrderDate { get; set; }

    }
}
