using AN.Ticket.Application.DTOs.Activity;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Application.Services.Base;
using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Domain.Interfaces.Base;

namespace AN.Ticket.Application.Services;
public class ActivityService
    : Service<ActivityDto, Activity>, IActivityService
{
    private readonly IActivityRepository _activityRepository;
    public ActivityService(
        IRepository<Activity> repository,
        IActivityRepository activityRepository
    )
        : base(repository)
    {
        _activityRepository = activityRepository;
    }

    public async Task<ActivityDto> CreateActivityAsync(ActivityDto model)
    {
        var activity = new Activity(
            model.Type,
            model.Description ?? "",
            model.ScheduledDate,
            model.TicketId
        );

        await _activityRepository.SaveAsync(activity);

        return new ActivityDto
        {
            Id = activity.Id,
            Type = activity.Type,
            Description = activity.Description,
            ScheduledDate = activity.ScheduledDate,
            TicketId = activity.TicketId ?? Guid.Empty,
        };
    }
}
