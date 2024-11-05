using AN.Ticket.Application.DTOs.User;
using System.ComponentModel.DataAnnotations;

namespace AN.Ticket.WebUI.ViewModels.Team;

public class TeamCreationViewModel
{
    [Required(ErrorMessage = "O nome do time é obrigatório.")]
    public string TeamName { get; set; }
    public List<UserDto> AvailableUsers { get; set; } = new List<UserDto>();
    public List<Guid> SelectedUserIds { get; set; } = new List<Guid>();
}
