using AN.Ticket.Application.DTOs.User;

namespace AN.Ticket.WebUI.ViewModels.Team;

public class TeamMemberListViewModel
{
    public Guid TeamId { get; set; }
    public List<UserDto> Members { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public string SearchTerm { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
}
