using AN.Ticket.Domain.Entities.Base;

namespace AN.Ticket.Domain.Entities;
public class User : EntityBase
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string ProfilePicture { get; private set; }

    protected User() { }

    public User(
        Guid id,
        string fullName,
        string email,
        string profilePicture = null
    )
    {
        Id = id;
        FullName = fullName;
        Email = email;
        ProfilePicture = profilePicture;
    }

    public void UpdateProfilePicture(string profilePicture)
        => ProfilePicture = profilePicture;

    public void UpdateFullName(string fullName)
        => FullName = fullName;

    public void UpdateEmail(string email)
        => Email = email;
}
