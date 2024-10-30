using AN.Ticket.Application.DTOs.Team;
using AN.Ticket.Application.Interfaces.Base;
using AN.Ticket.Domain.Entities;

namespace AN.Ticket.Application.Interfaces;

public interface ITeamService : IService<TeamDto, Team>
{
    Task<IEnumerable<TeamDto>> GetAllTeamsAsync();
    Task<IEnumerable<TeamDto>> GetAllByUserId(Guid userId);
}
