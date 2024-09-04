using AN.Ticket.Application.DTOs;
using AN.Ticket.Application.Interfaces.Base;
using AN.Ticket.Domain.Entities;

namespace AN.Ticket.Application.Interfaces;
public interface IContactService
    : IService<ContactDto, Contact>
{
}
