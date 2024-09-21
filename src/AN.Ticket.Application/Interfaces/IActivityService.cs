using AN.Ticket.Application.DTOs.Activity;
using AN.Ticket.Application.Interfaces.Base;
using AN.Ticket.Domain.Entities;

namespace AN.Ticket.Application.Interfaces;
public interface IActivityService
    : IService<ActivityDto, Activity>
{
}
