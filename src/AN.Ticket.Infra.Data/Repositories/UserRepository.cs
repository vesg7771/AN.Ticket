using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Infra.Data.Context;
using AN.Ticket.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AN.Ticket.Infra.Data.Repositories;
public class UserRepository
    : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context)
        : base(context)
    { }

    public async Task<IEnumerable<User>> GetAllByIds(List<Guid> ids)
    {
        return await Entities
            .Where(u => ids.Contains(u.Id))
            .ToListAsync();
    }
}
