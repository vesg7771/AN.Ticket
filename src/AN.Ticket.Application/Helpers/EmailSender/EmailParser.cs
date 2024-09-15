using AN.Ticket.Domain.Entities;
using System.Text.RegularExpressions;

namespace AN.Ticket.Application.Helpers.EmailSender;

public static class EmailParser
{
    public static List<TicketMessage> ParseEmailThread(string emailThread)
    {
        var messages = new List<TicketMessage>();

        var firstMessagePattern = new Regex(@"^(.+?)(?=(On .+? wrote:)|(Em .+? escreveu:)|(Suporte: .+)|\z)", RegexOptions.Singleline);
        var firstMatch = firstMessagePattern.Match(emailThread);

        if (firstMatch.Success)
        {
            var firstMessageBody = firstMatch.Groups[1].Value.Trim();
            firstMessageBody = CleanMessageBody(firstMessageBody);

            if (!string.IsNullOrEmpty(firstMessageBody) && !ContainsGreeting(firstMessageBody))
            {
                messages.Add(new TicketMessage(firstMessageBody, DateTime.UtcNow.ToLocalTime()));
            }
        }

        var messagePattern = new Regex(@"((On .+? wrote:)|(Em .+? escreveu:)|(Suporte: .+?))(.+?)(?=(On .+? wrote:)|(Em .+? escreveu:)|(Suporte: .+?)|\z)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        var matches = messagePattern.Matches(emailThread);

        foreach (Match match in matches)
        {
            var header = match.Groups[1].Value.Trim();
            var body = match.Groups[5].Value.Trim();
            body = CleanMessageBody(body);

            if (!string.IsNullOrEmpty(body) && !ContainsGreeting(body))
            {
                var dateTime = ExtractDateTimeFromHeader(header) ?? DateTime.UtcNow.ToLocalTime();
                messages.Add(new TicketMessage(body, dateTime.ToLocalTime()));
            }
        }

        return messages;
    }

    private static bool ContainsGreeting(string message)
    {
        var greetingPattern = @"\bOlá\b";
        return Regex.IsMatch(message, greetingPattern, RegexOptions.IgnoreCase);
    }

    private static string CleanMessageBody(string body)
    {
        var cleanedBody = Regex.Replace(body, @"^>+\s*", string.Empty, RegexOptions.Multiline);
        cleanedBody = Regex.Replace(cleanedBody, @"^Em .+ escreveu:\s*", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        return cleanedBody.Trim();
    }

    private static DateTime? ExtractDateTimeFromHeader(string header)
    {
        var dateTimePattern = @"\w{3}, \d{1,2} \w{3} \d{4} at \d{1,2}:\d{2}\s?[APM]{2}|[A-Z][a-z]{2}, \d{1,2} de [a-z]{3} de \d{4} às \d{1,2}:\d{2}";
        var match = Regex.Match(header, dateTimePattern);

        if (match.Success)
        {
            if (DateTime.TryParse(match.Value.Replace(" ", " "), out var dateTime))
            {
                return dateTime;
            }
        }

        return null;
    }
}
