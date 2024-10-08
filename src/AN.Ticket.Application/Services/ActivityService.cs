﻿using AN.Ticket.Application.DTOs.Activity;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Application.Services.Base;
using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.EntityValidations;
using AN.Ticket.Domain.Enums;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Domain.Interfaces.Base;

namespace AN.Ticket.Application.Services;
public class ActivityService
    : Service<ActivityDto, Activity>, IActivityService
{
    private readonly IActivityRepository _activityRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ActivityService(
        IRepository<Activity> repository,
        IActivityRepository activityRepository,
        ITicketRepository ticketRepository,
        IUnitOfWork unitOfWork

    )
        : base(repository)
    {
        _activityRepository = activityRepository;
        _ticketRepository = ticketRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ActivityDto> CreateActivityAsync(ActivityDto model)
    {
        var ticket = await _ticketRepository.GetByIdAsync(model.TicketId);
        if (ticket.Status == TicketStatus.Closed)
            throw new EntityValidationException("Não é possivel criar uma atividade para o ticket, pois está fechado.");

        var activity = new Activity(
            model.Type,
            model.Description ?? "",
            model.ScheduledDate,
            model.TicketId,
            model.Subject,
            model.Duration,
            model.Priority
        );

        ticket.UpdateStatus(TicketStatus.InProgress);

        await _activityRepository.SaveAsync(activity);
        await _unitOfWork.CommitAsync();

        return new ActivityDto
        {
            Id = activity.Id,
            Type = activity.Type,
            Description = activity.Description,
            ScheduledDate = activity.ScheduledDate,
            TicketId = activity.TicketId ?? Guid.Empty,
        };
    }

    public async Task UpdateActivityAsync(ActivityDto model)
    {
        if (!model.Id.HasValue)
            throw new EntityValidationException("O ID da atividade não pode ser nulo.");

        var activity = await _activityRepository.GetByIdAsync(model.Id.Value);
        if (activity is null)
            throw new EntityValidationException("Atividade não encontrada.");

        activity.UpdateActivity(
             model.Subject,
             model.Type,
             model.Description,
             model.ScheduledDate,
             model.Duration,
             model.Priority,
             model.ContactId
         );


        _activityRepository.Update(activity);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteActivityAsync(Guid id)
    {
        var activity = await _activityRepository.GetByIdAsync(id);
        if (activity is null)
            throw new EntityValidationException("Atividade não encontrada.");

        _activityRepository.Delete(activity);
        await _unitOfWork.CommitAsync();
    }
}