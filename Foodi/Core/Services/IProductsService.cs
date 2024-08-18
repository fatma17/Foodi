using Foodi.Core.Models;
using Foodi.Core.ViewModels;

namespace Foodi.Core.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> GetAllProductsWithCategory();
        Task<IEnumerable<Product>> GetProductByCategory(int[] categoryIds); 
        Product? GetProductById(int id);
        Task<int> GetProductCount();

        Task Create(ProductCreateViewModel model);
        Task<Product> Update(ProductEditViewModel model);
        bool Delete(int id);
    }
}
