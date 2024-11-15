﻿using AN.Ticket.Application.Exceptions;
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

    public async Task<List<Contact>> GetByUserAsync(Guid userId)
        => await Entities.Where(c => c.UserId == userId).ToListAsync();

    public async Task<List<Contact>> GetByIdsAsync(List<Guid> ids)
        => await Entities.Where(c => ids.Contains(c.Id)).ToListAsync();

    public async Task<Contact> GetByIdIncludeUserAsync(Guid id)
    {
        var contact = await Entities.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id);
        if (contact is null)
            throw new NotFoundException($"Contato com ID {id} não encontrado.");
        return contact;
    }
}
