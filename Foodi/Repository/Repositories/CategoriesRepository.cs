using Foodi.Core.Models;
using Foodi.Core.Repositroy;
using Foodi.Repository.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Foodi.Repository.Repositories
{
    public class CategoriesRepository : BaseRepository<Category>, ICategoriesRepository
    {
        private readonly ApplicationDbContext _DbContext;
        public CategoriesRepository(ApplicationDbContext DbContext) : base(DbContext)
        {
            _DbContext = DbContext;
        }

        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _DbContext.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .OrderBy(c => c.Text)
                .AsNoTracking()
                .ToList();
        }

    }
}
