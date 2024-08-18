using Foodi.Core;
using Foodi.Core.Models;
using Foodi.Core.Services;
using Foodi.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using Foodi.Core.Enums;

namespace Foodi.Services
{
    public class PaymentsService : IPaymentsService
    {
        private readonly IUnitOfWork _UnitOfWork;
        public PaymentsService(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<Payment> Create(PaymentMethod paymentMethod , int Amount)
        {
            var payment = new Payment
            {
                PaymentMethod = paymentMethod,
                Amount = Amount,
                PaymentDate = DateTime.Now
            };
            return await _UnitOfWork.Payments.AddAsync(payment);
        }
    }
}
