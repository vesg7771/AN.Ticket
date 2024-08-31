using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Entities.Base;
using AN.Ticket.Domain.EntityValidations;

namespace AN.Ticket.Domain.ValueObjects;
public class SocialNetwork : EntityBase
{
    public string Name { get; private set; }
    public string Url { get; private set; }
    public Guid ContactId { get; private set; }
    public Contact Contact { get; private set; }

    protected SocialNetwork() { }

    public SocialNetwork(string name, string url, Guid contactId)
    {
        if (string.IsNullOrEmpty(name)) throw new EntityValidationException("Name is required.");
        if (string.IsNullOrEmpty(url)) throw new EntityValidationException("URL is required.");

        Name = name;
        Url = url;
        ContactId = contactId;
    }
}
