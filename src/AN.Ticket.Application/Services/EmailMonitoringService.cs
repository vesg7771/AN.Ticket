using AN.Ticket.Application.DTOs.Email;
using AN.Ticket.Application.Helpers.EmailSender;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Enums;
using AN.Ticket.Domain.Extensions;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Domain.Interfaces.Base;
using AN.Ticket.Hangfire.Enums;
using Hangfire;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using DomainEntity = AN.Ticket.Domain.Entities;

namespace AN.Ticket.Application.Services;
public class EmailMonitoringService : IEmailMonitoringService
{
    private readonly ILogger<EmailMonitoringService> _logger;
    private readonly SmtpSettings _smtpSettings;
    private readonly IContactRepository _contactRepository;
    private readonly IEmailSenderService _emailSenderService;
    private readonly ITicketRepository _ticketRepository;
    private readonly ITicketMessageRepository _ticketMessageRepository;
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EmailMonitoringService(
        ILogger<EmailMonitoringService> logger,
        IOptions<SmtpSettings> smtpSettings,
        IContactRepository contactRepository,
        IEmailSenderService emailSenderService,
        ITicketRepository ticketRepository,
        ITicketMessageRepository ticketMessageRepository,
        IAttachmentRepository attachmentRepository,
        IUnitOfWork unitOfWork
    )
    {
        _smtpSettings = smtpSettings.Value;
        _contactRepository = contactRepository;
        _emailSenderService = emailSenderService;
        _ticketRepository = ticketRepository;
        _ticketMessageRepository = ticketMessageRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    [Queue(nameof(TypeQueue.synchronization))]
    public async Task StartMonitoringAsync(CancellationToken cancellationToken)
    {
        using var client = new ImapClient();

        try
        {
            await client.ConnectAsync(_smtpSettings.ImapHost, _smtpSettings.ImapPort, true, cancellationToken);
            await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password, cancellationToken);

            var inbox = client.Inbox;
            await inbox.OpenAsync(FolderAccess.ReadWrite, cancellationToken);

            var uids = await inbox.SearchAsync(SearchQuery.NotSeen, cancellationToken);

            foreach (var uid in uids)
            {
                var message = await inbox.GetMessageAsync(uid, cancellationToken);
                MonitorEmailsAsync(message);

                await inbox.AddFlagsAsync(uid, MessageFlags.Seen, true, cancellationToken);
            }

            await Task.Delay(TimeSpan.FromSeconds(15), cancellationToken);

            //BackgroundJob.Schedule<IEmailMonitoringService>(
            //    service => service.StartMonitoringAsync(cancellationToken),
            //    TimeSpan.FromSeconds(5));

            await client.DisconnectAsync(true, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error monitoring email");
            throw;
        }
    }

    private void MonitorEmailsAsync(MimeMessage email)
    {
        var fromAddress = email.From.Mailboxes.First().Address;
        var fromName = email.From.Mailboxes.First().Name;
        var subject = email.Subject;
        var body = email.TextBody;
        var priority = email.Priority;
        var attachments = GetAttachmentsFromEmail(email);

        BackgroundJob.Enqueue(() => ProcessEmailAsync(fromName, fromAddress, subject, body, priority, attachments));
    }

    public async Task ProcessEmailAsync(
        string fromName,
        string fromAddress,
        string subject,
        string body,
        MessagePriority priority,
        List<EmailAttachment> attachments
    )
    {
        try
        {
            var contact = await _contactRepository.GetByEmailAsync(fromAddress);
            var ticket = await _ticketRepository.GetByEmailAndSubjectAsync(fromAddress, subject.Replace("Re: ", ""));
            var newMessages = new List<TicketMessage>();

            if (ticket is not null)
            {
                var existingMessages = EmailParser.ParseEmailThread(body);
                await HandleAttachmentsAndAddMessages(ticket, attachments);

                foreach (var msg in existingMessages)
                {
                    msg.TicketId = ticket.Id;
                    var existingMessage = ticket.Messages?.FirstOrDefault(m => m.Message == msg.Message);
                    if (existingMessage is null)
                    {
                        newMessages.Add(msg);
                    }
                }

                if (newMessages.Any())
                {
                    await AddMessagesToTicket(ticket, newMessages);
                }
                return;
            }
            var ticketPriority = MapEmailPriorityToTicketPriority(priority);

            var newTicket = new DomainEntity.Ticket(
                fromName,
                contact is not null ? contact.GetFullName() : fromName,
                fromAddress,
                contact is not null ? contact.Phone : "",
                subject.Replace("Re: ", ""),
                TicketStatus.Onhold,
                DateTime.UtcNow.ToLocal(),
                ticketPriority
            );

            var messages = EmailParser.ParseEmailThread(body);
            messages.Select(msg => msg.TicketId = newTicket.Id);
            await HandleAttachmentsAndAddMessages(newTicket, attachments);

            newTicket.AddMessages(messages);

            await _ticketRepository.SaveAsync(newTicket);
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing email");
            throw;
        }
    }

    private async Task AddMessagesToTicket(DomainEntity.Ticket ticket, List<TicketMessage> messages)
    {
        await _ticketMessageRepository.SaveListAsync(messages);
        await _unitOfWork.CommitAsync();
    }

    private async Task HandleAttachmentsAndAddMessages(DomainEntity.Ticket ticket, List<EmailAttachment> attachments)
    {
        foreach (var attachment in attachments)
        {
            var ticketAttachment = new DomainEntity.Attachment(
                attachment.FileName,
                attachment.Content,
                attachment.ContentType,
                ticket.Id
            );
            ticket.AddAttachment(ticketAttachment);

            await _attachmentRepository.SaveAsync(ticketAttachment);
        }

        await _unitOfWork.CommitAsync();
    }

    private List<EmailAttachment> GetAttachmentsFromEmail(MimeMessage emailMessage)
    {
        var attachments = new List<EmailAttachment>();

        foreach (var attachment in emailMessage.Attachments)
        {
            if (attachment is MimePart mimePart)
            {
                using var memoryStream = new MemoryStream();
                mimePart.Content.DecodeTo(memoryStream);

                attachments.Add(new EmailAttachment
                (
                    mimePart.FileName,
                    memoryStream.ToArray(),
                    mimePart.ContentType.MimeType
                ));
            }
        }

        return attachments;
    }

    private TicketPriority MapEmailPriorityToTicketPriority(MessagePriority emailPriority)
    {
        return emailPriority switch
        {
            MessagePriority.Urgent => TicketPriority.High,
            MessagePriority.Normal => TicketPriority.Medium,
            MessagePriority.NonUrgent => TicketPriority.Low,
            _ => TicketPriority.Low,
        };
    }
}
