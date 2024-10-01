using AN.Ticket.Application.Interfaces;
using AN.Ticket.Infra.Data.Identity;
using AN.Ticket.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AN.Ticket.WebUI.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHomeService _homeService;

    public HomeController(
        ILogger<HomeController> logger,
        UserManager<ApplicationUser> userManager,
        IHomeService homeService
    )
    {
        _logger = logger;
        _userManager = userManager;
        _homeService = homeService;
    }

    public async Task<IActionResult> Index(DateTime? selectedDate, bool showInProgress = true)
    {
        var userId = await GetCurrentUserId();

        DateTime filterDate = selectedDate ?? DateTime.Now;

        var startOfWeek = filterDate.AddDays(-(int)filterDate.DayOfWeek + (int)DayOfWeek.Monday);
        var endOfWeek = startOfWeek.AddDays(6);

        ViewBag.SelectedDate = filterDate;
        ViewBag.ShowInProgress = showInProgress;

        var homeData = await _homeService.GetHomeData(userId, startOfWeek, endOfWeek, showInProgress);

        return View(homeData);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private async Task<ApplicationUser?> GetCurrentUserAsync()
    => await _userManager.GetUserAsync(User);

    private async Task<Guid> GetCurrentUserId()
    {
        var user = await GetCurrentUserAsync();
        return user != null ? Guid.Parse(user.Id) : Guid.Empty;
    }
}
