using AN.Ticket.Application.DTOs.User;
using AN.Ticket.Application.Interfaces.Base;
using AN.Ticket.Domain.Entities;

namespace AN.Ticket.Application.Interfaces;

public interface IUserService : IService<UserDto, User>
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
}
