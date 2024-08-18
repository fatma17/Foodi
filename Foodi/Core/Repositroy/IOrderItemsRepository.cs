using Foodi.Core.Models;
using Foodi.Core.ViewModels;

namespace Foodi.Core.Repositroy
{
    public interface IOrderItemsRepository : IBaseRepository<OrderItem>
    {
        List<BestSellerViewModel> GetBestSellers(int topN);

        List<ProductViewModel> GetBestProducts(int topN);

    }
}
