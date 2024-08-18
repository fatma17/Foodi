using System.ComponentModel.DataAnnotations;

namespace Foodi.Core.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } 

        public int Price { get; set; }

        public string ImageName { get; set; } 
    }
}
