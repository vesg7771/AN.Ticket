using AN.Ticket.Domain.Entities.Base;

namespace AN.Ticket.Domain.Entities;

public class Attachment : EntityBase
{
    public string? FileName { get; private set; }
    public byte[] Content { get; private set; }
    public string? ContentType { get; private set; }
    public Guid TicketId { get; private set; }
    public Ticket? Ticket { get; private set; }

    public long Size => Content?.Length ?? 0;

    protected Attachment() { }

    public Attachment(
        string fileName,
        byte[] content,
        string contentType,
        Guid ticketId
    )
    {
        FileName = fileName;
        Content = content;
        ContentType = contentType;
        TicketId = ticketId;
    }
}
