using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces.Base;

namespace AN.Ticket.Domain.Interfaces;

public interface ITeamRepository : IRepository<Team>
{
    Task<IEnumerable<Team>> GetAllByUserId(Guid userId);
}
