using AN.Ticket.Domain.Entities.Base;
using AN.Ticket.Domain.EntityValidations;
using AN.Ticket.Domain.ValueObjects;

namespace AN.Ticket.Domain.Entities;

public class Contact : EntityBase
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string PrimaryEmail { get; private set; }
    public string SecondaryEmail { get; private set; }
    public string Phone { get; private set; }
    public string Mobile { get; private set; }
    public string Department { get; private set; }
    public string Title { get; private set; }
    public List<SocialNetwork> SocialNetworks { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    protected Contact() { }

    public Contact(
        string firstName,
        string lastName,
        string primaryEmail
    )
    {
        if (string.IsNullOrEmpty(firstName)) throw new EntityValidationException("First name is required.");
        if (string.IsNullOrEmpty(primaryEmail)) throw new EntityValidationException("Primary email is required.");

        FirstName = firstName;
        LastName = lastName;
        PrimaryEmail = primaryEmail;
        SocialNetworks = new List<SocialNetwork>();
    }

    public void AddSocialNetwork(SocialNetwork socialNetwork)
    {
        if (socialNetwork == null) throw new EntityValidationException("Social network is required.");
        SocialNetworks.Add(socialNetwork);
    }

    public void AssignUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        User = user;
    }
}

