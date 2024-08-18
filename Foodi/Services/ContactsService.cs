using Foodi.Core;
using Foodi.Core.Models;
using Foodi.Core.Services;
using Stripe.Climate;

namespace Foodi.Services
{
    public class ContactsService : IContactsService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public ContactsService(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await _UnitOfWork.Contacts.GetAll();
        }
        public async Task<Contact> Create(Contact contact)
        {
            contact.ContactDate = DateTime.Now;
            await _UnitOfWork.Contacts.AddAsync(contact);
            _UnitOfWork.Save();
            return contact;
        }

    }
}
