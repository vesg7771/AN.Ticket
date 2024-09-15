using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Infra.Data.Context;
using AN.Ticket.Infra.Data.Repositories.Base;

namespace AN.Ticket.Infra.Data.Repositories;

public class AttachmentRepository
    : Repository<Attachment>, IAttachmentRepository
{
    public AttachmentRepository(ApplicationDbContext context)
        : base(context)
    { }
}
