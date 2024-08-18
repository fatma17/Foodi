namespace Foodi.Core.ViewModels
{
    public class OrderItemViewModel
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
    }
}
