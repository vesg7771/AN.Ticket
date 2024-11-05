using AN.Ticket.Application.DTOs.User;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Application.Services.Base;
using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Domain.Interfaces.Base;
using AutoMapper;

namespace AN.Ticket.Application.Services;

public class UserService : Service<UserDto, User>, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(
        IRepository<User> repository,
        IUserRepository userRepository,
        IMapper mapper
    )
        : base(repository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}
