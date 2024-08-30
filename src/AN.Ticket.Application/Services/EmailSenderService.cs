using AN.Ticket.Application.Helpers.EmailSender;
using AN.Ticket.Application.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace AN.Ticket.Application.Services;
public class EmailSenderService : IEmailSenderService
{
    private readonly SmtpSettings _smtpSettings;

    public EmailSenderService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var senderName = email;
        var senderAddress = _smtpSettings.Username;

        var recipientAddress = email;

        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(senderAddress, senderAddress));
        emailMessage.To.Add(new MailboxAddress(senderName, recipientAddress));
        emailMessage.Subject = subject;

        emailMessage.Body = new TextPart("html")
        {
            Text = message
        };

        using var client = new SmtpClient();
        await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, false);
        await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
    }
}
