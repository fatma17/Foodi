using Foodi.Core.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Foodi.Core.Models
{
    public class Category : Base
    {
       // public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}







