using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Foodi.Core.Models
{
    public class Base
    {
        public int Id { get; set; }

        [MaxLength(250)]
        //[Index(IsUnique = true)]
        public string Name { get; set; } = string.Empty;
    }
}
