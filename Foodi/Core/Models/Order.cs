using Foodi.Core.Enums;

namespace Foodi.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public int UserId { get; set; }
        public OrderStatus Status { get; set; }
        public int PaymentId { get; set; }
        public DateTime OrderDate { get; set; }


        public User User { get; set; } = default!;
        public Payment Payment { get; set; } = default!;
        public ICollection<OrderItem> OrderItems { get; set; }



    }
}
