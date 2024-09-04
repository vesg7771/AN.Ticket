using AN.Ticket.Domain.Entities.Base;
using AN.Ticket.Domain.EntityValidations;
using AN.Ticket.Domain.ValueObjects;

namespace AN.Ticket.Domain.Entities;

public class Contact : EntityBase
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Cpf { get; private set; }
    public string PrimaryEmail { get; private set; }
    public string SecondaryEmail { get; private set; }
    public string Phone { get; private set; }
    public string Mobile { get; private set; }
    public string Department { get; private set; }
    public string Title { get; private set; }
    public List<SocialNetwork> SocialNetworks { get; private set; }
    public Guid UserId { get; private set; }

    public User User { get; set; }

    protected Contact() { }

    public Contact(
        string firstName,
        string lastName,
        string primaryEmail,
        string secondaryEmail = null,
        string phone = null,
        string mobile = null,
        string department = null,
        string title = null
    )
    {
        if (string.IsNullOrEmpty(primaryEmail)) throw new EntityValidationException("Primary email is required.");

        FirstName = firstName;
        LastName = lastName;
        PrimaryEmail = primaryEmail;
        SecondaryEmail = secondaryEmail;
        Phone = phone;
        Mobile = mobile;
        Department = department;
        Title = title;
        SocialNetworks = new List<SocialNetwork>();
    }

    public void AddSocialNetwork(SocialNetwork socialNetwork)
    {
        if (socialNetwork == null) throw new EntityValidationException("Social network is required.");
        SocialNetworks.Add(socialNetwork);
    }

    public void AssignUser(Guid? userId)
    {
        if (!userId.HasValue) throw new EntityValidationException("User is required.");

        UserId = userId.Value;
    }

    public string GetFullName()
        => $"{FirstName} {LastName}";

    public bool ValidateCpf(string cpf)
    {
        if (string.IsNullOrEmpty(cpf)) return false;

        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11) return false;

        if (cpf.Distinct().Count() == 1) return false;

        var numbers = cpf.Substring(0, 9);
        var digits = cpf.Substring(9, 2);

        var sum = 0;
        for (var i = 0; i < 9; i++)
            sum += int.Parse(numbers[i].ToString()) * (10 - i);

        var result = sum % 11;

        if (result == 0 || result == 1)
        {
            if (int.Parse(digits[0].ToString()) != 0) return false;
        }
        else if (int.Parse(digits[0].ToString()) != 11 - result) return false;

        sum = 0;
        for (var i = 0; i < 10; i++)
            sum += int.Parse(cpf[i].ToString()) * (11 - i);

        result = sum % 11;

        if (result == 0 || result == 1)
        {
            if (int.Parse(digits[1].ToString()) != 0) return false;
        }
        else if (int.Parse(digits[1].ToString()) != 11 - result) return false;

        return true;
    }
}

