using Foodi.Core.Enums;

namespace Foodi.Core.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public Order Order{ get; set; }

    } 
}
