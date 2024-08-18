using Foodi.Core.Models;
using Foodi.Core;
using Foodi.Core.Services;
using Foodi.Core.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Foodi.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public ProductsService(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _UnitOfWork.Products.GetAll();
        }
        public async Task<IEnumerable<Product>> GetAllProductsWithCategory()
        {
            return await _UnitOfWork.Products.GetAll(["Category"]);
        }

        public Product? GetProductById(int id)
        {
            return _UnitOfWork.Products.GetById(id);
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(int[] categoryIds)
        {
             return await _UnitOfWork.Products.FindAllAsync(p => categoryIds.Contains(p.CategoryId)); 
        }

        public async Task<int> GetProductCount()
        {
            return await _UnitOfWork.Products.CountAsync();
        }

        public async Task Create(ProductCreateViewModel model)
        {
            string imagename = await ImageService.SaveImage(model.Image);

            Product product = new()
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                CategoryId= model.CategoryId,
                ImageName = imagename,
                CreatedDate = DateTime.Now
            };
            await _UnitOfWork.Products.AddAsync(product);
            _UnitOfWork.Save();
        }

        public async Task<Product> Update(ProductEditViewModel model)
        {
            var product = _UnitOfWork.Products.Find(p=>p.Id==model.Id);

            if (product == null)
                return null;

            var hasNewImage = model.Image != null;  // if(model.Image != null)return true
            var oldImage = product.ImageName;

            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.CategoryId = model.CategoryId;
           
            if (hasNewImage)
            {
                product.ImageName = await ImageService.SaveImage(model.Image);
            }

            var result = _UnitOfWork.Save();

            if (result > 0)
            {
                if (hasNewImage)
                {
                    //delete old image
                    var image = Path.Combine("wwwroot/images", oldImage);
                    File.Delete(image);
                }
                return product;
            }
            else
            {
                if (hasNewImage)
                {
                    //delete new image
                    var image = Path.Combine("wwwroot/images", product.ImageName);
                    File.Delete(image);
                }
                return null;
            }
        }

        public bool Delete(int id)
        {
            var isDeleted = false;

            var product = _UnitOfWork.Products.Find(c => c.Id == id);

            if (product == null)
                return isDeleted;

            _UnitOfWork.Products.Delete(product);

            var effectRows = _UnitOfWork.Save();
            if (effectRows > 0)
            {

                isDeleted = true;
                var image = Path.Combine("wwwroot/images", product.ImageName);
                File.Delete(image);

            }
            return isDeleted;
        }

       
    }
}


