using Foodi.Core.Models;

namespace Foodi.Core.ViewModels
{
    public class MenuViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
