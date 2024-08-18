namespace Foodi.Core.Models
{
    public class Product : Base
    {
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageName { get; set; }

        //public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CategoryId {  get; set; }

        public Category Category { get; set; } = default!;
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();




    }
}
