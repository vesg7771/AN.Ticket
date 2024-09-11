using AN.Ticket.Application.DTOs.Contact;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Infra.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AN.Ticket.WebUI.Controllers;

[Authorize]
public class ContactController : Controller
{
    private readonly ILogger<ContactController> _logger;
    private readonly IContactService _contactService;
    private readonly UserManager<ApplicationUser> _userManager;

    public ContactController(
        ILogger<ContactController> logger,
        IContactService contactService,
        UserManager<ApplicationUser> userManager
    )
    {
        _logger = logger;
        _contactService = contactService;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult CreateContact() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateContact(ContactCreateDto model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await GetCurrentUserAsync();
        model.UserId = Guid.Parse(user!.Id);

        await _contactService.CreateContactAsync(model);

        return Redirect(nameof(GetContact));
    }

    [HttpGet]
    public async Task<IActionResult> GetContact()
    {
        var contacts = await _contactService.GetAllAsync();

        var contactDTOs = new List<ContactDto>();
        foreach (var contact in contacts)
        {
            contactDTOs.Add(new ContactDto
            {
                Id = contact.Id,
                FullName = contact.GetFullName(),
                PrimaryEmail = contact.PrimaryEmail,
                SecondaryEmail = contact.SecondaryEmail,
                Phone = contact.Phone,
                Mobile = contact.Mobile,
                Department = contact.Department,
            });
        }

        return View(contactDTOs);
    }

    private async Task<ApplicationUser?> GetCurrentUserAsync()
    => await _userManager.GetUserAsync(User);

    private async Task<Guid> GetCurrentUserId()
    {
        var user = await GetCurrentUserAsync();
        return user != null ? Guid.Parse(user.Id) : Guid.Empty;
    }
}
