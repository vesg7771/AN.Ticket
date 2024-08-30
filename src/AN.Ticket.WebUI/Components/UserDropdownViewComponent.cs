using AN.Ticket.Infra.Data.Identity;
using AN.Ticket.WebUI.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AN.Ticket.WebUI.Components;

[ViewComponent]
public class UserDropdownViewComponent : ViewComponent
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserDropdownViewComponent(
        UserManager<ApplicationUser> userManager
    )
        => _userManager = userManager;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (user is null)
            return View("Default", new UserDropdownViewModel());

        var viewModel = new UserDropdownViewModel
        {
            FullName = user.FullName ?? "Usuário Desconhecido",
            Email = user.Email,
            ProfilePicture = user.ProfilePicture
        };

        return View("Default", viewModel);
    }
}
