using AN.Ticket.Application.DTOs.Contact;
using AN.Ticket.Application.Extensions;
using AN.Ticket.Application.Helpers.Pagination;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Application.Services.Base;
using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.EntityValidations;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Domain.Interfaces.Base;
using AN.Ticket.Domain.ValueObjects;

namespace AN.Ticket.Application.Services;
public class ContactService
    : Service<ContactDto, Contact>, IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentPlanRepository _paymentPlanRepository;

    public ContactService(
        IRepository<Contact> repository,
        IContactRepository contactRepository,
        IPaymentRepository paymentRepository,
        IUnitOfWork unitOfWork,
        IPaymentPlanRepository paymentPlanRepository
    )
        : base(repository)
    {
        _contactRepository = contactRepository;
        _paymentRepository = paymentRepository;
        _unitOfWork = unitOfWork;
        _paymentPlanRepository = paymentPlanRepository;

    }

    public async Task<bool> CreateContactAsync(
        ContactCreateDto contactCreateDto
    )
    {
        if (!CpfValidator.Validate(contactCreateDto.Cpf))
            throw new EntityValidationException("Cpf inválido.");

        var existingContact = await _contactRepository.ExistContactCpfAsync(contactCreateDto.Cpf);
        if (existingContact)
            throw new EntityValidationException("Contato já existe com esse cpf.");

        var contact = new Contact(
            contactCreateDto.FirstName!,
            contactCreateDto.LastName!,
            contactCreateDto.PrimaryEmail!,
            contactCreateDto.SecondaryEmail!,
            contactCreateDto.Phone!,
            contactCreateDto.Mobile!,
            contactCreateDto.Department!,
            contactCreateDto.Title!
        );

        contact.ChangedCpf(contactCreateDto.Cpf);

        if (contactCreateDto.SocialNetworks != null)
        {
            foreach (var socialNetworkDto in contactCreateDto.SocialNetworks)
            {
                contact.AddSocialNetwork(new SocialNetwork(
                    socialNetworkDto.Name!,
                    socialNetworkDto.Url!,
                    contact.Id
                ));
            }
        }

        if (contactCreateDto.UserId != Guid.Empty)
            contact.AssignUser(contactCreateDto.UserId);

        await _contactRepository.SaveAsync(contact);

        var planPrice = await _paymentPlanRepository.GetByIdAsync(contactCreateDto.PaymentPlanId);
        var payments = new List<Payment>();
        for (int i = 0; i < 12; i++)
        {
            payments.Add(new Payment(
                contact.Id,
                planPrice.Value,
                DateTime.Now.AddMonths(i),
                planPrice.Id
            ));
        }

        foreach (var payment in payments)
            await _paymentRepository.SaveAsync(payment);

        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<PagedResult<ContactDto>> GetPaginatedContactsAsync(Guid userId, int pageNumber, int pageSize, string searchTerm = "")
    {
        var contactsQuery = await _contactRepository.GetByUserAsync(userId);

        var filteredContacts = contactsQuery.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            filteredContacts = filteredContacts.Where(c =>
                c.FirstName.Contains(searchTerm) ||
                c.LastName.Contains(searchTerm) ||
                c.PrimaryEmail.Contains(searchTerm));
        }

        var totalItems = filteredContacts.Count();

        var contacts = filteredContacts
            .OrderBy(c => c.FirstName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var contactDTOs = contacts.Select(c => new ContactDto
        {
            Id = c.Id,
            FullName = c.GetFullName(),
            PrimaryEmail = c.PrimaryEmail,
            SecondaryEmail = c.SecondaryEmail,
            Phone = c.Phone,
            Mobile = c.Mobile,
            Department = c.Department,
            Title = c.Title,
            CreatedAt = c.CreatedAt
        }).ToList();

        return new PagedResult<ContactDto>
        {
            Items = contactDTOs,
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<bool> DeleteContactsAsync(List<Guid> ids)
    {
        var contacts = await _contactRepository.GetByIdsAsync(ids);
        if (!contacts.Any()) return false;

        var contactIds = contacts.Select(x => x.Id).ToList();

        foreach (var contact in contacts)
            _contactRepository.Delete(contact);

        var payments = await _paymentRepository.GetByContacts(contactIds);
        if (payments.Any())
        {
            foreach (var payment in payments)
                _paymentRepository.Delete(payment);
        }

        await _unitOfWork.CommitAsync();
        return true;
    }

    public async Task<bool> UpdateContactAsync(ContactCreateDto contactCreateDto)
    {
        var contact = await _contactRepository.GetByIdAsync(contactCreateDto.Id);
        if (contact is null)
            return false;

        contact.Update(
            contactCreateDto.FirstName!,
            contactCreateDto.LastName!,
            contactCreateDto.Cpf!,
            contactCreateDto.PrimaryEmail!,
            contactCreateDto.SecondaryEmail!,
            contactCreateDto.Phone!,
            contactCreateDto.Mobile!,
            contactCreateDto.Department!,
            contactCreateDto.Title!
        );

        var contactIds = new List<Guid> { contact.Id };

        var payments = await _paymentRepository.GetByContacts(contactIds);
        var existingPaymentPlanId = payments.FirstOrDefault()?.PaymentPlanId;

        if (existingPaymentPlanId != contactCreateDto.PaymentPlanId)
        {
            foreach (var payment in payments)
                _paymentRepository.Delete(payment);

            var planPrice = await _paymentPlanRepository.GetByIdAsync(contactCreateDto.PaymentPlanId);
            var newPayments = new List<Payment>();
            for (int i = 0; i < 12; i++)
            {
                newPayments.Add(new Payment(
                    contact.Id,
                    planPrice.Value,
                    DateTime.Now.AddMonths(i),
                    planPrice.Id
                ));
            }

            foreach (var payment in newPayments)
                await _paymentRepository.SaveAsync(payment);
        }

        _contactRepository.Update(contact);
        await _unitOfWork.CommitAsync();
        return true;
    }
}
