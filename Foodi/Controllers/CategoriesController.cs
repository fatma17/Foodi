using Foodi.Core.Consts;
using Foodi.Core.Models;
using Foodi.Core.Services;
using Foodi.Core.ViewModels;
using Foodi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foodi.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoriesService.GetAllCategories());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryFormViewModel Model)
        {
            if (ModelState.IsValid)
            {
                if (_categoriesService.IsCategoryNameAvailable(Model.Name))
                {
                    ModelState.AddModelError("Name", "Category name already exists.");
                    return View(Model);
                }
                await _categoriesService.Create(Model);

                TempData["Message"] = "Category added successfully";
                TempData["MessageType"] = "success";

                return RedirectToAction(nameof(Index));
            }
            return View(Model);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var category = _categoriesService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category EditCategory)
        {
            if (ModelState.IsValid)
            {
                if (_categoriesService.IsCategoryNameAvailable(EditCategory.Name))
                {
                    ModelState.AddModelError("Name", "Category name already exists.");
                    return View(EditCategory);
                }
                _categoriesService.Update(EditCategory);
                return RedirectToAction(nameof(Index));
            }

            return View(EditCategory);
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] int id)
        {
            if (id == 0)
            {
                return Json(new { success = false, message = "Invalid ID" });
            }
            var isDeleted = _categoriesService.Delete(id);
            return isDeleted ? Json(new { success = true, message = "Category deleted successfully" })
                               : Json(new { success = false, message = "Category deleted Failed" });
        }
    }
}
