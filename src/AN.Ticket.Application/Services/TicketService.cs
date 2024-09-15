using AN.Ticket.Application.DTOs.Email;
using AN.Ticket.Application.DTOs.Ticket;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Application.Services.Base;
using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.EntityValidations;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Domain.Interfaces.Base;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using DomainEntity = AN.Ticket.Domain.Entities;

namespace AN.Ticket.Application.Services;
public class TicketService
    : Service<TicketDto, DomainEntity.Ticket>, ITicketService
{
    private readonly IRepository<DomainEntity.Ticket> _ticketService;
    private readonly ITicketRepository _ticketRepository;
    private readonly ITicketMessageRepository _ticketMessageRepository;
    private readonly IActivityRepository _activityRepository;
    private readonly ISatisfactionRatingRepository _satisfactionRatingRepository;
    private readonly IEmailSenderService _emailSenderService;
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public TicketService(
        IRepository<DomainEntity.Ticket> service,
        ITicketRepository ticketRepository,
        ITicketMessageRepository ticketMessageRepository,
        IActivityRepository activityRepository,
        ISatisfactionRatingRepository satisfactionRatingRepository,
        IEmailSenderService emailSenderService,
        IAttachmentRepository attachmentRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
        : base(service)
    {
        _ticketService = service;
        _ticketRepository = ticketRepository;
        _ticketMessageRepository = ticketMessageRepository;
        _activityRepository = activityRepository;
        _satisfactionRatingRepository = satisfactionRatingRepository;
        _emailSenderService = emailSenderService;
        _attachmentRepository = attachmentRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CreateTicketAsync(CreateTicketDto createTicket)
    {
        var ticket = new DomainEntity.Ticket(
            createTicket.Name,
            createTicket.AccountName,
            createTicket.Email,
            createTicket.Phone,
            createTicket.Subject,
            createTicket.Status,
            createTicket.DueDate,
            createTicket.Priority
        );

        if (createTicket.UserId != Guid.Empty)
            ticket.AssignUsers(createTicket.UserId);

        if (createTicket.AttachmentFile != null && createTicket.AttachmentFile.Length > 0)
        {
            if (createTicket.AttachmentFile.Length > 10485760)
            {
                throw new EntityValidationException("O arquivo excede o tamanho máximo permitido de 10 MB.");
            }

            using var memoryStream = new MemoryStream();
            await createTicket.AttachmentFile.CopyToAsync(memoryStream);

            var attachment = new Attachment(
                createTicket.AttachmentFile.FileName,
                memoryStream.ToArray(),
                createTicket.AttachmentFile.ContentType,
                ticket.Id
            );

            ticket.AddAttachment(attachment);

            await _attachmentRepository.SaveAsync(attachment);
        }

        var ticketMessage = new TicketMessage(
            createTicket.Description,
            DateTime.UtcNow.AddHours(-3)
        );

        if (createTicket.UserId != Guid.Empty)
            ticketMessage.AssignUser(createTicket.UserId);

        ticketMessage.AssignTicket(ticket.Id);

        var listTicketMessages = new List<TicketMessage> { ticketMessage };
        ticket.AddMessages(listTicketMessages);

        await _ticketRepository.SaveAsync(ticket);
        await _ticketMessageRepository.SaveAsync(ticketMessage);
        await _unitOfWork.CommitAsync();

        var code = await _ticketRepository.GetTicketCodeByIdAsync(ticket.Id);

        await _emailSenderService.SendEmailAsync(
            ticket.Email,
            ticket.Subject,
            $@"
            <html>
                <body>
                    <h2 style='color: #0056b3;'>Olá {ticket.ContactName},</h2>
                    <p>#{code}</p>
                    <p>Obrigado por entrar em contato com o nosso suporte! Seu ticket foi <strong>criado com sucesso</strong> e nossa equipe já está trabalhando para resolvê-lo. Se você tiver mais detalhes ou informações para adicionar, basta responder este e-mail.</p>
            
                    <h3>Detalhes do Ticket:</h3>
                    <ul style='list-style-type: none; padding: 0;'>
                        <li><strong>Assunto:</strong> {ticket.Subject}</li>
                        <li><strong>Descrição:</strong> {createTicket.Description}</li>
                    </ul>
            
                    <hr style='border: 0; border-top: 1px solid #eee;' />
            
                    <p style='font-size: 14px; color: #777;'>Atenciosamente,<br />Equipe de Suporte</p>
                </body>
            </html>"
        );

        return true;
    }

    public async Task<IEnumerable<TicketDto>> GetTicketsByUserIdAsync(Guid userId)
    {
        var userTickets = await _ticketRepository.GetTicketsByUserIdAsync(userId);
        return userTickets.Select(t => new TicketDto
        {
            Id = t.Id,
            ContactName = t.ContactName,
            AccountName = t.AccountName,
            Email = t.Email,
            Phone = t.Phone,
            Subject = t.Subject,
            Status = t.Status,
            Priority = t.Priority,
            DueDate = t.DueDate,
            ClosedAt = t.ClosedAt,
            FirstResponseAt = t.FirstResponseAt
        }).ToList();
    }

    public async Task<IEnumerable<TicketDto>> GetTicketsNotAssignedAsync()
    {
        var tickets = await _ticketRepository.GetTicketsNotAssignedAsync();

        return tickets.Select(t => new TicketDto
        {
            Id = t.Id,
            ContactName = t.ContactName,
            AccountName = t.AccountName,
            Email = t.Email,
            Phone = t.Phone,
            Subject = t.Subject,
            Status = t.Status,
            Priority = t.Priority,
            DueDate = t.DueDate,
            ClosedAt = t.ClosedAt,
            FirstResponseAt = t.FirstResponseAt
        }).ToList();
    }

    public async Task<bool> AssignTicketToUserAsync(Guid ticketId, Guid userId)
    {
        var ticket = await _ticketRepository.GetByIdAsync(ticketId);
        if (ticket is null || ticket.UserId != null)
        {
            return false;
        }

        ticket.AssignUsers(userId);
        _ticketRepository.Update(ticket);

        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<TicketDetailsDto> GetTicketDetailsAsync(Guid ticketId)
    {
        var ticket = await _ticketRepository.GetTicketWithDetailsAsync(ticketId);
        if (ticket == null)
        {
            return null;
        }


        var ticketDetailsDto = new TicketDetailsDto
        {
            Ticket = _mapper.Map<TicketDto>(ticket),
        };

        return ticketDetailsDto;
    }

    public async Task<bool> ResolveTicketAsync(TicketResolutionDto ticketResolutionDto)
    {
        var ticket = await _ticketRepository.GetByIdAsync(ticketResolutionDto.TicketId);
        if (ticket is null)
        {
            return false;
        }

        ticket.SetResolution(ticketResolutionDto.ResolutionDetails);
        if (
            ticket.FirstResponseAt is null &&
            ticket.FirstResponseAt != DateTime.MinValue
        )
            ticket.RecordFirstResponse();

        ticket.CloseTicket();

        _ticketRepository.Update(ticket);

        if (ticketResolutionDto.NotifyContact)
        {
            await _emailSenderService.SendEmailResponseAsync(
                ticket.Email,
                "Ticket Resolvido",
                $@"
                <html>
                    <body>
                        <h1>Ticket Fechado</h1>
                        <p>O ticket com o código {ticket.TicketCode} foi fechado.</p>
                        <p>Assunto: {ticket.Subject}</p>
                        <p>Data de Fechamento: {ticket.ClosedAt?.ToString("dd/MM/yyyy HH:mm:ss")}</p>
                        <p>Obrigado por utilizar nossos serviços.</p>
                    </body>
                </html>",
                ticket.EmailMessageId!
            );
        }

        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> ReplyToTicketAsync(Guid ticketId, string messageText, Guid userId, List<IFormFile> attachments)
    {
        var ticket = await _ticketRepository.GetTicketWithDetailsAsync(ticketId);
        if (ticket is null)
        {
            return false;
        }

        var ticketMessage = new TicketMessage(messageText, DateTime.UtcNow.ToLocalTime());
        ticketMessage.AssignUser(userId);

        ticketMessage.AssignTicket(ticketId);
        ticket.AddMessages(new List<TicketMessage> { ticketMessage });

        var emailAttachments = new List<EmailAttachment>();
        if (attachments != null && attachments.Any())
        {
            foreach (var file in attachments)
            {
                if (file.Length > 10485760)
                {
                    throw new EntityValidationException("O arquivo excede o tamanho máximo permitido de 10 MB.");
                }

                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);

                var attachment = new Attachment(file.FileName, memoryStream.ToArray(), file.ContentType, ticket.Id);
                ticket.AddAttachment(attachment);
                emailAttachments.Add(new EmailAttachment(file.FileName, memoryStream.ToArray(), file.ContentType));
            }
        }

        await _ticketMessageRepository.SaveAsync(ticketMessage);
        await _unitOfWork.CommitAsync();

        await _emailSenderService.SendEmailResponseAsync(
            ticket.Email,
            $"{ticket.Subject}",
            $@"
            <html>
                <body>
                    <h1 style='color: #0056b3;'>Resposta ao seu ticket</h1>
                    <p>Olá {ticket.ContactName},</p>
                    <p>Você recebeu uma nova resposta ao seu ticket:</p>
                    <p>#{ticket.TicketCode}</p>
                    <p><strong>Mensagem:</strong> {messageText}</p>
                    <p>Se precisar de mais assistência, por favor, responda a este e-mail.</p>
                    <p>Obrigado por utilizar nossos serviços!</p>
                    <hr style='border: 0; border-top: 1px solid #eee;' />
                    <p style='font-size: 14px; color: #777;'>Atenciosamente,<br />Equipe de Suporte</p>
                </body>
            </html>",
            ticket.EmailMessageId!,
            emailAttachments
        );

        return true;
    }
}
