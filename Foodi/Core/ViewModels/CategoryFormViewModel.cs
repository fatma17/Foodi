using System.ComponentModel.DataAnnotations;

namespace Foodi.Core.ViewModels
{
    public class CategoryFormViewModel
    {
        [MaxLength(250)]
        public string Name { get; set; }
    }
}
