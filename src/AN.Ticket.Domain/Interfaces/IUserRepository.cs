using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces.Base;

namespace AN.Ticket.Domain.Interfaces;
public interface IUserRepository
    : IRepository<User>
{
    Task<IEnumerable<User>> GetAllByIds(List<Guid> ids);
}
