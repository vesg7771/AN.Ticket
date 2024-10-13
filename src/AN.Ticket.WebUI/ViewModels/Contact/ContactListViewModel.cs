using AN.Ticket.Application.DTOs.Contact;

namespace AN.Ticket.WebUI.ViewModels.Contact;

public class ContactListViewModel
{
    public List<ContactDto> Contacts { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public string SearchTerm { get; set; }

    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
}
