using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Infra.Data.Context;
using AN.Ticket.Infra.Data.Repositories.Base;

namespace AN.Ticket.Infra.Data.Repositories;
public class SatisfactionRatingRepository
    : Repository<SatisfactionRating>, ISatisfactionRatingRepository
{
    public SatisfactionRatingRepository(ApplicationDbContext context)
        : base(context)
    { }
}
