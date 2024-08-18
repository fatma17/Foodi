using Foodi.Core.Models;

namespace Foodi.Core.Services
{
    public interface IContactsService
    {
        Task<IEnumerable<Contact>> GetAll();

        Task<Contact> Create(Contact contact);
    }
}
