using AN.Ticket.Application.Interfaces;
using AN.Ticket.Infra.Data.Identity;
using AN.Ticket.WebUI.ViewModels.Team;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AN.Ticket.WebUI.Components;

[ViewComponent]
public class ProfileSettingsViewComponent : ViewComponent
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITeamService _teamService;

    public ProfileSettingsViewComponent(
        UserManager<ApplicationUser> userManager,
        ITeamService teamService
    )
    {
        _userManager = userManager;
        _teamService = teamService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (user is null)
            return View(new ProfileSettingsViewModel());

        var teams = await _teamService.GetAllByUserId(Guid.Parse(user.Id));

        var model = new ProfileSettingsViewModel
        {
            FullName = user.FullName,
            Email = user.Email,
            ProfilePicture = user.ProfilePicture,
            Teams = teams.Select(t => new TeamViewModel
            {
                TeamName = t.Name,
                Members = t.Members.Select(m => new TeamMemberViewModel
                {
                    FullName = m.FullName,
                    Email = m.Email,
                    ProfilePicture = m.ProfilePicture
                }).ToList()
            }).ToList()
        };

        return View(model);
    }
}
