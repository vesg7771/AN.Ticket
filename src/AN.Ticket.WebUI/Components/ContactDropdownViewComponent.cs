using AN.Ticket.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AN.Ticket.WebUI.Components;

[ViewComponent]
public class ContactDropdownViewComponent : ViewComponent
{
    private readonly IContactRepository _contactRepository;

    public ContactDropdownViewComponent(IContactRepository contactRepository)
        => _contactRepository = contactRepository;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var contacts = await _contactRepository.GetAllAsync();
        var viewModel = new ContactDropdownViewModel
        {
            Contacts = contacts.Select(x => new ContactViewModel
            {
                Id = x.Id,
                FullName = x.GetFullName(),
                Email = x.PrimaryEmail
            }).ToList(),
            SelectedContactName = "Selecione um cliente"
        };

        return View(viewModel);
    }
}

public class ContactDropdownViewModel
{
    public List<ContactViewModel> Contacts { get; set; }
    public string SelectedContactName { get; set; }
}

public class ContactViewModel
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}
