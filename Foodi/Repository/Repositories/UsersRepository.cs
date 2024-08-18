using Foodi.Core.Consts;
using Foodi.Core.Models;
using Foodi.Core.Enums;

using Foodi.Core.Repositroy;
using Foodi.Core.ViewModels;
using Foodi.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Foodi.Repository.Repositories
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UsersRepository(ApplicationDbContext DbContext) : base(DbContext)
        {
            _dbContext = DbContext;
        }
        public async Task<IEnumerable<UserOrderViewModel>> GetUserOrderInfosAsync()
        {
            var roleName = AppRoles.User;

            var result = await _dbContext.Users
                .Join(_dbContext.UserRoles,
                    user => user.Id,
                    userRole => userRole.UserId,
                    (user, userRole) => new { user, userRole })
                .Join(_dbContext.Roles,
                    userUserRole => userUserRole.userRole.RoleId,
                    role => role.Id,
                    (userUserRole, role) => new { userUserRole.user, role })
                .Where(userRole => userRole.role.Name == roleName)
                .Select(userRole => new UserOrderViewModel
                {  
                    Id = userRole.user.Id,
                    Name = userRole.user.FirstName,
                    Email = userRole.user.Email,
                    NumberOfOrders = userRole.user.Orders.Count(),
                    TotalSpent = userRole.user.Orders.Where(o=> o.Status==OrderStatus.Completed).Select(o => o.Payment).Sum(p => p.Amount),
                    LastOrderDate = userRole.user.Orders.Max(o => (DateTime?)o.OrderDate)
                })
                .ToListAsync();

            return result;
        }
        public int GetUserCount()
        {
            var role = _dbContext.Roles.First(r => r.Name == AppRoles.User);
            var count = _dbContext.UserRoles.Count(r => r.RoleId == role.Id);
            return count;
        }
    }
}
