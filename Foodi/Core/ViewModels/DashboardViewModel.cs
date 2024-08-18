namespace Foodi.Core.ViewModels
{
    public class DashboardViewModel
    {
        public int ProductCount { get; set; }
        public int UserCount { get; set; }
        public int OrderCount { get; set; }
        public int CategoryCount { get; set; }
        public List<BestSellerViewModel> Bestsellers { get; set; }
       

    }
}
