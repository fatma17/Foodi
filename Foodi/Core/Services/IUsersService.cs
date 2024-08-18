using Foodi.Core.Models;
using Foodi.Core.ViewModels;

namespace Foodi.Core.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UserOrderViewModel>> GetAllUser();

        UserDetailsViewModel GetUserById(int UserId);

        int GetUserCount(); 

    }
}
