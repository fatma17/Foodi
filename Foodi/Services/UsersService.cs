using Foodi.Core;
using Foodi.Core.Models;
using Foodi.Core.Services;
using Foodi.Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Foodi.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public UsersService(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserOrderViewModel>> GetAllUser()
        {
            return  await _UnitOfWork.Users.GetUserOrderInfosAsync();

        }
        public int GetUserCount()
        {
            return _UnitOfWork.Users.GetUserCount();
        }
        public UserDetailsViewModel GetUserById(int UserId)
        {
            return _UnitOfWork.Users.GetById<UserDetailsViewModel>(u => u.Id == UserId , UserDetailsViewModel.UserDetailsSelector);
        }
    }
}
