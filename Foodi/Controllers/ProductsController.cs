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
    public class ProductsController : Controller
    {
        private readonly IProductsService _ProductsService;
        private readonly ICategoriesService _CategoriesService;

        public ProductsController(IProductsService ProductsService , ICategoriesService categoriesService)
        {
            _ProductsService = ProductsService;
            _CategoriesService = categoriesService;
        }

        [Authorize(Roles = AppRoles.User)]
        public async Task<IActionResult> Menu()
        {

            var products = await _ProductsService.GetAllProducts();
            var categories  =  await _CategoriesService.GetAllCategories();

            var viewModel = new MenuViewModel
            {
                Categories = categories,
                Products = products
            };

            return View(viewModel);
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _ProductsService.GetAllProductsWithCategory();
            return View(products);
        }

        [Authorize(Roles = AppRoles.User)]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _ProductsService.GetAllProducts();
            return PartialView("_ProductsPartial", products);
        }


        [Authorize(Roles = AppRoles.User)]
        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory([FromQuery] int[] categoryIds)
        {
            var products = await _ProductsService.GetProductByCategory(categoryIds);
            return PartialView("_ProductsPartial", products);
        }

        [Authorize(Roles = AppRoles.User)]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var Product = _ProductsService.GetProductById(id);

            if (Product is null)
                return NotFound();

            return View(Product);
        }


        [Authorize(Roles = AppRoles.Admin)]
        [HttpGet]
        public IActionResult Create() 
        {
            ProductCreateViewModel productViewModel = new()
            {
                Categories = _CategoriesService.GetCategoriesSelectList(),
            };
            
            return View(productViewModel);
        }


        [Authorize(Roles = AppRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel creatProductViewModel)
        {
            if (!ModelState.IsValid)
            {
                creatProductViewModel.Categories = _CategoriesService.GetCategoriesSelectList(); 
                return View(creatProductViewModel);
            }
            await _ProductsService.Create(creatProductViewModel);

            TempData["Message"] = "Product added successfully";
            TempData["MessageType"] = "success"; 

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = AppRoles.Admin)]
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _ProductsService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            ProductEditViewModel model = new()
            {
                Id = id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Categories = _CategoriesService.GetCategoriesSelectList(),
                CurrentImage = product.ImageName
            };
            return View(model);
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Update(ProductEditViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.Categories = _CategoriesService.GetCategoriesSelectList();
                return View(model);
            }

            var product = await _ProductsService.Update(model);

            if (product is null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpDelete]
        public IActionResult Delete([FromBody] int id)
        {
            var isDeleted = _ProductsService.Delete(id);
            return isDeleted ? Json(new { success = true, message = "Product deleted successfully" })
                               : Json(new { success = false, message = "Product deleted Failed" });
        }

    }
}
