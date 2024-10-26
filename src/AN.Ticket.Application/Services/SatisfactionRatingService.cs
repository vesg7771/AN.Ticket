using AN.Ticket.Application.DTOs.SatisfactionRating;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Enums;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Domain.Interfaces.Base;

namespace AN.Ticket.Application.Services;
public class SatisfactionRatingService
    : ISatisfactionRatingService
{
    private readonly ISatisfactionRatingRepository _satisfactionRatingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SatisfactionRatingService(
        ISatisfactionRatingRepository satisfactionRatingRepository,
        IUnitOfWork unitOfWork
    )
    {
        _satisfactionRatingRepository = satisfactionRatingRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<SatisfactionRatingDto?> GetRatingByTicketIdAsync(Guid ticketId)
    {
        var rating = await _satisfactionRatingRepository.GetByTicketIdAsync(ticketId);

        return rating == null ? null : new SatisfactionRatingDto
        {
            Id = rating.Id,
            Rating = rating.Rating ?? SatisfactionRatingValue.Dissatisfied,
            Comment = rating.Comment,
            TicketId = rating.TicketId
        };
    }

    public async Task SaveOrUpdateRatingAsync(SatisfactionRatingDto ratingDto)
    {
        var rating = await _satisfactionRatingRepository.GetByTicketIdAsync(ratingDto.TicketId);

        if (rating == null)
        {
            await _satisfactionRatingRepository.SaveAsync(new SatisfactionRating
            (
                ratingDto.Rating,
                ratingDto.TicketId,
                ratingDto.Comment ?? string.Empty
            ));
        }
        else
        {
            rating.UpdateRating(ratingDto.Rating, ratingDto.Comment);

            _satisfactionRatingRepository.Update(rating);
        }

        await _unitOfWork.CommitAsync();
    }
}
