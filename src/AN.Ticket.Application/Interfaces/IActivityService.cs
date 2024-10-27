using AN.Ticket.Application.DTOs.Activity;
using AN.Ticket.Application.Helpers.Pagination;
using AN.Ticket.Application.Interfaces.Base;
using AN.Ticket.Domain.Entities;

namespace AN.Ticket.Application.Interfaces;
public interface IActivityService
    : IService<ActivityDto, Activity>
{
    Task<ActivityDto> CreateActivityAsync(ActivityDto model);
    Task<bool> DeleteActivitiesAsync(List<Guid> ids);
    Task DeleteActivityAsync(Guid id);
    Task<PagedResult<ActivityDto>> GetPaginatedActivitiesAsync(int pageNumber, int pageSize, string searchTerm = "");
    Task UpdateActivityAsync(ActivityDto model);
}
