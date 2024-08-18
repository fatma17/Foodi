using Foodi.Core.ViewModels;

namespace Foodi.Core.Services
{
    public interface IDashboardService
    {
        
        Task<DashboardViewModel> GetDashboardDataAsync();

    }
}
