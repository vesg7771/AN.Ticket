using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Infra.Data.Context;
using AN.Ticket.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AN.Ticket.Infra.Data.Repositories;
public class ContactRepository
    : Repository<Contact>, IContactRepository
{
    public ContactRepository(
        ApplicationDbContext context
    )
        : base(context)
    {
    }

    public async Task<Contact> GetByEmailAsync(string email)
        => await Entities.SingleOrDefaultAsync(c => c.PrimaryEmail == email);

    public async Task<bool> ExistContactCpfAsync(string cpf)
        => await Entities.AnyAsync(c => c.Cpf.Equals(cpf));
}
