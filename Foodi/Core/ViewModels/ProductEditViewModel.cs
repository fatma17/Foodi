using Foodi.Core.Attributes;
using Foodi.Core.Consts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Foodi.Core.ViewModels
{
    public class ProductEditViewModel
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public int Price { get; set; }

        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;


        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public string? CurrentImage { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

        [AllowedExtensions(FileSettings.AllowedExtensions)
         , MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Image { get; set; } = default!;
    }
}
