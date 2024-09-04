using AN.Ticket.Application.DTOs;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Application.Services.Base;
using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces.Base;

namespace AN.Ticket.Application.Services;
public class ContactService
    : Service<ContactDto, Contact>, IContactService
{
    public ContactService(
        IRepository<Contact> repository
    )
        : base(repository)
    {
    }
}
