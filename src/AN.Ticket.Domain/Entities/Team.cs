using AN.Ticket.Domain.Entities.Base;
using AN.Ticket.Domain.EntityValidations;

namespace AN.Ticket.Domain.Entities;
public class Team : EntityBase
{
    public string Name { get; private set; }
    public ICollection<User> Members { get; private set; }

    public Team(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new EntityValidationException("Team name is required.");

        Name = name;
        Members = new List<User>();
    }

    public void AddMember(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        Members.Add(user);
    }

    public void RemoveMember(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        Members.Remove(user);
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new EntityValidationException("Team name is required.");
        Name = name;
    }

    public void ClearMembers()
    {
        Members.Clear();
    }
}

