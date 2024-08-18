using Foodi.Core.Services;
using Foodi.Core.ViewModels;

namespace Foodi.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IOrdersService _orderService;
        private readonly IUsersService _userService;
        private readonly IProductsService _productsService;
        private readonly IOrderItemsService _orderItemsService;
        private readonly ICategoriesService _CategoriesService;




        public DashboardService(IOrdersService orderService, IUsersService userService, IProductsService productsService, 
                                IOrderItemsService orderItemsService ,  ICategoriesService categoriesService)
        {

            _orderService = orderService;
            _userService = userService;
            _productsService = productsService;
            _orderItemsService = orderItemsService;
            _CategoriesService = categoriesService;
        }
        public async Task<DashboardViewModel> GetDashboardDataAsync() 
        {
            return new DashboardViewModel
            {
                OrderCount =  await _orderService.GetOrderCount(),
                UserCount =  _userService.GetUserCount(),
                ProductCount = await _productsService.GetProductCount(),
                CategoryCount= await _CategoriesService.GetCategoryCount(),
                Bestsellers = _orderItemsService.GetBestSellers(6)
            };
        }
    }
}
