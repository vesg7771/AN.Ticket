using AN.Ticket.Application.DTOs.Ticket;
using AutoMapper;
using DomainEntity = AN.Ticket.Domain.Entities;

namespace AN.Ticket.Application.Mappings;
public class DomainToDTOMappingProfile
    : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<DomainEntity.Ticket, TicketDto>()
            .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages))
            .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
            .ForMember(dest => dest.InteractionHistories, opt => opt.MapFrom(src => src.InteractionHistories))
            .ForMember(dest => dest.SatisfactionRating, opt => opt.MapFrom(src => src.SatisfactionRating))
            .ReverseMap();
    }
}
