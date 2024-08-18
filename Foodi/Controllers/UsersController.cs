using Foodi.Core.Consts;
using Foodi.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Foodi.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class UsersController : Controller
    {
        private readonly IUsersService _UsersService;

        public UsersController(IUsersService usersService) 
        {
            _UsersService = usersService;
        }

        public async Task<IActionResult> Index()
        {
            return View( await _UsersService.GetAllUser());
        }

        public IActionResult Details(int id)
        {
            var userDetails = _UsersService.GetUserById(id);
            if (userDetails is null)
                return NotFound();

            return View(userDetails);
        }
    }
}
