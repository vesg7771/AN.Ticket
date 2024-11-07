using AN.Ticket.Application.Interfaces;
using AN.Ticket.WebUI.ViewModels.Team;
using Microsoft.AspNetCore.Mvc;

namespace AN.Ticket.WebUI.Components;

[ViewComponent]
public class TeamViewComponent : ViewComponent
{
    private readonly ITeamService _teamService;
    private readonly IUserService _userService;

    public TeamViewComponent(
        ITeamService teamService,
        IUserService userService
    )
    {
        _teamService = teamService;
        _userService = userService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var teams = await _teamService.GetAllTeamsAsync();
        var users = await _userService.GetAllUsersAsync();
        ViewBag.AvailableUsers = users.ToList();

        var viewModel = new TeamListViewModel
        {
            Teams = teams.ToList()
        };
        return View(viewModel);
    }
}
