using Foodi.Core.Models;
using Foodi.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Foodi.Core.Services
{
    public interface ICategoriesService
    {
        Category GetCategoryById(int id);
        Task<IEnumerable<Category>> GetAllCategories();
        IEnumerable<SelectListItem> GetCategoriesSelectList();
        bool IsCategoryNameAvailable(string name);

        Task<int> GetCategoryCount();

        Task Create(CategoryFormViewModel creatCategoryViewModel);
        Task Update(Category category);
        bool Delete(int id);
    }
}
