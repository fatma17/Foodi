using Foodi.Core.Models;
using Foodi.Core.Repositroy;
using Foodi.Infrastructure;
using Foodi.Repository.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Contracts;
using System.Numerics;

namespace Foodi.Core
{
    public interface IUnitOfWork
    {
        ICategoriesRepository Categories { get; }
        IBaseRepository<Product>  Products { get; }
        IUsersRepository Users { get; }
        IBaseRepository<Cart> Carts { get; }
        IBaseRepository<Payment> Payments { get; }

        IBaseRepository<Order> Orders { get; }
        IOrderItemsRepository OrderItems { get; }

        IBaseRepository<Contact> Contacts { get; }

        int Save();

    }
}
