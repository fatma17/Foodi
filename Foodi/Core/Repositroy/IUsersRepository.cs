using Foodi.Core.Models;
using Foodi.Core.ViewModels;

namespace Foodi.Core.Repositroy
{
    public interface IUsersRepository : IBaseRepository<User>
    {
       Task<IEnumerable<UserOrderViewModel>> GetUserOrderInfosAsync();
       int GetUserCount();

    }
}
