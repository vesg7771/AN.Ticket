using AN.Ticket.Domain.Entities.Base;

namespace AN.Ticket.Domain.Entities;
public class User : EntityBase
{
    public string FullName { get; private set; }
    public string Email { get; private set; }

    protected User() { }

    public User(
        Guid id,
        string fullName,
        string email
    )
    {
        Id = id;
        FullName = fullName;
        Email = email;
    }
}
