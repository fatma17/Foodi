using Foodi.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Foodi.Core.Repositroy
{
    public interface ICategoriesRepository : IBaseRepository<Category>
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}
