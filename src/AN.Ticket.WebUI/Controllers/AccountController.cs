using AN.Ticket.Domain.Accounts;
using AN.Ticket.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AN.Ticket.WebUI.Controllers;
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAuthenticate _authenticate;

    public AccountController(
        ILogger<AccountController> logger,
        IAuthenticate authenticate
    )
    {
        _logger = logger;
        _authenticate = authenticate;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (await _authenticate.EmailExists(model.Email))
                {
                    return View(model);
                }

                var result = await _authenticate.RegisterUser(model.FullName, model.Email, model.Password, model.RememberMe);

                if (result.success)
                {
                    return Redirect("/");
                }
                else
                {
                    TempData["info"] = result.msg;
                }
            }

            return View(model);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
