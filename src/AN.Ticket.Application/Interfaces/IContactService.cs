using AN.Ticket.Application.DTOs.Contact;
using AN.Ticket.Application.Interfaces.Base;
using AN.Ticket.Domain.Entities;

namespace AN.Ticket.Application.Interfaces;
public interface IContactService
    : IService<ContactDto, Contact>
{
    Task CreateContactAsync(ContactCreateDto contactCreateDto);
}
