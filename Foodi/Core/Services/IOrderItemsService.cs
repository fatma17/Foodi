using Foodi.Core.ViewModels;

namespace Foodi.Core.Services
{
    public interface IOrderItemsService
    {
        List<BestSellerViewModel> GetBestSellers(int topN);
        List<ProductViewModel> GetBestProducts(int topN);


    }
}
