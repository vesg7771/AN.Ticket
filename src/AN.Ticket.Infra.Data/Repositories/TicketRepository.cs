using AN.Ticket.Domain.Enums;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Infra.Data.Context;
using AN.Ticket.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using DomainEntity = AN.Ticket.Domain.Entities;

namespace AN.Ticket.Infra.Data.Repositories;

public class TicketRepository
    : Repository<DomainEntity.Ticket>, ITicketRepository
{
    public TicketRepository(
        ApplicationDbContext context
    )
        : base(context)
    {
    }

    public async Task<DomainEntity.Ticket> GetByEmailAndSubjectAsync(string email, string subject)
    {
        return await Entities
            .AsNoTracking()
            .Include(x => x.Messages)
            .FirstOrDefaultAsync(
                x => x.Email == email &&
                x.Subject == subject
            );
    }

    public async Task<IEnumerable<DomainEntity.Ticket>> GetTicketsByUserIdAsync(Guid userId)
    {
        return await Entities
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<DomainEntity.Ticket>> GetTicketsNotAssignedAsync()
    {
        return await Entities
            .AsNoTracking()
            .Where(x => x.UserId == null)
            .ToListAsync();
    }

    public async Task<DomainEntity.Ticket> GetTicketWithDetailsAsync(Guid ticketId)
    {
        return await Entities
            .AsNoTracking()
            .Include(x => x.Messages).ThenInclude(x => x.User)
            .Include(x => x.Activities)
            .Include(x => x.InteractionHistories).ThenInclude(x => x.User)
            .Include(x => x.SatisfactionRating)
            .Include(x => x.User)
            .Include(x => x.Attachments)
            .FirstOrDefaultAsync(x => x.Id == ticketId);
    }

    public async Task<int> GetTicketCodeByIdAsync(Guid ticketId)
    {
        return await Entities
            .AsNoTracking()
            .Where(x => x.Id == ticketId)
            .Select(x => x.TicketCode)
            .SingleAsync();
    }

    public async Task<DomainEntity.Ticket> GetByTicketCodeAsync(int ticketCode)
    {
        return await Entities
            .AsNoTracking()
            .SingleAsync(x => x.TicketCode == ticketCode);
    }

    public async Task<bool> IsTicketClosedAsync(Guid ticketId)
    {
        return await Entities
            .AsNoTracking()
            .Where(x => x.Id == ticketId && x.Status == TicketStatus.Closed)
            .AnyAsync();
    }
}
