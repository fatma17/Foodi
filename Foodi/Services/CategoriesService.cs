using Foodi.Core;
using Foodi.Core.Models;
using Foodi.Core.Services;
using Foodi.Core.ViewModels;
using Foodi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Foodi.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IUnitOfWork _UnitOfWork ;

        public CategoriesService(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public Category GetCategoryById(int id)
        {
            return _UnitOfWork.Categories.GetById(id);
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _UnitOfWork.Categories.GetAll();
        }

        public  IEnumerable<SelectListItem> GetCategoriesSelectList()
        {
            return _UnitOfWork.Categories.GetSelectList();

        }

        public bool IsCategoryNameAvailable(string name)
        {
            return _UnitOfWork.Categories.Any(c => c.Name == name);
        }

        public async Task<int> GetCategoryCount()
        {
            return await _UnitOfWork.Categories.CountAsync();
        }
        public async Task Create(CategoryFormViewModel creatCategoryViewModel)
        {
            Category category = new()
            {
                Name = creatCategoryViewModel.Name,
                CreatedDate = DateTime.Now
            };
            await _UnitOfWork.Categories.AddAsync(category);
            _UnitOfWork.Save();
        }

        public Task Update(Category EditCategory)
        {
            var category = _UnitOfWork.Categories.Find(c=>c.Id== EditCategory.Id);
            if (category == null)
                return null;

            category.Name = EditCategory.Name;

            _UnitOfWork.Categories.Update(category);
            _UnitOfWork.Save();

            return Task.CompletedTask;
        }

        public bool Delete(int id)
        {
            var isDeleted = false;

            var category = _UnitOfWork.Categories.Find(c => c.Id == id);

            if (category == null)
                return isDeleted;

            _UnitOfWork.Categories.Delete(category);

                var effectRows = _UnitOfWork.Save();
            if (effectRows > 0)
            {
                isDeleted = true;
            }

            return isDeleted;
        }
        
    }
}
