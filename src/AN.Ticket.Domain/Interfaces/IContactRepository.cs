using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces.Base;

namespace AN.Ticket.Domain.Interfaces;
public interface IContactRepository
    : IRepository<Contact>
{
    Task<Contact> GetByEmailAsync(string email);
    Task<bool> ExistContactCpfAsync(string cpf);
    Task<List<Contact>> GetByUserAsync(Guid userId);
    Task<List<Contact>> GetByIdsAsync(List<Guid> ids);
}
