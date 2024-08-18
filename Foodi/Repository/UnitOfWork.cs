using Foodi.Core;
using Foodi.Core.Models;
using Foodi.Core.Repositroy;
using Foodi.Repository.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using static Azure.Core.HttpHeader;
using System.Numerics;
using Foodi.Repository.Data;

namespace Foodi.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICategoriesRepository Categories { get; private set; }
        public IBaseRepository<Product> Products { get; private set; }
        public IUsersRepository Users { get; private set; }
        public IBaseRepository<Cart> Carts { get; private set; }
        public IBaseRepository<Payment> Payments { get; private set; }

        public IBaseRepository<Order> Orders { get; private set; }

        public IOrderItemsRepository OrderItems { get; private set; }
        
        public IBaseRepository<Contact> Contacts { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Categories = new CategoriesRepository(_context);
            Products = new BaseRepository<Product>(_context);
            Users = new UsersRepository(_context);
            Carts = new BaseRepository<Cart>(_context);
            Orders = new BaseRepository<Order>(_context);
            Payments = new BaseRepository<Payment>(_context);
            Contacts = new BaseRepository<Contact>(_context);
            OrderItems = new OrderItemsRepository(_context);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
