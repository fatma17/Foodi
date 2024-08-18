namespace Foodi.Core.ViewModels
{
    public class BestSellerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        public int SalesCount { get; set; }
        public decimal TotalRevenue { get; set; }
        public DateTime LastSaleDate { get; set; }
    }
}
