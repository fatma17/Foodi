using Foodi.Core.Models;
using Foodi.Core.Enums;
using Foodi.Core.Repositroy;
using Foodi.Core.ViewModels;
using Foodi.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Foodi.Repository.Repositories
{
    public class OrderItemsRepository : BaseRepository<OrderItem>, IOrderItemsRepository
    {
        private readonly ApplicationDbContext _DbContext;
        public OrderItemsRepository(ApplicationDbContext DbContext) : base(DbContext)
        {
            _DbContext = DbContext;
        }
        public List<BestSellerViewModel> GetBestSellers(int topN)
        {
            var topProducts = _DbContext.OrderItems.Where(io=>io.Order.Status==OrderStatus.Completed)
                .GroupBy(oi => oi.ProductId)
                .Select(group => new
                {
                    ProductId = group.Key,
                    SalesCount = group.Sum(io => io.Quantity),
                    TotalRevenue = group.Sum(io => io.Quantity * io.UnitPrice),
                    LastSaleDate = group.Max(io => io.Order.OrderDate)
                })
                .OrderByDescending(d => d.SalesCount)
                .Take(topN)
                .Join(_DbContext.Products, ordering => ordering.ProductId, product => product.Id,
                     (ordering, product) => new BestSellerViewModel
                     {
                         Id = product.Id,
                         Name = product.Name,
                         ImageName = product.ImageName,
                         SalesCount = ordering.SalesCount,
                         TotalRevenue = ordering.TotalRevenue,
                         LastSaleDate = ordering.LastSaleDate
                     })
                .ToList();

            return topProducts;
        }
        public List<ProductViewModel> GetBestProducts(int topN)
        {
            var topProducts = _DbContext.OrderItems.Where(io => io.Order.Status == OrderStatus.Completed)
                .GroupBy(oi => oi.ProductId)
                .Select(group => new
                {
                    ProductId = group.Key,
                    SalesCount = group.Sum(io => io.Quantity),
                })
                .OrderByDescending(d => d.SalesCount)
                .Take(topN)
                .Join(_DbContext.Products, ordering => ordering.ProductId, product => product.Id,
                     (ordering, product) => new ProductViewModel
                     {
                         Id = product.Id,
                         Name = product.Name,
                         ImageName = product.ImageName,
                         Price = product.Price
                     })
                .ToList();

            return topProducts;
        }
    }
}
