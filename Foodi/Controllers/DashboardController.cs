using Foodi.Core.Consts;
using Foodi.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Foodi.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class DashboardController : Controller
    {

        private readonly IDashboardService _dashboardService; 

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _dashboardService.GetDashboardDataAsync();
            return View(data);
        }

    }
}
