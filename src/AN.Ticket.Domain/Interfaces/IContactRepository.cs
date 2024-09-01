using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces.Base;

namespace AN.Ticket.Domain.Interfaces;
public interface IContactRepository
    : IRepository<Contact>
{
    Task<Contact> GetByEmailAsync(string email);
}
