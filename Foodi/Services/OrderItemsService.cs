using Foodi.Core;
using Foodi.Core.Services;
using Foodi.Core.ViewModels;

namespace Foodi.Services
{
    public class OrderItemsService : IOrderItemsService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public OrderItemsService(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        public List<BestSellerViewModel> GetBestSellers(int topN)
        {
            var result = _UnitOfWork.OrderItems.GetBestSellers(topN);
            return result;
        }
        public List<ProductViewModel> GetBestProducts(int topN)
        {
            var result = _UnitOfWork.OrderItems.GetBestProducts(topN);
            return result;
        }

    }
}
