using AN.Ticket.Application.DTOs.User;

namespace AN.Ticket.Application.DTOs.Team;
public class TeamDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<UserDto> Members { get; set; }
}
