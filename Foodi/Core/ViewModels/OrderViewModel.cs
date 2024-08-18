using Foodi.Core.Enums;

namespace Foodi.Core.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public int Total { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
