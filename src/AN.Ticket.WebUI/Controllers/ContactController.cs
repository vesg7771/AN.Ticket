using AN.Ticket.Application.DTOs.Contact;
using AN.Ticket.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AN.Ticket.WebUI.Controllers;

public class ContactController : Controller
{
    private readonly ILogger<ContactController> _logger;
    private readonly IContactService _contactService;

    public ContactController(
        ILogger<ContactController> logger,
        IContactService contactService
    )
    {
        _logger = logger;
        _contactService = contactService;
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
}
