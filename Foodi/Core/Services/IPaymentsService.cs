using Foodi.Core.Enums;
using Foodi.Core.Models;
using Foodi.Core.ViewModels;

namespace Foodi.Core.Services
{
    public interface IPaymentsService
    {
        Task<Payment> Create(PaymentMethod paymentMethod, int Amount);
    }
}
