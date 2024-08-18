using Foodi.Core.Enums;

namespace Foodi.Core.ViewModels
{
    public class OrderUpdateStatusViewModel
    {
        public int OrderId { get; set; }
        public OrderStatus NewStatus { get; set; }
    }
}
