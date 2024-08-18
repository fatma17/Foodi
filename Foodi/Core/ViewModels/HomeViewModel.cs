using Foodi.Core.Models;

namespace Foodi.Core.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<ProductViewModel> BestProducts { get; set; }
        public Contact ContactUs { get; set; }
    }
}
